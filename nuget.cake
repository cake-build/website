#addin "nuget:https://api.nuget.org/v3/index.json?package=Polly&version=7.1.0"
#addin "nuget:https://api.nuget.org/v3/index.json?package=LitJson&version=0.13.0"
#addin "nuget:https://api.nuget.org/v3/index.json??package=Cake.FileHelpers&version=3.3.0"

using System.Net.Http;
using Polly;

public static void DownloadPackages(this ICakeContext context, DirectoryPath extensionDir, string[] packageIds)
{
    var retryPolicy = Policy
                        .Handle<Exception>()
                        .WaitAndRetry(5, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                        );

    using (var httpClient = new HttpClient())
    {
        Parallel.ForEach(
            packageIds,
            packageId => retryPolicy.Execute(
                    () => context.DownloadPackage(extensionDir, httpClient, packageId)
            )
        );
    }
}

public static void DownloadPackage(this ICakeContext context, DirectoryPath extensionDir, HttpClient httpClient, string packageId)
{
    context.Information("[{0}] fetching meta data...", packageId);
    var packageJson = httpClient.GetStringAsync($"https://api.nuget.org/v3/registration5-semver1/{packageId.ToLowerInvariant()}/index.json")
                    .GetAwaiter()
                    .GetResult();

    var package = LitJson.JsonMapper.ToObject<Package>(packageJson);

    var packageInfo = (
        from packageItem in package?.items ?? Enumerable.Empty<PackageItem>()
        from item in packageItem.items ?? Enumerable.Empty<PackageRevisions>()
        where item?.catalogEntry?.listed ?? false
        select new
        {
            id = packageId,
            version = item?.catalogEntry?.version,
            item?.packageContent
        }
    ).LastOrDefault();

    context.Verbose("[{0}] found {1}...", packageId, packageInfo);


    var packageDir = extensionDir.Combine($"{packageId}.{packageInfo.version}".ToLower());

    using (var stream = httpClient.GetStreamAsync(packageInfo.packageContent).GetAwaiter().GetResult())
    {
        using (var zipStream = new System.IO.Compression.ZipArchive(stream))
        {
            var extractedFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var entry in zipStream.Entries)
            {
                if (!entry.FullName.StartsWith("lib") || extractedFiles.Contains(entry.Name))
                {
                    continue;
                }

                extractedFiles.Add(entry.Name);

                var entryPath = packageDir.CombineWithFilePath(entry.FullName);

                switch (entryPath.GetExtension().ToLowerInvariant())
                {
                    case ".xml":
                    case ".dll":
                        break;

                    default:
                        continue;
                }

                context.Verbose("[{0}] {1}", packageId, entry.FullName);

                var directory = entryPath.GetDirectory();

                context.EnsureDirectoryExists(directory);

                using (System.IO.Stream source = entry.Open(),
                                        target = context.FileSystem.GetFile(entryPath).OpenWrite())
                {
                    source.CopyTo(target);
                }
            }
        }
    }

    context.FileWriteText(extensionDir.CombineWithFilePath($"{packageId}.version"), packageInfo.version);

    context.Information("[{0}] done.", packageId);
}

public class Package
{
    public PackageItem[] items { get; set; }
}

public class PackageItem
{
    public PackageRevisions[] items { get; set; }
    public string upper { get; set; }
}

public class PackageRevisions
{
    public PackageCatalogentry catalogEntry { get; set; }
    public string packageContent { get; set; }
}

public class PackageCatalogentry
{
    public string version { get; set; }
    public bool listed { get; set; }
}

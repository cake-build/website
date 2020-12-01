#addin "nuget:https://api.nuget.org/v3/index.json?package=Polly&version=7.1.0"
#addin "nuget:https://api.nuget.org/v3/index.json?package=LitJson&version=0.13.0"
#addin "nuget:https://api.nuget.org/v3/index.json??package=Cake.FileHelpers&version=3.3.0"
#addin "nuget:https://api.nuget.org/v3/index.json?package=NuGet.Protocol&version=5.7.0"

using System.Net.Http;
using System.Threading;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGetRepository = NuGet.Protocol.Core.Types.Repository;
using NuGet.Versioning;
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
        let version = item?.catalogEntry?.version
        let nugetVersion = version is null ? null : NuGetVersion.Parse(version)
        orderby !(nugetVersion?.IsPrerelease ?? false), nugetVersion
        select new
        {
            id = packageId,
            version = nugetVersion?.ToNormalizedString(),
            isPrerelease = nugetVersion?.IsPrerelease,
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
    context.FileWriteText(extensionDir.CombineWithFilePath($"{packageId}.isprerelease"), packageInfo.isPrerelease.GetValueOrDefault().ToString());

    context.Information("[{0}] done.", packageId);
}

// Cake compatilibity ranges derived from `LatestPotentialBreakingChange`
// Version range values use interval notation
// https://docs.microsoft.com/en-us/nuget/concepts/package-versioning#version-ranges
static VersionRange[] _compatibilityVersionRanges = new []
{
    "[1.0.0, )",
    "[0.33.0, 1.0.0)",
    "[0.28.0, 0.33.0)",
    "[0.26.0, 0.28.0)",
    "[0.22.0, 0.26.0)",
    "[0.16.0, 0.22.0)",
    "[0.15.0, 0.16.0)",
    "[0.14.0, 0.15.0)",
    "[0.13.0, 0.14.0)",
    "[0.12.0, 0.13.0)",
    "[0.11.0, 0.12.0)",
    "[0.10.0, 0.11.0)",
    "[0.9.0, 0.10.0)",
    "[0.8.0, 0.9.0)",
    "[0.7.0, 0.8.0)",
    "[0.6.0, 0.7.0)",
    "[0.5.0, 0.6.0)",
    "[0.4.0, 0.5.0)",
    "[0.3.0, 0.4.0)",
    "[0.2.0, 0.3.0)",
    "[0.1.0, 0.2.0)",
    "[0.0.0, 0.1.0)",
}
.Select(r => VersionRange.Parse(r))
.OrderByDescending(r => r.MinVersion)
.ToArray();

public static void CalcSupportedCakeVersions(this ICakeContext context, DirectoryPath extensionDir, IDictionary<string, string> packageVersionLookup)
{
    var allListedCakeVersions = GetAllListedCakeVersions();

    foreach (var item in packageVersionLookup)
    {
        var packageId = item.Key;
        var targetCakeVersion = item.Value is null ? null : new NuGetVersion(item.Value);

        var supportedCakeVersions = CalcSupportedCakeVersionsForExtension(targetCakeVersion, allListedCakeVersions);
        context.FileWriteText(extensionDir.CombineWithFilePath($"{packageId}.supportedcakeversions"), supportedCakeVersions);
    }
}

static IReadOnlyList<NuGetVersion> GetAllListedCakeVersions()
{
    using (var cacheContext = new SourceCacheContext { NoCache = true })
    {
        var repository = NuGetRepository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");

        var resource = repository.GetResourceAsync<PackageMetadataResource>().GetAwaiter().GetResult();

        var packages = resource.GetMetadataAsync("Cake", includePrerelease: true,
            includeUnlisted: true, cacheContext, NullLogger.Instance, CancellationToken.None).GetAwaiter().GetResult();

        var allVersionsOfCake = packages.OfType<PackageSearchMetadataRegistration>()
            .OrderByDescending(p => p.Version)
            .Select(p => p.Version)
            .ToList();

        return allVersionsOfCake;
    }
}

static string CalcSupportedCakeVersionsForExtension(NuGetVersion extensionVersion, IReadOnlyList<NuGetVersion> allListedCakeVersions)
{
    if (extensionVersion is null) return null;

    // Map AddInDiscoverer's TargetCakeVersion to a listed Cake version
    // (edge case if core libraries are released separately from the Cake package)
    var minCakeVersion = allListedCakeVersions
        .Where(v => v >= extensionVersion && v.IsPrerelease == extensionVersion.IsPrerelease)
        .OrderBy(v => v)
        .FirstOrDefault();

    if (minCakeVersion is null)
    {
        // Extension seems to reference a version of Cake that is not listed
        return null;
    }

    NuGetVersion maxCakeVersion = null;
    if (minCakeVersion.IsPrerelease)
    {
        // Extensions targeting pre-release versions of Cake are pinned to the specific pre-release version they target
        return $"{minCakeVersion}";
    }
    else
    {
        // Find the latest compatibility range that this extension falls into
        var compatRange = _compatibilityVersionRanges
            .Where(r => r.Satisfies(minCakeVersion))
            .OrderByDescending(r => r.MinVersion)
            .First();

        // Find the maximum stable Cake version that satisfies the compat range
        maxCakeVersion = allListedCakeVersions
            .Where(v => compatRange.Satisfies(v) && !v.IsPrerelease)
            .OrderByDescending(v => v)
            .First();
    }

    return (minCakeVersion == maxCakeVersion)
        ? $"{maxCakeVersion}"
        : $"{minCakeVersion} - {maxCakeVersion}";
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

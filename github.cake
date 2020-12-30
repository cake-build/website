#addin "nuget:https://api.nuget.org/v3/index.json?package=Octokit&version=0.32.0"
#addin "nuget:https://api.nuget.org/v3/index.json?package=NuGet.Versioning&version=5.7.0"

using Octokit;
using NuGet.Versioning;

static CakeGitHubReleaseInfo _cakeGitHubReleaseInfo;

public static CakeGitHubReleaseInfo GetCakeGitHubReleaseInfo(this ICakeContext context, DirectoryPath releaseDir)
{
    if (!(_cakeGitHubReleaseInfo is null))
    {
        return _cakeGitHubReleaseInfo;
    }

    // We cache the latest Cake release information in a file to speed up local development and also
    // to decrease the chance of getting rate limited by GitHub when running unauthenticated
    var cachedGitHubDir = context.MakeAbsolute(releaseDir.Combine("cake-github"));
    var cachedReleaseInfoFile = cachedGitHubDir.CombineWithFilePath($"{nameof(CakeGitHubReleaseInfo)}.yml");
    if (context.FileExists(cachedReleaseInfoFile))
    {
        context.Verbose("Retrieving Cake release information from cached file {0}...", cachedReleaseInfoFile);

        _cakeGitHubReleaseInfo = context.DeserializeYamlFromFile<CakeGitHubReleaseInfo>(cachedReleaseInfoFile);
        LogGitHubReleaseInfo(context, _cakeGitHubReleaseInfo);

        return _cakeGitHubReleaseInfo;
    }

    context.Information("Retrieving Cake release information from GitHub...");

    var client = new GitHubClient(new ProductHeaderValue("cake-build-website"));

    var gitHubAccessToken =  context.EnvironmentVariable("git_access_token");
    if (!string.IsNullOrWhiteSpace(gitHubAccessToken))
    {
        client.Credentials = new Credentials(gitHubAccessToken);
    }

    var allCakeReleases = client.Repository.Release.GetAll("cake-build", "cake")
        .GetAwaiter()
        .GetResult()
        .Where(r => !r.Draft && r.PublishedAt.HasValue && r.Name.StartsWith("v", StringComparison.Ordinal))
        .OrderByDescending(r => new NuGetVersion(r.Name.Substring(1)))
        .ToList();

    var latestCakeRelease = allCakeReleases
        .First();

    _cakeGitHubReleaseInfo = new CakeGitHubReleaseInfo
    {
        LatestReleaseName = latestCakeRelease.Name,
        LatestReleaseUrl = latestCakeRelease.HtmlUrl,
    };

    LogGitHubReleaseInfo(context, _cakeGitHubReleaseInfo);

    context.EnsureDirectoryExists(cachedReleaseInfoFile.GetDirectory());
    context.SerializeYamlToFile<CakeGitHubReleaseInfo>(cachedReleaseInfoFile, _cakeGitHubReleaseInfo);

    return _cakeGitHubReleaseInfo;
}

private static void LogGitHubReleaseInfo(ICakeContext context, CakeGitHubReleaseInfo releaseInfo)
{
    context.Information("Cake Latest Release Name: {0}", releaseInfo.LatestReleaseName);
    context.Information("Cake Latest Release Url: {0}", releaseInfo.LatestReleaseUrl);
}

public class CakeGitHubReleaseInfo
{
    public string LatestReleaseName { get; set; }
    public string LatestReleaseUrl { get; set; }
}

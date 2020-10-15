#addin nuget:?package=Polly&version=7.1.0
#addin nuget:?package=Cake.Kudu.Client&version=0.8.0
#addin nuget:?package=Cake.Http&version=0.7.0

using Polly;

Setup<Deployment>(
  context=>{
    context.Information("Performing setup...");

    FilePath zipFilePath = GetFiles("./**/output.zip").Single();

    context.Information("Found {0} zip file to deploy {0}.", zipFilePath);

    var kuduPublishVariables = context.EnvironmentVariables()
          .Where(key => key.Key.StartsWith("KUDU_CLIENT_BASEURI_") ||
                        key.Key.StartsWith("KUDU_CLIENT_USERNAME_") ||
                        key.Key.StartsWith("KUDU_CLIENT_PASSWORD_"))
          .ToDictionary(
              key => key.Key,
              value => value.Value,
              StringComparer.OrdinalIgnoreCase);

    var shouldPurgeCloudFlareCache = context.EnvironmentVariable("SHOULD_PURGE_CLOUDFLARE_CACHE", false);

    var cloudflareAuthEmail = context.EnvironmentVariable("CLOUDFLARE_AUTH_EMAIL");

    var cloudflareAuthKey = context.EnvironmentVariable("CLOUDFLARE_AUTH_KEY");

    var cloudflareZoneId = context.EnvironmentVariable("CLOUDFLARE_ZONE_ID");

    var deploymentTargets = kuduPublishVariables.Keys
                                .Select(
                                    key=>{
                                        switch(key)
                                        {
                                          case string baseUri when baseUri.StartsWith("KUDU_CLIENT_BASEURI_"):
                                            return baseUri.Substring(20);
                                          case string userName when userName.StartsWith("KUDU_CLIENT_USERNAME_"):
                                            return userName.Substring(21);
                                          case string password when password.StartsWith("KUDU_CLIENT_PASSWORD_"):
                                            return password.Substring(21);
                                          default:
                                            return key;
                                        }
                                    }
                                    ).Distinct()
                                    .Select(
                                        key=> new DeploymentTarget(
                                            key,
                                            context.KuduClient(
                                              baseUri:  kuduPublishVariables["KUDU_CLIENT_BASEURI_" + key],
                                              userName:  kuduPublishVariables["KUDU_CLIENT_USERNAME_" + key],
                                              password:  kuduPublishVariables["KUDU_CLIENT_PASSWORD_" + key])))
                                    .ToArray();
    context.Information("Setup complete found {0} deployment targets.", deploymentTargets.Length);

    return new Deployment(zipFilePath, deploymentTargets, shouldPurgeCloudFlareCache, cloudflareAuthEmail, cloudflareAuthKey, cloudflareZoneId);
});

public class DeploymentTarget
{
    public string Key { get; }

    public IKuduClient KuduClient { get; }

    public DeploymentTarget(string key, IKuduClient kuduClient)
    {
        Key = key;
        KuduClient = kuduClient;
    }
}

public class Deployment
{
    public Policy RetryPolicy { get; } = Policy
                                            .Handle<Exception>()
                                            .WaitAndRetry(5,
                                                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public FilePath ZipFilePath { get; }

    public DeploymentTarget[] Targets { get; }

    public bool ShouldPurgeCloudFlareCache { get; }

    public string CloudflareAuthEmail { get; }

    public string CloudflareAuthKey { get; }

    public string CloudflareZoneId { get; }

    public Deployment(FilePath zipFilePath, DeploymentTarget[] targets, bool shouldPurgeCloudFlareCache, string cloudflareAuthEmail, string cloudflareAuthKey, string cloudflareZoneId)
    {
        ZipFilePath = zipFilePath;
        Targets = targets;
        ShouldPurgeCloudFlareCache = shouldPurgeCloudFlareCache;
        CloudflareAuthEmail = cloudflareAuthEmail;
        CloudflareAuthKey = cloudflareAuthKey;
        CloudflareZoneId = cloudflareZoneId;
    }
}

Task("Deploy")
  .Does<Deployment>(
      (context, deployment)
        => Parallel.ForEach(
              deployment.Targets,
              target => {
                  context.Information("Deploying {0} to {1}...", deployment.ZipFilePath, target.Key);

                   deployment.RetryPolicy.Execute(
                      () => target.KuduClient.ZipDeployFile(deployment.ZipFilePath));

                  context.Information("Deployed {0} to {1}.", deployment.ZipFilePath, target.Key);
              }
           )
  );

Task("Purge-Cloudflare-Cache")
    .IsDependentOn("Deploy")
    .Does<Deployment>((context, deployment) =>
{
    if(deployment.ShouldPurgeCloudFlareCache)
    {
        context.Information("Purging Cloudflare Cache...");

        var settings = new HttpSettings()
            .SetRequestBody("{ \"purge_everything\": true }")
            .AppendHeader("X-Auth-Email", deployment.CloudflareAuthEmail)
            .AppendHeader("X-Auth-Key", deployment.CloudflareAuthKey);

        var result = HttpSend(
            string.Format("https://api.cloudflare.com/client/v4/zones/{0}/purge_cache", deployment.CloudflareZoneId),
            "DELETE",
            settings);

        context.Information(result);
    }
    else
    {
        context.Information("Skipping purge of Cloudflare Cache");
    }
});

RunTarget("Purge-Cloudflare-Cache");
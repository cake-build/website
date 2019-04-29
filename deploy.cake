#addin nuget:?package=Polly&version=7.1.0
#addin nuget:?package=Cake.Kudu.Client&version=0.8.0

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

    return new Deployment(zipFilePath, deploymentTargets);
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

    public Deployment(FilePath zipFilePath, DeploymentTarget[] targets)
    {
        ZipFilePath = zipFilePath;
        Targets = targets;
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


RunTarget("Deploy");
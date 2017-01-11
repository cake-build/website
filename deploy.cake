#tool "nuget:https://www.nuget.org/api/v2/?package=KuduSync.NET"
#addin "nuget:https://www.nuget.org/api/v2/?package=Cake.Kudu"
#addin "nuget:https://www.nuget.org/api/v2/?package=Cake.Git"
#load "build.cake"
DirectoryPath   outputPath          = MakeAbsolute(Directory("./output"));
DirectoryPath   deploymentPath;

DateTime        utcNow              = DateTime.UtcNow;
string          version             = string.Format(
                                            "{0}.{1}.{2}.{3}{4:00}",
                                            utcNow.Year,
                                            utcNow.Month,
                                            utcNow.Day,
                                            utcNow.Hour,
                                            utcNow.Minute
                                        );
string          semVersion          = string.Format(
                                            "{0}+{1}.{2}.{3}",
                                            version,
                                            GitBranchCurrent("./").FriendlyName,
                                            GitLogTip("./").Sha,
                                            System.Environment.MachineName
                                        );


Setup(ctx =>
{
    if (!Kudu.IsRunningOnKudu)
    {
        throw new Exception("Not running on Kudu");
    }

    deploymentPath = Kudu.Deployment.Target;

    if (!DirectoryExists(deploymentPath))
    {
        throw new DirectoryNotFoundException(
            string.Format(
                "Deployment target directory not found {0}.",
                deploymentPath
                )
            );
    }

    // Executed BEFORE the first task.
    Information("Building version {0}...", semVersion);
});

Task("Deploy")
    .IsDependentOn("Build")
    .Does(() =>
    {
        Information("Deploying web from {0} to {1}...", outputPath, deploymentPath);
        Kudu.Sync(outputPath);
    });

RunTarget("Deploy");
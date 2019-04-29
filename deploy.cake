Task("Deploy")
    .Does(() =>
{
    Information("Hi from Azure Release Pipeline");
});

RunTarget("Deploy");
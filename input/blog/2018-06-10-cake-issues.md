---
title: Cake.Issues - A suite of addins for issue handling in Cake
category: Addins
author: Pascal Berger
---

Do you want to break your build on JetBrains InspectCode issues?
Do you want to create nice reports for StyleCop issues in your code?
Do you want to have ESLint issues reported as comments to pull requests?
The [Cake.Issues addins](https://cake-contrib.github.io/Cake.Issues.Website/) allows you to do this and much more.
Read issues from different analyzer or linters, create reports or add them as comments to pull requests.

<!--excerpt-->

### The Cake.Issues addins

While some linting tools provide nice reporting capabilities, others don't.
Some build systems provide tasks to report issues into pull requests.
But if you're using another build system your out of luck.
Maybe you found a new nice linting tool which does exactly what you need, but probably it won't integrate well
with your existing other linting tools.

The [Cake.Issues addins](https://cake-contrib.github.io/Cake.Issues.Website/) tries to solve this,
by providing a common and extensible infrastructure for issue management in Cake build scripts.

Unlike other Cake addins, the Cake Issues addin consists of over 10 different addins, working together
and providing over 50 aliases which can be used in Cake build scripts to work with issues.
The addins are built in a modular architecture and are providing different extension points which allows
to easily enhance them for supporting additional analyzers, linters, report formats and code review systems.

### Preparation work

In the following examples we assume a Visual Studio solution, containing some Roslyn Analyzers, which we want
to build and additionally run JetBrains InspectCode on it.

For creating an XML log from MsBuild we need the `XmlFileLogger` class from the [MSBuild Extension Pack](http://www.msbuildextensionpack.com/)
and the JetBrains InspectCode tool:

```csharp
#tool "nuget:?package=MSBuild.Extension.Pack"
#tool "nuget:?package=JetBrains.ReSharper.CommandLineTools"
```

Now we can use the aliases provided by Cake out of the box to build the solution and run JetBrains InspectCode resulting in two log files containing the issues:

```csharp
var repoRootFolder = MakeAbsolute(Directory("./"));
var msBuildLog = repoRootFolder.CombineWithFilePath("msbuild.log");
var inspectCodeLog = repoRootFolder.CombineWithFilePath("inspectCode.log");

Task("Build")
    .Does(() =>
{
    var settings = new MSBuildSettings()
    .WithTarget("Rebuild")
    .WithLogger(
        Context.Tools.Resolve("MSBuild.ExtensionPack.Loggers.dll").FullPath,
        "XmlFileLogger",
        string.Format(
            "logfile=\"{0}\";verbosity=Detailed;encoding=UTF-8",
            msBuildLog)
    );

    MSBuild(repoRootFolder.Combine("src").CombineWithFilePath("MySolution.sln"), settings);
});

Task("Run-InspectCode")
    .Does(() =>
{
    var settings = new InspectCodeSettings() {
        OutputFile = inspectCodeLog
    };

    InspectCode(repoRootFolder.Combine("src").CombineWithFilePath("MySolution.sln"), settings);
});
```

### Reading issues

The `Cake.Issues` addin now allows you to parse the generated log files and retrieve the issues in a common structure.
The addin provides aliases for reading issues and can be used to aggregate issues from different sources.
This can for example be useful to break builds based on the reported issues.

To read the issues we need to import the following core addin:

```csharp
#addin "Cake.Issues"
```

We also need to import at least one issue provider.
Issue provider are separate addins which can provide issues from a specific linter or tool.
The [issue provider](https://cake-contrib.github.io/Cake.Issues.Website/docs/issue-providers/) documentation gives an
overview of available issue providers including samples how to integrate them in a Cake build script.

In the following example the issue providers for reading warnings from MsBuild log files
and from JetBrains InspectCode are imported:

```csharp
#addin "Cake.Issues.MsBuild"
#addin "Cake.Issues.InspectCode"
```

Finally we  define a task where we call the core addin with the desired issue providers.
The following example reads the reported issues with comments formatted as Markdown:

```csharp
IEnumerable<IIssue> issues = null;

Task("Read-Issues")
    .IsDependentOn("Build")
    .IsDependentOn("Run-InspectCode")
    .Does(() =>
{
    var settings =
        new ReadIssuesSettings(repoRootFolder)
        {
            Format = IssueCommentFormat.Markdown
        };

    issues = ReadIssues(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                msBuildLog,
                MsBuildXmlFileLoggerFormat),
            InspectCodeIssuesFromFilePath(
                inspectCodeLog)
        },
        settings);

    Information("{0} issues are found.", issues.Count());
});
```

### Creating reports

With the issues from the different tools in a common format it is now possible to create reports out of it.
The `Cake.Issues.Reporting` addin provides the required aliases for this.
The concrete reporting format is defined in separate [report format addins](https://cake-contrib.github.io/Cake.Issues.Website/docs/report-formats/).

Currently there's one report format available which allows you to render reports using [RazorEngine](https://github.com/Antaris/RazorEngine).

The following example will create a HTML report of the issues:

```csharp
#addin "Cake.Reporting"
#addin "Cake.Reporting.Generic"

Task("Create-Report")
    .IsDependentOn("Read-Issues")
    .Does(() =>
{
    // Create HTML report using Data Table template.
    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDataTable),
        repoRootFolder,
        repoRootFolder.CombineWithFilePath("report.html"));
});
```

The result will look like this:

![Report](/assets/img/cake-issues/htmldatatable.png)

The addin provides some [out of the box templates](https://cake-contrib.github.io/Cake.Issues.Website/docs/report-formats/generic/templates/),
but you can also pass your own [custom template](https://cake-contrib.github.io/Cake.Issues.Website/docs/report-formats/generic/examples#use-custom-template)
in which you can do whatever RazorEngine is capable of doing.

### Reporting issues to pull requests

The last core part of the Cake Issues suite is for reporting issues as comments to pull requests.
The `Cake.Issues.PullRequests` addin provides the required aliases and the implementation for the concrete pull request system
is again implemented in separate [pull request system addins](https://cake-contrib.github.io/Cake.Issues.Website/docs/pull-request-systems/).

The following example will report issues as comments to Team Foundation Server:

```csharp
#addin "Cake.Issues.PullRequests"
#addin "Cake.Issues.PullRequests.Tfs"

Task("ReportIssuesToPullRequest").Does(() =>
{
    ReportIssuesToPullRequest(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                msBuildLog,
                MsBuildXmlFileLoggerFormat),
            InspectCodeIssuesFromFilePath(
                inspectCodeLog)
        },
        TfsPullRequests(
            new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
            "refs/heads/feature/myfeature",
            TfsAuthenticationNtlm()),
        repoRootFolder);
});
```

The result will look like this in Team Foundation Server:

![Pull request integration](/assets/img/cake-issues/pullrequests.png)

### Conclusion

The [Cake.Issues addins](https://cake-contrib.github.io/Cake.Issues.Website/) provide a common way to integrate different linters and tools into a Cake build script.
It's fully [extensible](https://cake-contrib.github.io/Cake.Issues.Website/docs/extending/) for additional issue providers, report formats or pull request systems.

Documentation is available on its own [website](https://cake-contrib.github.io/Cake.Issues.Website/).

While the addins already cover a wide range of scenarios we're always looking for people interested in implementing additional issue providers,
report formats & templates or pull request systems.

---

My name is Pascal Berger.
I'm working as a Software Architect at [BBT Software AG](http://www.bbtsoftware.ch).
You can find me on [Twitter](https://x.com/hereispascal) or [GitHub](https://github.com/pascalberger).

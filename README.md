# Cake Website - An Amazing Project

This is a Cake website. It's a static site generated by [Wyam](http://wyam.io) which is hosted on Azure. Wyam is a static content toolkit and can be used to generate web sites, produce documentation, create ebooks, and much more.

## Contributing

Any contributions are appreciated, no matter how big or small. The Cake site consists of several different sections and each one is described below.

## Documentation

The basic documentation pages can be found under `./input/docs`. The directory structure mirrors what's on the site. Most pages are written in Markdown. To add a new page, just add a new file.

## Blog

The Cake site contains a blog where important announcements and other relevant information are posted. The blog posts can be found under `./input/blog`. As with documentation pages, blog posts are written in Markdown. The file name for each blog post contains its published date in the format `YYYY-MM-DD-title.md`.

## Addins

> PLEASE NOTE: Addin authors do not have to manually create YAML files as there is an automated process that scans nuget.org twice per day to find all addins that follow the recommended naming convention (which is `Cake.xxx` where xxx describes the functionality provided by the addin) and generates the appropriate YAML content based on the metadata for the NuGet package.

All addins are specified in individual YAML files under `./addins`. Adding an addin here will trigger downloading it's NuGet Package during site generation and will include it in the "Reference" and "Addins" sections of the Cake site.

The format of an addin file generally looks like:

```
Name: Cake.Wyam
NuGet: Cake.Wyam
Prerelease: true
Assemblies:
- "/**/Cake.Wyam.dll"
Repository: https://github.com/Wyamio/Wyam
Author: Dave Glick, Gary Ewan Park
Description: "An alias that generates static sites and other content using Wyam."
Categories:
- Documentation
- Static Site Generation
```

Note that the `Prerelease` flag can be omitted for non-prerelease packages and controls whether NuGet will attempt to download a prerelease version of the package when generating the site.

# Building

The site is built using Cake (of course!). There are a number of different targets depending on what you're working on and how complete you want the generated site to be.

`build --target=GetSource` will download the Cake source code that the generation process uses to create the "API" section.

`build --target=GetAddinPackages` will download new NuGet packages for all specified addins. These packages are used to create the "Reference" and "Addins" sections.

`build --target=GetArtifacts` will download both the Cake source code and the addin NuGet packages.

`build --target=Build` will run a complete build, downloading new copies of Cake source code and addin NuGet packages. Note that due to the number of addins and the complexity of generating complete API documentation, the site generation may take a while (sometimes as long as 20 minutes).

`build --target=Preview` will run a build but *will not* download Cake source code or NuGet packages. This lets you shorten the build cycle by avoiding the time to obtain those resources if you've already downloaded them, or to bypass them altogether if you're just working on something like general documentation pages. This target will also launch a preview server to look at the generated site from a local web browser. The URL of the generated preview site is `http://localhost:5080/`.

---
title: Cake support in WhiteSource Renovate
category: News
author: nils-a
---

[WhiteSource Renovate][renovate] is a solution for automating dependency updates in software projects which works (among others)
hosted in GitHub (much like dependabot does).

In the new [version 24.114.0][cake-in-renovate-release] renovate added support for Cake files! 

So, as of now it is possible to have pull requests automatically created to update `#addin`, `#tool` and `#module` references in Cake files.

Big thanks go to the people behind WhiteSource Renovate for getting this done so quickly!

<!--excerpt-->

### Enabling Renovate (on GitHub)

If renovate was never activated in an account on GitHub, the process starts by installing the [renovate app][gh-marketplace].

To add an existing project to renovate (after renovate was installed) go to *Organization Account* -> *Installed GitHub Apps* for organizations or 
*Account Settings* -> *Applications* for personal accounts. Click *Configure* on the *Renovate* application and add all repositories that should be 
enabled for renovate.

When renovate is activated first on a project, it starts the onboarding process by creating a pull request
that adds a (very bare) configuration (in the form of a `renovate.json` file) to the repository. 

`renovate.json` has [many configuration options][config-options], a simple example to add some labels to each pull request would look like this:

```json
{
  "$schema": "https://docs.renovatebot.com/renovate-schema.json",
  "extends": [
    "config:base"
  ],
  "labels": ["dependencies"],
  "cake": {
    "addLabels": ["cake", "build"]
  },
  "github-actions": {
    "addLabels": ["github_actions", "build"]
  },
  "nuget": {
    "addLabels": [".NET"]
  }
}
```


[renovate]: https://www.whitesourcesoftware.com/free-developer-tools/renovate
[cake-in-renovate-release]: https://github.com/renovatebot/renovate/releases/tag/24.114.0
[gh-marketplace]: https://github.com/marketplace/renovate
[config-options]: https://docs.renovatebot.com/configuration-options/
---
Title: Frequently Asked Questions
---

[//]: # (TOC Begin)
* [How do you pin the version of Cake being used?](#how-do-you-pin-the-version-of-cake-being-used?)
* [How do you pin the version of an Addin being used?](#how-do-you-pin-the-version-of-an-addin-being-used?)
* [How do you pin the version of a Tool being used?](#how-do-you-pin-the-version-of-a-tool-being-used?)
* [How do you pin the version of a Module being used?](#how-do-you-pin-the-version-of-a-module-being-used?)

[//]: # (TOC End)


### How do you pin the version of Cake being used?

This is done via the packages.config file at the root of the tools folder.  See this [tutorial](https://cakebuild.net/docs/tutorials/pinning-cake-version) for more information.

### How do you pin the version of an Addin being used?

This can either be done through the packages.config file in the root of the Addins folder, or via the addin pre-processor directive in your Cake script.  See this [tutorial](https://cakebuild.net/docs/tutorials/pinning-cake-version) for more information.

### How do you pin the version of a Tool being used?

This can either be done through the packages.config file in the root of the Tools folder, or via the tool pre-processor directive in your Cake script.  See this [tutorial](https://cakebuild.net/docs/tutorials/pinning-cake-version) for more information.

### How do you pin the version of a Module being used?

This can be done through the packages.config file in the root of the Modules folder.  See this [tutorial](https://cakebuild.net/docs/tutorials/pinning-cake-version) for more information.

### How do you enable a different verbosity for logging within Cake?

This has been answered on a Stack Overflow question [here](https://stackoverflow.com/questions/386586600).
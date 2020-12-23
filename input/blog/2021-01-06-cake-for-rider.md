---
title: Cake for Rider released
category: News
author: nils-a
---

I am very proud to announce the first release (version 0.1.0) of the [Cake for Rider](https://plugins.jetbrains.com/plugin/15729-cake-rider) extension for [JetBrains Rider](https://www.jetbrains.com/rider/). This extension brings run configurations for Cake tasks, as well a tool window to show Cake scripts and tasks in the project.

<!--excerpt-->

### Cake Tasks tool window

Cake files in the project are automatically found by extension and their tasks are displayed in a tool window:

![Cake tasks tool window](/assets/img/cake-rider/blog-0.1.0/toolWindow.png){.img-responsive}

Here, a double click on the task will run the task immediately:

![Running a Cake task](/assets/img/cake-rider/blog-0.1.0/cake-run.png){.img-responsive}

Alternatively, the buttons at the top of the tool window can be used to either run the task directly,
or create a new run configuration.

### Cake run configurations

It is possible to have Cake tasks as run configurations:

![Run configurations](/assets/img/cake-rider/blog-0.1.0/runConfigurations.png){.img-responsive}

The configurations can either be created from an existing Cake task, using the tool window or 
created manually using the run configuration editor:

![Run configuration editor](/assets/img/cake-rider/blog-0.1.0/runConfiguration-editor.png){.img-responsive}

### Getting started

In Rider, go to File -> Settings -> Plugins -> Marketplace and search for *Cake for Rider*

![Cake for Rider Plugin](/assets/img/cake-rider/blog-0.1.0/riderPlugin.png){.img-responsive}

### Feedback and contributions

Feedback and contributions (as well as ideas and feature requests) are [always welcome](https://github.com/cake-build/cake-rider). 

### Thanks

I would like to thank [Anna Dolbina](https://github.com/anna-dolbina) who created a plugin with similar functions and who jumped right in to help us creating this official plugin.

I would like to thank [Matt Ellis](https://github.com/citizenmatt) for his valuable help and input.
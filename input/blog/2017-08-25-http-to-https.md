---
title: HTTP to HTTPS
category: News
author: gep13
---

As some of you may have noticed, the [Cake Website](https://www.cakebuild.net/) has changed to use an SSL Certificate.  This is something that has been [on our cards for a while](https://github.com/cake-build/website/issues/40), it is just something that we never got around to.

## History

Originally, we thought that we would purchase our own SSL Certificate, however, we never got around to it.  Then, after joining the .NET Foundation, we had the chance to have them purchase the certificate for us, but again, we never got around to it.  However, after some discussion, we decided to use [Cloudflare](https://www.cloudflare.com/) to sit in front on our site, which means that we get an SSL certificate, as well as caching, for free.  We like free.

## What does this mean?

For the most part, the switch to HTTPS doesn't mean anything, and your normal everyday use of the website should not change.  We have put in place HTTP 301 redirects, so any attempt to access a non-HTTPS link directed at the site should redirect you through to the HTTPS version.  You can see that in action if you try to access this URL [http://www.cakebuild.net/](http://www.cakebuild.net/) you should end up at the HTTPS version.

There is one area where HTTPS is important though, and that is when it comes to accessing the bootstrappers, config file, and packages.config file, which are downloaded either through our IDE integrations, or from the documentation on the website.  These are files that we, the Cake Team provide to you, and as a result, we want to make sure that these files haven't been tampered with.  Using HTTPS on the site ensures that this happens, and prevents the chance of a man in the middle attack, which could, in theory, tamper with those files.

All the documentation on the website has been updated to use the HTTPS URLs, as well as all IDE integrations, i.e:

* Visual Studio
* Visual Studio Code
* Yeoman Generators

have also all been updated to use the new HTTPS URLs for these files.

To be clear, these are files that we strongly recommended that you download and check into your source control repository.  Therefore, downloading of these files should be a one time thing for each project that you set up.  Without the SSL certificate in place, there was a very real chance that the downloaded files could have been tampered with.  There is an element of due diligence expected when downloading arbitrary scripts from the internet, i.e. you wouldn't just download and run a script from `i-am-a-hacker.net`.  Having verified the downloaded files, you can commit them to your source control repository.  With the older version of the bootstrappers in your source control repository, they would still be downloading the packages.config (if required, i.e. not committed into your source control repository) from a non-HTTPS URL on each build.  As a result, again, on each build there was a chance that a malicious user could tamper with the downloaded file.  To be clear, this is not a file that is executed, but rather placed on your file system, and packages are restored from that file using NuGet.  As a result, in theory, you could have had packages restored onto your server/computer that you would not have expected to be there.  I think you will all agree that this is a minimal risk situation.

Going forward, none of the things that have been described above should be a problem.  The introduction of the SSL Certificate means that all future downloads from the Cake Website will be exactly what is expected.

## What do I need to do?

If you have already committed both the bootstrapper and the packages.config file into your repository, then _technically_ there is nothing that you need to do.  Since both artifacts exist in your repository, no additional downloads happen from the Cake Website, so you don't need to do anything immediately.  In the longer term, we would recommend that you pull the latest default bootstrapper(s) and commit them into your source control repository.

If you don't, for whatever reason, have the packages.config committed into your source control repository, then there is one thing that you need to do, and that is to refresh the bootstrapper(s).  This can be easily done fetching the latest bootstrapper(s) using the instructions [here](https://www.cakebuild.net/docs/tutorials/setting-up-a-new-project).  With this change made, simply commit the changes back into your source control repository, and you are good to go.

**NOTE:** If you have customised your bootstrapper(s), and you don't simply want to override it with the new default one(s), you can see the changes that were made to the default bootstrapper(s) in this [commit](https://github.com/cake-build/resources/commit/2ed021f5cfef2d1106b0caf6c801633bbfb58cc0).
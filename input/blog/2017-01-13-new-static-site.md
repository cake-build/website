title: New Static Cake Website
category: Announcement
author: daveaglick
---
Hi! My name is Dave Glick and I run the [Wyam static site engine](http://wyam.io) project. I'm also a big Cake fanboy, and over the last couple months I've been privileged to work with the rest of the Cake team on transitioning the Cake website from a dynamic ASP.NET website to a static one built by [Wyam](http://wyam.io). This effort was more than mere window dressing; the new site goes beyond the old one in some interesting ways:
- Every API type now has a clickable type hierarchy diagram on it's page.
- A new quick-search provides instant API type searching across the entire API surface (including addins).
- Type names are automatically linked to their corresponding API doc in documentation and blog posts.
- The documentation section is more flexible and can accommodate arbitrary nesting levels (which we plan to take advantage of soon).
- Being fully static, no run-time evaluation is needed resulting in faster page delivery and easier caching.

In the course of converting the Cake site, I was also working on a more general purpose [docs recipe](http://wyam.io/recipes/docs) so that anyone can take advantage of the cool features we've added here. This will also benefit the Cake site because as the Wyam docs recipe gets enhanced, this site will automatically benefit too. We'll also be going through the backlog of issues on the [website repo](https://github.com/cake-build/website) now that we've got a new engine in place.

Please keep an eye open for anything that looks amiss. While we've put the new site through some exhaustive testing, it's always possible we missed something. If you do find something wrong, please report it by opening an issue at the [website repo](https://github.com/cake-build/website).

Finally, I'd like to thank the Cake team for coming along on this adventure with me. This was the ideal test case for the new [Wyam docs recipe](http://wyam.io/recipes/docs) and simultaneously building out both has proven hugely beneficial. As this project has shown, Cake and Wyam work really well together and I'm looking forward to exploring more ways that the two can interoperate (did you know there's already a [Wyam addin for Cake](/dsl/wyam)?).

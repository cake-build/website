Order: 10
Excerpt: Modules allow augmenting, changing or replacing the internal logic of Cake
RedirectFrom: docs/fundamentals/modules
---
Modules are a special Cake component designed to augment, change or replace the internal logic of Cake itself.
Modules can be used, for example, to replace the built-in Cake build log, process runner or tool locator, just to name a few.
Internally, this is how Cake manages its "moving parts", but you can also load modules as part of running your build script, which will allow you to replace/change how Cake works as part of your build code.
Order: 30
Title: Tools
Description: Working with external tools
---

During a build tasks like compiling, linting, testing, etc need to be execute.
Cake itself is only a build orchestrator.
For achieving the previously mentioned task Cake calls different tools (like MsBuid, NUnit, etc).

Cake supports installing tools which are distributed as NuGet packages and provides logic to find tool locations during runtime.

:::{.alert .alert-info}
See [working with tools](/docs/writing-builds/tools/) for details.
:::

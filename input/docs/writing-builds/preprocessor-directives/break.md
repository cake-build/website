Order: 60
Title: Break directive
Description: Directive to stop while debugging
---

The break directive, when placed on any line within your Cake file, will cause `System.Diagnostics.Debugger.Break();` to be emitted at runtime.  As a result, when used in conjunction with the `-debug` parameter (which can be passed into the Cake.exe), the debugger will automatically stop on these lines.

:::{.alert .alert-info}
If the debugger is not currently attached, then the `#break` directive is simply ignored.
:::

# Usage

```bash
#break
```

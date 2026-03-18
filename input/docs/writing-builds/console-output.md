Order: 55
Description: How to output content to console
---

# Logging

Cake provides [logging aliases] for writing log messages to the console.

[Logging aliases]: /dsl/logging/

# Advanced output

Cake ships with [Spectre.Console], which provides advanced functionality to output content to the console,
like [progress bars], [spinners], [tables], [trees], [bar charts] or [FIGlet text].

Use the `AnsiConsole` class to render advanced widgets to the console.

See [Spectre.Console] documentation for details.

[Spectre.Console]: https://spectreconsole.net/
[progress bars]: https://spectreconsole.net/live/progress
[spinners]: https://spectreconsole.net/live/status
[tables]: https://spectreconsole.net/widgets/table
[trees]: https://spectreconsole.net/widgets/tree
[bar charts]: https://spectreconsole.net/widgets/barchart
[FIGlet text]: https://spectreconsole.net/widgets/figlet

Cake is built against a specific version of Spectre.Console. If you intend to use Spectre.Console directly in your scripts (e.g., via the #addin directive), you should ensure that you use the same version that Cake uses to avoid assembly versioning conflicts and potential FileLoadException errors.

For more details on why this is necessary, see GitHub issue cake-build/cake#3260.

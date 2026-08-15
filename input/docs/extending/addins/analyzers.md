Order: 40
Title: Available analyzers
Description: Analyzers available for validating Cake addins
---

When writing a Cake addin, it can be useful to have automated checks that verify your addin follows the conventions expected by Cake. Analyzers can catch common mistakes at compile time, before you publish your addin.

## Cake.Addin.Analyzer

[Cake.Addin.Analyzer](https://github.com/WormieCorp/Cake.Addin.Analyzer) is a Roslyn analyzer that checks an addin project against the recommended and required rules every addin is expected to follow. It runs automatically as part of your build once installed, and reports issues directly in your IDE or build output.

Some of the things it checks for include:

- Alias classes being marked with the correct `CakeAliasCategory` attribute.
- Alias methods being marked with either `CakeMethodAlias` or `CakePropertyAlias`, so that Cake can correctly identify and import them.

Many of these rules also provide an automatic code fix, so you can resolve violations directly from your editor's light bulb suggestions.

### Installing

Add the analyzer to your addin project via NuGet:

```powershell
PM> Install-Package Cake.Addin.Analyzer
```

Once installed, the analyzer will run automatically during your build and highlight any violations of the recommended rules.

### Suppressing rules

If a rule doesn't apply to your situation, it can be suppressed either through an `.editorconfig` file, a `<NoWarn>` entry in your `.csproj`, or with a `SuppressMessage` attribute in code. See the [full rule documentation](https://wormiecorp.github.io/Cake.Addin.Analyzer/) for details on each individual rule and how to configure it.
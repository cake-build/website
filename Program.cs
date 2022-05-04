using Statiq.App;
using Statiq.Docs;

await Bootstrapper
    .Factory
    .CreateDocs(args)
    .RunAsync();
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Cake.Core.IO;
using Cake.Web.Core.NuGet;

namespace Cake.Web.Core.Content.Modules
{
    public sealed class ModuleReader : IModuleReader
    {
        private readonly IFileSystem _fileSystem;

        public ModuleReader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public ModuleIndex Read(FilePath path)
        {
            var file = _fileSystem.GetFile(path);
            if (file.Exists)
            {
                using (var stream = file.OpenRead())
                {
                    return Read(stream);
                }
            }
            var message = $"Could not find toc file ({path.FullPath}).";
            throw new FileNotFoundException(message);
        }

        public ModuleIndex Read(Stream stream)
        {
            var result = new List<Module>();

            // Parse document.
            var document = new XmlDocument();
            document.Load(stream);

            // ReadElement all addins.
            var moduleNodes = document.SelectNodes("/Modules/Module");
            if (moduleNodes != null)
            {
                foreach (XmlNode moduleNode in moduleNodes)
                {
                    var module = new Module();

                    ReadElement(moduleNode, "Name", s => module.Name = s);
                    ReadElement(moduleNode, "Repository", s => module.Repository = new Uri(s));
                    ReadElement(moduleNode, "Website", s => module.Website = new Uri(s));
                    ReadElement(moduleNode, "Author", s => module.Author = s);
                    ReadElement(moduleNode, "Description", s => module.Description = s);
                    ReadElement(moduleNode, "Categories", s => module.Categories.AddRange(ParseCategories(s)));

                    var nugetNode = moduleNode.SelectSingleNode("NuGet");
                    if (nugetNode?.Attributes != null)
                    {
                        var id = nugetNode.Attributes["Id"].Value;
                        var version = nugetNode.Attributes["Version"]?.Value;

                        module.PackageDefinition = new PackageDefinition
                        {
                            PackageName = id,
                            Version = version,
                            Metadata = module
                        };

                        var filters = nugetNode.SelectNodes("Filter");
                        if (filters != null)
                        {
                            foreach (XmlNode filterNode in filters)
                            {
                                module.PackageDefinition.Filters.Add(filterNode.InnerText);
                            }
                        }
                    }

                    result.Add(module);
                }
            }

            return new ModuleIndex(result);
        }

        private static void ReadElement(XmlNode parent, string name, Action<string> func)
        {
            var node = parent.SelectSingleNode(name);
            if (node != null)
            {
                func(node.InnerText);
            }
        }

        private static IEnumerable<string> ParseCategories(string categories)
        {
            if (string.IsNullOrWhiteSpace(categories))
            {
                return Enumerable.Empty<string>();
            }
            var parts = categories.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            return parts.Select(x => x.Trim());
        }

    }
}
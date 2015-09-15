using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Cake.Core.IO;
using Cake.Web.Core.NuGet;
using Cake.Web.Docs;

namespace Cake.Web.Core.Content.Addins
{
    public sealed class AddinReader : IAddinReader
    {
        private readonly IFileSystem _fileSystem;

        public AddinReader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public AddinIndex Read(FilePath path)
        {
            var file = _fileSystem.GetFile(path);
            if (file.Exists)
            {
                using (var stream = file.OpenRead())
                {
                    return Read(stream);
                }
            }
            var message = string.Format("Could not find toc file ({0}).", path.FullPath);
            throw new FileNotFoundException(message);
        }

        public AddinIndex Read(Stream stream)
        {
            var result = new List<Addin>();

            // Parse document.
            var document = new XmlDocument();
            document.Load(stream);

            // ReadElement all addins.
            var addinNodes = document.SelectNodes("/Addins/Addin");
            if (addinNodes != null)
            {
                foreach (XmlNode addinNode in addinNodes)
                {
                    var addin = new Addin();

                    ReadElement(addinNode, "Name", s => addin.Name = s);
                    ReadElement(addinNode, "Repository", s => addin.Repository = new Uri(s));
                    ReadElement(addinNode, "Website", s => addin.Website = new Uri(s));
                    ReadElement(addinNode, "Author", s => addin.Author = s);
                    ReadElement(addinNode, "Description", s => addin.Description = s);
                    ReadElement(addinNode, "Categories", s => addin.Categories.AddRange(ParseCategories(s)));

                    var nugetNode = addinNode.SelectSingleNode("NuGet");
                    if (nugetNode != null && nugetNode.Attributes != null)
                    {
                        var id = nugetNode.Attributes["Id"].Value;

                        addin.PackageDefinition = new PackageDefinition();
                        addin.PackageDefinition.PackageName = id;
                        addin.PackageDefinition.Metadata = addin;

                        var filters = nugetNode.SelectNodes("Filter");
                        if (filters != null)
                        {
                            foreach (XmlNode filterNode in filters)
                            {
                                addin.PackageDefinition.Filters.Add(filterNode.InnerText);
                            }
                        }
                    }

                    result.Add(addin);
                }
            }

            return new AddinIndex(result);
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
            var parts = categories.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
            return parts.Select(x => x.Trim());
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Cake.Web.Docs.Reflection;
using Cake.Web.Docs.Reflection.Model;
using Cake.Web.Docs.Xml;
using Cake.Web.Docs.Xml.Model;
using Mono.Cecil;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Responsible for building a document model
    /// given a reflection model and an XML documentation model.
    /// </summary>
    public sealed class DocumentModelBuilder
    {
        /// <summary>
        /// Generates a document model.
        /// </summary>
        /// <param name="paths">The items.</param>
        /// <returns>The built document model.</returns>
        public DocumentModel BuildModel(IDictionary<string, IDocumentationMetadata> paths)
        {
            var reflectionModel = BuildReflectionModel(paths);
            var xmlModel = BuildXmlModel(paths);
            return DocumentModelMapper.Map(reflectionModel, xmlModel);
        }

        private static ReflectionModel BuildReflectionModel(IDictionary<string, IDocumentationMetadata> paths)
        {
            var items = FilterFilesOnExtension(paths, ".dll");
            var definitions = new Dictionary<AssemblyDefinition, IDocumentationMetadata>();

            var resolver = new DefaultAssemblyResolver();
            foreach (var item in items)
            {
                resolver.AddSearchDirectory(Path.GetDirectoryName(item.Key));
            }

            var parameters = new ReaderParameters
            {
                AssemblyResolver = resolver,
            };

            foreach (var item in items)
            {
                var definition = AssemblyDefinition.ReadAssembly(item.Key, parameters);
                definitions.Add(definition, item.Value);
            }

            return ReflectionModelBuilder.Build(definitions);
        }

        private static XmlDocumentationModel BuildXmlModel(IDictionary<string, IDocumentationMetadata> paths)
        {
            var parser = new XmlDocumentationParser();
            var documents = new List<XmlDocument>();
            var xmlPaths = FilterFilesOnExtension(paths, ".xml");
            foreach (var path in xmlPaths)
            {
                using (var stream = File.OpenRead(path.Key))
                {
                    var document = new XmlDocument { PreserveWhitespace = false };
                    document.Load(stream);
                    documents.Add(document);
                }
            }
            return parser.Parse(documents);
        }

        private static IDictionary<string, IDocumentationMetadata> FilterFilesOnExtension(IDictionary<string, IDocumentationMetadata> items, string extension)
        {
            var result = new Dictionary<string, IDocumentationMetadata>();
            var processedFiles = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in items)
            {
                var path = item.Key;
                var pathExtension = Path.GetExtension(path);
                var filename = Path.GetFileName(path);
                if (string.IsNullOrWhiteSpace(pathExtension) || string.IsNullOrWhiteSpace(filename))
                {
                    continue;
                }

                if (!pathExtension.Equals(extension, StringComparison.OrdinalIgnoreCase) || processedFiles.ContainsKey(filename))
                {
                    continue;
                }

                result.Add(item.Key, item.Value);
                processedFiles.Add(filename, true);
            }
            return result;
        }
    }
}

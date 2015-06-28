using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// <param name="paths">The paths.</param>
        /// <returns>The built document model.</returns>
        public DocumentModel BuildModel(IEnumerable<string> paths)
        {
            var filePaths = paths.ToArray();
            var reflectionModel = BuildReflectionModel(filePaths);
            var xmlModel = BuildXmlModel(filePaths);
            return DocumentModelMapper.Map(reflectionModel, xmlModel);
        }

        private static ReflectionModel BuildReflectionModel(IEnumerable<string> paths)
        {
            var assemblyPaths = FilterFilesOnExtension(paths, ".dll").ToArray();
            var definitions = new List<AssemblyDefinition>();

            var resolver = new DefaultAssemblyResolver();
            foreach (var assemblyPath in assemblyPaths)
            {
                resolver.AddSearchDirectory(Path.GetDirectoryName(assemblyPath));
            }
            var parameters = new ReaderParameters {
                AssemblyResolver = resolver,
            };

            foreach (var assemblyPath in assemblyPaths)
            {
                definitions.Add(AssemblyDefinition.ReadAssembly(assemblyPath, parameters));
            }
            return ReflectionModelBuilder.Build(definitions);
        }

        private static XmlDocumentationModel BuildXmlModel(IEnumerable<string> paths)
        {
            var parser = new XmlDocumentationParser();
            var documents = new List<XmlDocument>();
            var xmlPaths = FilterFilesOnExtension(paths, ".xml").ToArray();
            foreach (var path in xmlPaths)
            {
                using (var stream = File.OpenRead(path))
                {
                    var document = new XmlDocument { PreserveWhitespace = false };
                    document.Load(stream);
                    documents.Add(document);
                }
            }
            return parser.Parse(documents);
        }

        private static IEnumerable<string> FilterFilesOnExtension(IEnumerable<string> paths, string extension)
        {
            foreach (var path in paths)
            {
                var pathExtension = Path.GetExtension(path);
                if (!string.IsNullOrWhiteSpace(pathExtension))
                {
                    if (pathExtension.Equals(extension, StringComparison.OrdinalIgnoreCase))
                    {
                        yield return path;
                    }
                }
            }
        }
    }
}

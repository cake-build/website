using System.Collections.Generic;
using System.Linq;
using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Reflection.Model;
using Cake.Web.Docs.Xml.Model;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Responsible for mapping a reflection and a xml documentation model.
    /// </summary>
    internal static class DocumentModelMapper
    {
        /// <summary>
        /// Generates a document model from a <see cref="ReflectionModel"/>
        /// and a <see cref="XmlDocumentationModel"/>.
        /// </summary>
        /// <param name="reflectionModel">The reflection model.</param>
        /// <param name="xmlModel">The XML documentation model.</param>
        /// <returns>A document model.</returns>
        public static DocumentModel Map(ReflectionModel reflectionModel, XmlDocumentationModel xmlModel)
        {
            var assemblies = new List<DocumentedAssembly>();

            // Iterate all assemblies.
            foreach (var assembly in reflectionModel.Assemblies)
            {
                assemblies.Add(MapAssembly(assembly, xmlModel));
            }

            // Create the document model.
            var model = new DocumentModel(assemblies);

            // Map extension methods.
            var finder = new ExtensionMethodFinder(model);
            var namespaces = model.Assemblies.SelectMany(a => a.Namespaces).ToArray();
            foreach (var @namespace in namespaces)
            {
                foreach (var type in @namespace.Types)
                {
                    type.SetExtensionMethods(finder.FindExtensionMethods(type));
                }
            }

            // Build namespace trees and map them.
            var trees = DocumentedNamespaceTree.Build(namespaces);
            foreach (var @namespace in namespaces)
            {
                if (trees.ContainsKey(@namespace.Identity))
                {
                    @namespace.Tree = trees[@namespace.Identity];
                }
            }

            return model;
        }

        private static DocumentedAssembly MapAssembly(IAssemblyInfo assembly, XmlDocumentationModel model)
        {
            var types = new List<DocumentedType>();

            // Iterate all types in assembly.
            foreach (var type in assembly.Types)
            {
                var documentedType = MapType(type, model);
                types.Add(documentedType);

                // Add a reference to the type for every constructor.
                foreach (var constructor in documentedType.Constructors)
                {
                    constructor.Type = documentedType;
                }

                // Add a reference to the type for every method.
                foreach (var method in documentedType.Methods)
                {
                    method.Type = documentedType;
                }

                // Add a reference to the type for every method.
                foreach (var @operator in documentedType.Operators)
                {
                    @operator.Type = documentedType;
                }

                // Add a reference to the type for every property.
                foreach (var property in documentedType.Properties)
                {
                    property.Type = documentedType;
                }

                // Add a reference to the type for every method.
                foreach (var field in documentedType.Fields)
                {
                    field.Type = documentedType;
                }
            }

            // Now group all the types in this assembly by their namespace.
            var namespaces = new List<DocumentedNamespace>();
            var namespaceGroups = types.GroupBy(x => x.Definition.Namespace);
            foreach (var namespaceGroup in namespaceGroups)
            {
                var namespaceTypes = namespaceGroup.ToList();

                // Do we have a documentation for this namespace?
                var documentation = namespaceGroup.FirstOrDefault(x => x.Definition.Name.EndsWith("NamespaceDoc"));
                var summary = documentation != null ? documentation.Summary : null;
                if (documentation != null)
                {
                    namespaceTypes.Remove(documentation);
                }

                // Create a namespace for each grouping.
                var @namespace = new DocumentedNamespace(namespaceGroup.Key, namespaceGroup.Key, namespaceTypes, summary);
                namespaces.Add(@namespace);

                // Connect the types in this namespace to the namespace.
                foreach (var documentedType in namespaceGroup)
                {
                    documentedType.Namespace = @namespace;
                }
            }

            // Create an documented assembly out of it.
            var documentedAssembly = new DocumentedAssembly(assembly, namespaces);

            // Add the documented assembly as a parent of all namespaces.
            foreach (var @namespace in namespaces)
            {
                @namespace.Assembly = documentedAssembly;
            }

            // Return the documented assembly.
            return documentedAssembly;
        }

        private static DocumentedType MapType(ITypeInfo type, XmlDocumentationModel xmlModel)
        {
            SummaryComment summary = null;
            RemarksComment remarks = null;
            ExampleComment example = null;

            // Get the documentation for the type.
            var member = xmlModel.Find(type.Identity);
            if (member != null)
            {
                // Get the comments for the type.
                summary = member.Comments.OfType<SummaryComment>().SingleOrDefault();
                remarks = member.Comments.OfType<RemarksComment>().SingleOrDefault();
                example = member.Comments.OfType<ExampleComment>().SingleOrDefault();
            }

            // Map the methods.
            var methods = new List<DocumentedMethod>();
            foreach (var method in type.Methods)
            {
                methods.Add(MapMethod(method, xmlModel));
            }

            // Map the properties.
            var properties = new List<DocumentedProperty>();
            foreach (var property in type.Properties)
            {
                properties.Add(MapProperty(property, xmlModel));
            }

            // Map the fields.
            var fields = new List<DocumentedField>();
            foreach (var field in type.Fields)
            {
                fields.Add(MapField(field, xmlModel));
            }

            // Return the documented type.
            return new DocumentedType(type, properties, methods, fields, summary, remarks, example);
        }

        private static DocumentedMethod MapMethod(IMethodInfo method, XmlDocumentationModel xmlModel)
        {
            var parameters = new List<DocumentedParameter>();

            SummaryComment summary = null;
            RemarksComment remarks = null;
            ExampleComment example = null;
            ReturnsComment returns = null;

            // Get the documentation for the type.
            var member = xmlModel.Find(method.Identity);
            if (member != null)
            {
                // Get the comments for the type.
                summary = member.Comments.OfType<SummaryComment>().FirstOrDefault();
                remarks = member.Comments.OfType<RemarksComment>().FirstOrDefault();
                example = member.Comments.OfType<ExampleComment>().FirstOrDefault();
                returns = member.Comments.OfType<ReturnsComment>().FirstOrDefault();
            }

            // Map parameters.
            foreach (var parameterDefinition in method.Definition.Parameters.ToList())
            {
                ParamComment comment = null;
                if (member != null)
                {
                    // Try to get the comment for the current parameter.
                    comment = member.Comments.OfType<ParamComment>().FirstOrDefault(x => x.Name == parameterDefinition.Name);
                }

                var parameter = new DocumentedParameter(parameterDefinition, comment);
                parameters.Add(parameter);
            }

            return new DocumentedMethod(method, parameters, summary, remarks, example, returns);
        }

        private static DocumentedProperty MapProperty(IPropertyInfo property, XmlDocumentationModel xmlModel)
        {
            SummaryComment summary = null;
            RemarksComment remarks = null;
            ExampleComment example = null;
            ValueComment value = null;

            // Get the documentation for the type.
            var member = xmlModel.Find(property.Identity);
            if (member != null)
            {
                // Get the comments for the type.
                summary = member.Comments.OfType<SummaryComment>().SingleOrDefault();
                remarks = member.Comments.OfType<RemarksComment>().SingleOrDefault();
                example = member.Comments.OfType<ExampleComment>().SingleOrDefault();
                value = member.Comments.OfType<ValueComment>().SingleOrDefault();
            }

            return new DocumentedProperty(property, summary, remarks, example, value);
        }

        private static DocumentedField MapField(IFieldInfo field, XmlDocumentationModel xmlModel)
        {
            SummaryComment summary = null;
            RemarksComment remarks = null;
            ExampleComment example = null;

            // Get the documentation for the type.
            var member = xmlModel.Find(field.Identity);
            if (member != null)
            {
                // Get the comments for the type.
                summary = member.Comments.OfType<SummaryComment>().SingleOrDefault();
                remarks = member.Comments.OfType<RemarksComment>().SingleOrDefault();
                example = member.Comments.OfType<ExampleComment>().SingleOrDefault();
            }

            return new DocumentedField(field, summary, remarks, example);
        }
    }
}

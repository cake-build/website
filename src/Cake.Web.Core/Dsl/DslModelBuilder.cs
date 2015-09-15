using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Cake.Web.Docs;
using Cake.Web.Docs.Comments;
using Mono.Cecil;

namespace Cake.Web.Core.Dsl
{
    public static class DslModelBuilder
    {
        public static DslModel Build(DocumentModel model)
        {
            Dictionary<string, SummaryComment> summaries;
            var data = GetData(model, out summaries);

            var categories = new List<DslCategory>();
            foreach (var categoryName in data.Keys)
            {
                var metadata = (IDocumentationMetadata)null;

                var categoryMethods = new List<DocumentedMethod>();
                var subCategories = new List<DslSubCategory>();
                foreach (var subCategoryName in data[categoryName].Keys)
                {
                    var methods = data[categoryName][subCategoryName];
                    if (string.IsNullOrWhiteSpace(subCategoryName))
                    {
                        categoryMethods.AddRange(methods.OrderBy(x => x.Identity));
                    }
                    else
                    {
                        subCategories.Add(new DslSubCategory(subCategoryName, methods));
                    }

                    if (metadata == null)
                    {
                        metadata = methods.First().Metadata;
                    }
                }

                SummaryComment summary = null;
                if (summaries.ContainsKey(categoryName))
                {
                    summary = summaries[categoryName];
                }

                categories.Add(
                    new DslCategory(
                        categoryName,
                        metadata,
                        summary,
                        categoryMethods, 
                        subCategories.OrderBy(x => x.Name)));
            }

            var dslModel = new DslModel(categories.OrderBy(x => x.Name));
            foreach (var category in dslModel.Categories)
            {
                category.Parent = dslModel;
            }

            return dslModel;
        }

        private static Dictionary<string, Dictionary<string, List<DocumentedMethod>>> GetData(DocumentModel model, out Dictionary<string, SummaryComment> summaries)
        {
            var result = new Dictionary<string, Dictionary<string, List<DocumentedMethod>>>();

            summaries = new Dictionary<string, SummaryComment>();
            summaries.Add("General", new SummaryComment(new[] { new InlineTextComment("Contains miscellaneous functionality.") }));
            
            foreach (var assembly in model.Assemblies)
            {
                foreach (var ns in assembly.Namespaces)
                {
                    foreach (var type in ns.Types)
                    {
                        var typeCategory = GetCategory(type.Definition.CustomAttributes);
                        var parentCategory = typeCategory ?? "General";

                        var categorySummary = type.Summary;
                        if (!summaries.ContainsKey(parentCategory) && categorySummary != null)
                        {
                            summaries.Add(parentCategory, categorySummary);
                        }

                        foreach (var method in type.Methods)
                        {
                            bool isPropertyAlias;
                            if (IsCakeAlias(method, out isPropertyAlias))
                            {
                                var methodCategory = GetCategory(method.Definition.CustomAttributes);
                                var category = methodCategory ?? string.Empty;

                                if (!result.ContainsKey(parentCategory))
                                {
                                    result.Add(parentCategory, new Dictionary<string, List<DocumentedMethod>>());
                                }
                                if (!result[parentCategory].ContainsKey(category))
                                {
                                    result[parentCategory].Add(category, new List<DocumentedMethod>());
                                }

                                result[parentCategory][category].Add(method);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static string GetCategory(IEnumerable<CustomAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                if (attribute.AttributeType != null &&
                    attribute.AttributeType.FullName == "Cake.Core.Annotations.CakeAliasCategoryAttribute")
                {
                    return attribute.ConstructorArguments[0].Value as string;
                }
            }
            return null;
        }

        private static bool IsCakeAlias(DocumentedMember member, out bool isPropertyAlias)
        {
            var method = member as DocumentedMethod;
            if (method != null)
            {
                if (IsCakeAlias(method.Definition, out isPropertyAlias))
                {
                    return true;
                }
            }
            isPropertyAlias = false;
            return false;
        }

        private static bool IsCakeAlias(MethodDefinition method, out bool isPropertyAlias)
        {
            foreach (var attribute in method.CustomAttributes)
            {
                if (attribute.AttributeType != null && (
                    attribute.AttributeType.FullName == "Cake.Core.Annotations.CakeMethodAliasAttribute" ||
                    attribute.AttributeType.FullName == "Cake.Core.Annotations.CakePropertyAliasAttribute"))
                {
                    isPropertyAlias = attribute.AttributeType.FullName == "Cake.Core.Annotations.CakePropertyAliasAttribute";
                    return true;
                }
            }
            isPropertyAlias = false;
            return false;
        }
    }
}

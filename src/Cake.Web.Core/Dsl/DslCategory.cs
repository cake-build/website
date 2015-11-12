using System.Collections.Generic;
using System.Diagnostics;
using Cake.Web.Docs;
using Cake.Web.Docs.Comments;

namespace Cake.Web.Core.Dsl
{
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public sealed class DslCategory
    {
        public string Name { get; }
        public IDocumentationMetadata Metadata { get; }
        public SummaryComment Summary { get; }
        public string Slug { get; }
        public IReadOnlyList<DocumentedMethod> Methods { get; }
        public IReadOnlyList<DslSubCategory> SubCategories { get; }

        public DslModel Parent { get; internal set; }

        public DslCategory(
            string name,
            IDocumentationMetadata metadata,
            SummaryComment summary,
            IEnumerable<DocumentedMethod> methods,
            IEnumerable<DslSubCategory> categories)
        {
            Name = name;
            Metadata = metadata;
            Summary = summary;
            Slug = Name.ToSlug();
            Methods = new List<DocumentedMethod>(methods);
            SubCategories = new List<DslSubCategory>(categories);
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private string DebuggerDisplay()
        {
            return string.Concat("Category: ", Name ?? "[Unknown]");
        }
    }
}
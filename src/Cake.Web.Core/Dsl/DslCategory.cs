using System.Collections.Generic;
using System.Diagnostics;
using Cake.Web.Docs;
using Cake.Web.Docs.Comments;

namespace Cake.Web.Core.Dsl
{
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public sealed class DslCategory
    {
        private readonly string _name;
        private readonly SummaryComment _summary;
        private readonly string _slug;
        private readonly List<DocumentedMethod> _methods;
        private readonly List<DslSubCategory> _subCategories;

        public string Name
        {
            get { return _name; }
        }

        public SummaryComment Summary
        {
            get { return _summary; }
        }

        public string Slug
        {
            get { return _slug; }
        }

        public IReadOnlyList<DocumentedMethod> Methods
        {
            get { return _methods; }
        }

        public IReadOnlyList<DslSubCategory> SubCategories
        {
            get { return _subCategories; }
        }

        public DslModel Parent { get; internal set; }

        public DslCategory(
            string name,
            SummaryComment summary,
            IEnumerable<DocumentedMethod> methods,
            IEnumerable<DslSubCategory> categories)
        {
            _name = name;
            _summary = summary;
            _slug = _name.ToSlug();
            _methods = new List<DocumentedMethod>(methods);
            _subCategories = new List<DslSubCategory>(categories);
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private string DebuggerDisplay()
        {
            return string.Concat("Category: ", Name ?? "[Unknown]");
        }
    }
}
using System.Collections.Generic;
using System.Diagnostics;
using Cake.Web.Docs;

namespace Cake.Web.Core.Dsl
{
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public sealed class DslSubCategory
    {
        public string Name { get; }
        public IReadOnlyList<DocumentedMethod> Methods { get; }

        public DslSubCategory(string name, IEnumerable<DocumentedMethod> methods)
        {
            Name = name;
            Methods = new List<DocumentedMethod>(methods);
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private string DebuggerDisplay()
        {
            return string.Concat("Sub category: ", Name ?? "[Unknown]");
        }
    }
}
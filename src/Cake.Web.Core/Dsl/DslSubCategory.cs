using System.Collections.Generic;
using System.Diagnostics;
using Cake.Web.Docs;

namespace Cake.Web.Core.Dsl
{
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public sealed class DslSubCategory
    {
        private readonly string _name;
        private readonly List<DocumentedMethod> _methods;

        public string Name
        {
            get { return _name; }
        }

        public IReadOnlyList<DocumentedMethod> Methods
        {
            get { return _methods; }
        }

        public DslSubCategory(string name, IEnumerable<DocumentedMethod> methods)
        {
            _name = name;
            _methods = new List<DocumentedMethod>(methods);
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private string DebuggerDisplay()
        {
            return string.Concat("Sub category: ", Name ?? "[Unknown]");
        }
    }
}
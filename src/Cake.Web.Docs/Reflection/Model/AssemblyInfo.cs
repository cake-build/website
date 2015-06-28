using System.Collections.Generic;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    internal sealed class AssemblyInfo : IAssemblyInfo
    {
        private readonly AssemblyDefinition _definition;
        private readonly string _identity;
        private readonly string _name;
        private readonly List<ITypeInfo> _types;

        public string Identity
        {
            get { return _identity; }
        }

        public AssemblyDefinition Definition
        {
            get { return _definition; }
        }

        public IReadOnlyList<ITypeInfo> Types
        {
            get { return _types; }
        }

        public string Name
        {
            get { return _name; }
        }

        public AssemblyInfo(AssemblyDefinition definition, IEnumerable<ITypeInfo> types)
        {
            _definition = definition;
            _name = definition.Name.Name;
            _identity = definition.FullName;
            _types = new List<ITypeInfo>(types);
        }
    }
}

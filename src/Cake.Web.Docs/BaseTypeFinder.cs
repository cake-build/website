using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Web.Docs
{
    internal sealed class BaseTypeFinder
    {
        private readonly Dictionary<string, DocumentedType> _lookup;

        public BaseTypeFinder(DocumentModel model)
        {
            _lookup = new Dictionary<string, DocumentedType>(StringComparer.Ordinal);

            foreach (var type in model.Assemblies
                .SelectMany(assembly => assembly.Namespaces
                .SelectMany(@namespace => @namespace.Types)))
            {
                _lookup.Add(type.Definition.FullName, type);
            }
        }

        public DocumentedType FindBaseType(DocumentedType type)
        {
            var baseType = type.Definition.BaseType;
            if (baseType != null)
            {
                if (baseType.FullName != "System.Object" && baseType.FullName != "System.ValueType")
                {
                    DocumentedType value = null;
                    _lookup.TryGetValue(baseType.FullName, out value);
                    return value;
                }
            }
            return null;
        }
    }
}

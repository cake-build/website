using System.Collections.Generic;
using System.Text;
using Mono.Cecil;

namespace Cake.Web.Docs.Identity
{
    /// <summary>
    /// Responsible for generating cref identities.
    /// </summary>
    public static class CRefGenerator
    {
        /// <summary>
        /// Gets the type's identity.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type's identity.</returns>
        public static string GetTypeCRef(TypeReference type)
        {
            var name = type.FullName;
            var index = name.IndexOf('<');
            if (index != -1)
            {
                name = name.Substring(0, index);
            }
            name = string.Concat("T:", name);
            name = name.Replace("&", string.Empty);
            return name;
        }

        /// <summary>
        /// Gets the method's identity.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The method's identity.</returns>
        public static string GetMethodCRef(MethodDefinition method)
        {
            var builder = new StringBuilder();

            builder.Append("M:");
            builder.Append(method.DeclaringType.FullName);
            builder.Append(".");

            builder.Append(method.IsConstructor ? "#ctor" : method.Name);

            if (method.HasGenericParameters)
            {
                builder.Append("``");
                builder.Append(method.GenericParameters.Count);
            }

            if (method.HasParameters)
            {
                builder.Append("(");
                var result = new List<string>();
                foreach (var parameter in method.Parameters)
                {
                    if (parameter.ParameterType.IsGenericParameter)
                    {
                        result.Add(CRefHelpers.GetGenericParameterName(method, parameter.ParameterType));
                    }
                    else
                    {
                        var parameterTypeName = CRefHelpers.GetParameterTypeName(method, parameter.ParameterType);
                        if (parameter.IsOut || parameter.ParameterType.IsByReference)
                        {
                            parameterTypeName = parameterTypeName.TrimEnd('&');
                            parameterTypeName = string.Concat(parameterTypeName, "@");
                        }
                        result.Add(parameterTypeName);
                    }
                }
                builder.Append(string.Join(",", result));
                builder.Append(")");
            }

            if (method.IsSpecialName)
            {
                if (method.Name == "op_Implicit" || method.Name == "op_Explicit")
                {
                    builder.Append("~");
                    builder.Append(method.ReturnType.FullName);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets the property's identity.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The property's identity.</returns>
        public static string GetPropertyCRef(PropertyDefinition property)
        {
            var builder = new StringBuilder();
            builder.Append("P:");
            builder.Append(property.DeclaringType.FullName);
            builder.Append(".");
            builder.Append(property.Name);
            return builder.ToString();
        }

        /// <summary>
        /// Gets the field's identity.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The field's identity.</returns>
        public static string GetFieldCRef(FieldDefinition field)
        {
            var builder = new StringBuilder();
            builder.Append("F:");
            builder.Append(field.DeclaringType.FullName);
            builder.Append(".");
            builder.Append(field.Name);
            return builder.ToString();
        }
    }
}

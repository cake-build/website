using System;
using System.Collections.Generic;
using System.Text;
using Mono.Cecil;

namespace Cake.Web.Docs.Identity
{
    internal static class CRefHelpers
    {
        public static string GetParameterTypeName(MethodDefinition method, TypeReference parameterType)
        {
            var genericParameterType = parameterType as GenericInstanceType;
            if (genericParameterType != null)
            {
                // Generic parameter type, i.e. IEnumerable<int>
                return GetGenericParameterTypeName(method, genericParameterType);
            }

            return parameterType.IsGenericParameter
                ? GetGenericParameterName(method, parameterType)
                : parameterType.FullName;
        }

        public static string GetGenericParameterTypeName(MethodDefinition method, GenericInstanceType parameterType)
        {
            var builder = new StringBuilder();
            builder.Append(GetFullNameWithoutGenericTypeReference(parameterType));
            builder.Append("{");

            var genericArguments = new List<string>();
            foreach (var arg in parameterType.GenericArguments)
            {
                genericArguments.Add(GetParameterTypeName(method, arg));
            }
            builder.Append(string.Join(",", genericArguments));

            builder.Append("}");
            return builder.ToString();
        }

        public static bool ContainsGenericParameter(IGenericParameterProvider provider, TypeReference parameter)
        {
            foreach (var param in provider.GenericParameters)
            {
                if (param.MetadataToken == parameter.MetadataToken)
                {
                    return true;
                }
            }
            return false;
        }

        public static int GetIndexOfGenericParameter(MethodDefinition method, GenericParameter parameter)
        {
            if (parameter.IsGenericParameter)
            {
                // Genetic parameter has origin on method?
                for (var index = 0; index < method.GenericParameters.Count; index++)
                {
                    if (method.GenericParameters[index].MetadataToken == parameter.MetadataToken)
                    {
                        return index;
                    }
                }

                // Genetic parameter has origin on declared type?
                for (var index = 0; index < method.DeclaringType.GenericParameters.Count; index++)
                {
                    if (method.DeclaringType.GenericParameters[index].MetadataToken ==
                        parameter.MetadataToken)
                    {
                        return index;
                    }
                }
            }

            throw new InvalidOperationException("Could not find generic parameter.");
        }

        public static string GetGenericParameterName(MethodDefinition method, TypeReference parameter)
        {
            var parameterIndex = GetIndexOfGenericParameter(method, (GenericParameter)parameter);
            return string.Concat(GetPrefixForGenericParameter(method, parameter), parameterIndex);
        }

        public static string GetFullNameWithoutGenericTypeReference(TypeReference type)
        {
            var name = type.FullName;
            var index = name.IndexOf('`');
            if (index != -1)
            {
                name = name.Substring(0, index);
            }
            return name;
        }

        private static string GetPrefixForGenericParameter(MethodDefinition method, TypeReference parameter)
        {
            // Generic parameter originates from type?
            if (ContainsGenericParameter(method.DeclaringType, parameter))
            {
                return "`";
            }

            // Generic parameter originates from method?
            if (ContainsGenericParameter(method, parameter))
            {
                return "``";
            }

            throw new InvalidOperationException("Neither method or declaring type contains generic parameter.");
        }
    }
}

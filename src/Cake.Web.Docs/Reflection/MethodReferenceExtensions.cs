using System.Collections.Generic;
using System.Linq;
using Cake.Web.Docs.Identity;
using Cake.Web.Docs.Reflection.Signatures;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection
{
    /// <summary>
    /// Contains extensions for <see cref="MethodDefinition"/>.
    /// </summary>
    public static class MethodReferenceExtensions
    {
        /// <summary>
        /// Gets the method signature.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="resolver">The link resolver.</param>
        /// <returns>The method signature.</returns>
        public static MethodSignature GetMethodSignature(this MethodReference method, IUrlResolver resolver)
        {
            // Get the method definition.
            var definition = method.Resolve();

            // Get the method Identity and name.
            var identity = CRefGenerator.GetMethodCRef(definition);
            var name = GetMethodName(definition);

            // Get the declaring type and return type.
            var declaringType = definition.DeclaringType.GetTypeSignature(resolver);
            var returnType = definition.ReturnType.GetTypeSignature(resolver);

            // Get generic parameters and arguments.
            var genericParameters = new List<string>();
            if (method.HasGenericParameters)
            {
                // Generic parameters
                genericParameters.AddRange(
                    method.GenericParameters.Select(
                        genericParameter => genericParameter.Name));
            }

            // Get all parameters.
            var parameters = definition.Parameters.Select(
                parameterDefinition => new ParameterSignature(
                    parameterDefinition.Name,
                    parameterDefinition.ParameterType.GetTypeSignature(resolver),
                    parameterDefinition.IsOut,
                    parameterDefinition.ParameterType is ByReferenceType))
                .ToList();

            // Get the classifications.
            var classification = MethodClassifier.GetMethodClassification(definition);
            var operatorClassification = MethodClassifier.GetOperatorClassification(definition);

            // Return the method signature.
            var url = resolver == null ? string.Empty : resolver.GetUrl(identity);
            return new MethodSignature(identity, name, url, classification, operatorClassification, 
                declaringType, returnType, genericParameters, parameters);
        }

        /// <summary>
        /// Determines whether this method is protected.
        /// </summary>
        /// <param name="definition">The method.</param>
        /// <returns>Whether this method is protected.</returns>
        public static bool IsProtected(this MethodDefinition definition)
        {
            return definition.IsFamily;
        }

        /// <summary>
        /// Determines whether this method is protected or internal.
        /// </summary>
        /// <param name="definition">The method.</param>
        /// <returns>Whether this method is protected or internal.</returns>
        public static bool IsProtectedInternal(this MethodDefinition definition)
        {
            return definition.IsFamilyOrAssembly;
        }

        /// <summary>
        /// Determines whether this method is internal.
        /// </summary>
        /// <param name="definition">The method.</param>
        /// <returns>Whether this method is internal.</returns>
        public static bool IsInternal(this MethodDefinition definition)
        {
            return definition.IsFamilyAndAssembly;
        }

        /// <summary>
        /// Determines whether this method is an extension method.
        /// </summary>
        /// <param name="method">The method definition.</param>
        /// <returns>Whether this method is an extension method.</returns>
        public static bool IsExtensionMethod(this MethodDefinition method)
        {
            if (method.IsStatic)
            {
                foreach (var attribute in method.CustomAttributes)
                {
                    if (attribute.AttributeType != null &&
                        attribute.AttributeType.FullName == "System.Runtime.CompilerServices.ExtensionAttribute")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static string GetMethodName(MethodDefinition definition)
        {
            if (definition.IsConstructor)
            {
                var name = definition.DeclaringType.Name;
                var index = name.IndexOf('`');
                if (index != -1)
                {
                    name = name.Substring(0, index);
                }
                return name;
            }
            return definition.Name;
        }
    }
}

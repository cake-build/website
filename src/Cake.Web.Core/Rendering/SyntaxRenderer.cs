using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Cake.Web.Core.Services;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection;
using Cake.Web.Docs.Reflection.Signatures;
using Mono.Cecil;

namespace Cake.Web.Core.Rendering
{
    /// <summary>
    /// Responsible for rendering C# syntax.
    /// </summary>
    public sealed class SyntaxRenderer
    {
        private readonly DocumentModelResolver _resolver;
        private readonly SignatureCache _signatureResolver;
        private readonly SignatureRenderer _renderer;
        private readonly UrlResolver _urlResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyntaxRenderer"/> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <param name="signatureResolver">The signature service.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="urlResolver">The URL resolver.</param>
        public SyntaxRenderer(
            DocumentModelResolver resolver,
            SignatureCache signatureResolver,
            SignatureRenderer renderer,
            UrlResolver urlResolver)
        {
            _resolver = resolver;
            _signatureResolver = signatureResolver;
            _renderer = renderer;
            _urlResolver = urlResolver;
        }

        public IHtmlString Render(TypeSignature signature)
        {
            var writer = new HtmlTextWriter(new StringWriter());
            Render(writer, signature);
            return MvcHtmlString.Create(writer.InnerWriter.ToString());
        }

        public IHtmlString Render(MethodSignature signature)
        {
            var writer = new HtmlTextWriter(new StringWriter());
            Render(writer, signature);
            return MvcHtmlString.Create(writer.InnerWriter.ToString());
        }

        /// <summary>
        /// Renders the syntax for a type.
        /// </summary>
        /// <param name="writer">The text writer.</param>
        /// <param name="signature">The type signature.</param>
        public void Render(HtmlTextWriter writer, TypeSignature signature)
        {
            var type = _resolver.FindType(signature.Identity);
            if (type == null)
            {
                throw new InvalidOperationException("Could not find type.");
            }

            if (type.Definition.IsPublic)
            {
                writer.WriteEncodedText("public");
            }

            if (!type.Definition.IsEnum)
            {
                if (type.Definition.IsAbstract && type.Definition.IsSealed)
                {
                    // Static
                    writer.WriteEncodedText(" ");
                    writer.WriteEncodedText("static");
                }
                else
                {
                    if (type.TypeClassification != TypeClassification.Interface)
                    {
                        if (type.Definition.IsAbstract)
                        {
                            // Abstract
                            writer.WriteEncodedText(" ");
                            writer.WriteEncodedText("abstract");
                        }
                        else if (type.Definition.IsSealed)
                        {
                            // Sealed
                            writer.WriteEncodedText(" ");
                            writer.WriteEncodedText("sealed");
                        }
                    }
                }
            }

            // Type classification
            writer.WriteEncodedText(" ");
            writer.WriteEncodedText(type.TypeClassification.ToString().ToLowerInvariant());

            // Type name
            writer.WriteEncodedText(" ");
            var typeSignature = _signatureResolver.GetTypeSignature(type);
            _renderer.Render(writer, typeSignature, TypeRenderOption.Name);

            if (!type.Definition.IsEnum)
            {
                // Base classes (and interfaces).
                var baseClasses = GetBaseTypes(type);
                if (baseClasses.Count > 0)
                {
                    writer.WriteEncodedText(" : ");
                    writer.Write(string.Join(", ", baseClasses));
                }

                // Generic parameter constraints.
                var constraints = GetGenericParameterConstraints(type);
                if (constraints.Count > 0)
                {
                    foreach (var constraint in constraints)
                    {
                        writer.WriteLine();
                        writer.WriteEncodedText("   where " + constraint.Key + " : ");
                        writer.Write(string.Join(", ", constraint.Value));
                    }
                }
            }
        }

        private List<IHtmlString> GetBaseTypes(DocumentedType type)
        {
            var result = new List<IHtmlString>();
            var baseType = type.Definition.BaseType;
            if (baseType != null)
            {
                if (baseType.FullName != "System.Object" && baseType.FullName != "System.ValueType")
                {
                    result.Add(_renderer.Render(baseType.GetTypeSignature(_urlResolver),
                        TypeRenderOption.Name | TypeRenderOption.Link));
                }
                foreach (var @interface in type.Definition.Interfaces)
                {
                    result.Add(_renderer.Render(@interface.InterfaceType.GetTypeSignature(_urlResolver),
                        TypeRenderOption.Name | TypeRenderOption.Link));
                }
            }
            return result;
        }

        private Dictionary<string, List<IHtmlString>> GetGenericParameterConstraints(DocumentedType type)
        {
            var parameters = type.Definition.GenericParameters.Where(p =>
                p.HasConstraints || p.HasReferenceTypeConstraint ||
                p.HasDefaultConstructorConstraint || p.HasNotNullableValueTypeConstraint).ToArray();

            var result = new Dictionary<string, List<IHtmlString>>();
            if (parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    var constraints = new List<IHtmlString>();
                    if (parameter.HasNotNullableValueTypeConstraint)
                    {
                        constraints.Add(MvcHtmlString.Create("struct"));
                    }
                    if (parameter.HasReferenceTypeConstraint)
                    {
                        constraints.Add(MvcHtmlString.Create("class"));
                    }
                    foreach (var constraint in parameter.Constraints)
                    {
                        if (constraint.FullName == "System.ValueType")
                        {
                            continue;
                        }
                        constraints.Add(_renderer.Render(constraint.GetTypeSignature(_urlResolver),
                            TypeRenderOption.Name | TypeRenderOption.Link));
                    }
                    if (parameter.HasDefaultConstructorConstraint && !parameter.HasNotNullableValueTypeConstraint)
                    {
                        constraints.Add(MvcHtmlString.Create("new()"));
                    }
                    result.Add(parameter.FullName, constraints);
                }
            }
            return result;
        }

        /// <summary>
        /// Renders the syntax for a method.
        /// </summary>
        /// <param name="writer">The text writer.</param>
        /// <param name="signature">The method signature.</param>
        public void Render(HtmlTextWriter writer, MethodSignature signature)
        {
            var method = _resolver.FindMethod(signature.Identity);
            if (method == null)
            {
                throw new InvalidOperationException("Could not find method.");
            }

            if (method.Definition.IsPublic)
            {
                writer.WriteEncodedText("public");
            }
            else if (method.Definition.IsFamily)
            {
                writer.WriteEncodedText("protected");
            }
            else if (method.Definition.IsFamilyOrAssembly)
            {
                writer.WriteEncodedText("protected internal");
            }

            if (method.Definition.IsAbstract)
            {
                writer.WriteEncodedText(" ");
                writer.WriteEncodedText("abstract");
            }

            if (method.Definition.IsStatic)
            {
                writer.WriteEncodedText(" ");
                writer.WriteEncodedText("static");
            }

            if (method.Definition.IsVirtual)
            {
                writer.WriteEncodedText(" ");
                writer.WriteEncodedText("virtual");
            }

            var isImplicitOperator = signature.OperatorClassification == OperatorClassification.Implicit;
            var isExplicitOperator = signature.OperatorClassification == OperatorClassification.Explicit;
            var isImplicitOrExplicitOperator = isImplicitOperator || isExplicitOperator;

            // Return type
            if (!method.Definition.IsConstructor && !isImplicitOrExplicitOperator)
            {
                var returnType = method.Definition.ReturnType.GetTypeSignature(_urlResolver);
                writer.WriteEncodedText(" ");
                _renderer.Render(writer, returnType, TypeRenderOption.Name | TypeRenderOption.Link);
            }

            // Name
            if (signature.Classification == MethodClassification.Operator)
            {
                if (isImplicitOperator)
                {
                    writer.WriteEncodedText(" implicit");
                }
                else if (isExplicitOperator)
                {
                    writer.WriteEncodedText(" explicit");
                }

                writer.WriteEncodedText(" operator ");
                if (isImplicitOrExplicitOperator)
                {
                    // Implicit or explicit operator
                    var returnType = method.Definition.ReturnType.GetTypeSignature(_urlResolver);
                    _renderer.Render(writer, returnType, TypeRenderOption.Name | TypeRenderOption.Link);
                }
                else
                {
                    // Operator name.
                    writer.WriteEncodedText(signature.OperatorClassification.GetOperatorSymbol() ?? "[unknown_symbol]");
                }
            }
            else
            {
                writer.WriteEncodedText(" ");
                writer.WriteEncodedText(signature.Name);
            }

            // Parameters
            var isPropertyAlias = method.Metadata.IsPropertyAlias;
            if (!isPropertyAlias && method.Parameters.Count > 0)
            {
                writer.WriteEncodedText("(");
                writer.WriteLine();

                var index = 0;
                foreach (var parameter in method.Parameters)
                {
                    if(index == 0 && method.Metadata.IsAlias)
                    {
                        index++;
                        continue;
                    }

                    writer.WriteEncodedText("       ");

                    if (index == 0)
                    {
                        if (method.Definition.IsExtensionMethod())
                        {
                            writer.WriteEncodedText("this ");
                        }
                    }

                    if (parameter.Definition.IsOut)
                    {
                        writer.WriteEncodedText("out ");
                    }
                    else if (parameter.Definition.ParameterType is ByReferenceType)
                    {
                        writer.WriteEncodedText("ref ");
                    }

                    // Append the type name.
                    var parameterTypeSignature = parameter.Definition.ParameterType.GetTypeSignature(_urlResolver);
                    _renderer.Render(writer, parameterTypeSignature, TypeRenderOption.Name | TypeRenderOption.Link);
                    writer.WriteEncodedText(" ");

                    var parameterName = parameter.Name;
                    if (index == method.Parameters.Count - 1)
                    {
                        writer.WriteEncodedText(parameterName);
                        writer.WriteLine();
                    }
                    else
                    {
                        writer.WriteEncodedText(parameterName);
                        writer.WriteEncodedText(",");
                        writer.WriteLine();
                    }

                    index++;
                }
                writer.WriteEncodedText(")");
                writer.WriteLine();
            }
            else
            {
                if (!isPropertyAlias)
                {
                    writer.WriteEncodedText("()");
                    writer.WriteLine();
                }
            }
        }
    }
}

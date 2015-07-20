using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Cake.Web.Core.Services;
using Cake.Web.Docs.Reflection;
using Cake.Web.Docs.Reflection.Signatures;

namespace Cake.Web.Core.Rendering
{
    internal sealed class MethodRenderer
    {
        private readonly TypeRenderer _renderer;

        public MethodRenderer(TypeRenderer renderer)
        {
            _renderer = renderer;
        }

        public MvcHtmlString Render(LanguageProvider language,
            MethodSignature signature,
            MethodRenderOption options)
        {
            var builder = new HtmlTextWriter(new StringWriter());
            Render(builder, language, signature, options);
            return MvcHtmlString.Create(builder.InnerWriter.ToString());
        }

        public void Render(
            HtmlTextWriter writer,
            LanguageProvider language,
            MethodSignature signature,
            MethodRenderOption options)
        {
            // Add link.
            if ((options & MethodRenderOption.Link) == MethodRenderOption.Link)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Href, signature.Url);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
            }

            // Declaring type (with or without namespace).
            if ((options & MethodRenderOption.TypeName) == MethodRenderOption.TypeName ||
                (options & MethodRenderOption.TypeFullName) == MethodRenderOption.TypeFullName)
            {
                var onlyTypeName = (options & MethodRenderOption.TypeName) == MethodRenderOption.TypeName;
                var typeOptions = onlyTypeName ? TypeRenderOption.Name : TypeRenderOption.Namespace;
                _renderer.Render(writer, signature.DeclaringType, typeOptions);
            }

            var isImplicitOperator = signature.OperatorClassification == OperatorClassification.Implicit;
            var isExplicitOperator = signature.OperatorClassification == OperatorClassification.Explicit;
            var isImplicitOrExplicitOperator = isImplicitOperator || isExplicitOperator;

            if (isImplicitOrExplicitOperator)
            {
                // Implicit operator
                RenderImplicitOrExplicitOperator(writer, signature, options, isImplicitOperator);
            }
            else
            {
                // Everything else.
                if ((options & MethodRenderOption.Name) == MethodRenderOption.Name)
                {
                    if ((options & MethodRenderOption.TypeName) == MethodRenderOption.TypeName ||
                        (options & MethodRenderOption.TypeFullName) == MethodRenderOption.TypeFullName)
                    {

                        writer.WriteEncodedText("\u200B.");
                    }
                    writer.WriteEncodedText(GetMethodName(signature));
                }

                if ((options & MethodRenderOption.Parameters) == MethodRenderOption.Parameters)
                {
                    if (signature.Classification != MethodClassification.Operator)
                    {
                        writer.WriteEncodedText("(");
                        var parameterResult = new List<string>();
                        foreach (var parameter in signature.Parameters)
                        {
                            var parameterBuilder = new StringBuilder();
                            if (parameter.IsOutParameter)
                            {
                                parameterBuilder.Append("out ");
                            }
                            else if (parameter.IsRefParameter)
                            {
                                parameterBuilder.Append("ref ");
                            }
                            var paramType = _renderer.Render(parameter.ParameterType, TypeRenderOption.Name);
                            parameterBuilder.Append(paramType);
                            parameterResult.Add(parameterBuilder.ToString());
                        }
                        if (parameterResult.Count > 0)
                        {
                            if ((options & MethodRenderOption.ExtensionMethod) == MethodRenderOption.ExtensionMethod)
                            {
                                if (signature.Classification == MethodClassification.ExtensionMethod)
                                {
                                    // Remove first parameter.
                                    parameterResult.RemoveAt(0);
                                }
                            }
                            writer.Write(string.Join(", \u200B", parameterResult));
                        }
                        writer.WriteEncodedText(")\u200B");
                    }
                }
            }

            if ((options & MethodRenderOption.Link) == MethodRenderOption.Link)
            {
                writer.RenderEndTag();
            }
        }

        private string GetMethodName(MethodSignature signature)
        {
            if (signature.Classification == MethodClassification.Operator)
            {
                var operatorName = signature.OperatorClassification.GetOperatorName();
                if (operatorName != null)
                {
                    return operatorName;
                }
            }
            return signature.Name;
        }

        private void RenderImplicitOrExplicitOperator(
            HtmlTextWriter writer,
            MethodSignature signature,
            MethodRenderOption options,
            bool isImplicit)
        {
            if ((options & MethodRenderOption.TypeName) == MethodRenderOption.TypeName ||
                (options & MethodRenderOption.TypeFullName) == MethodRenderOption.TypeFullName)
            {
                writer.WriteEncodedText(".");
            }

            if ((options & MethodRenderOption.Name) == MethodRenderOption.Name)
            {
                writer.WriteEncodedText(isImplicit ? "Implicit" : "Explicit");
            }

            if ((options & MethodRenderOption.Parameters) == MethodRenderOption.Parameters)
            {
                if ((options & MethodRenderOption.Name) == MethodRenderOption.Name)
                {
                    writer.WriteEncodedText(" ");
                }

                writer.WriteEncodedText("(\u200B");
                _renderer.Render(writer, signature.Parameters[0].ParameterType, TypeRenderOption.Name);
                writer.WriteEncodedText(" to ");
                _renderer.Render(writer, signature.ReturnType, TypeRenderOption.Name);
                writer.WriteEncodedText(")\u200B");
            }
        }
    }
}

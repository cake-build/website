using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Cake.Web.Core.Services;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection.Signatures;

namespace Cake.Web.Core.Rendering
{
    internal sealed class TypeRenderer
    {
        private readonly DocumentModel _model;
        private readonly LanguageProvider _language;

        public TypeRenderer(DocumentModel model, LanguageProvider language)
        {
            _model = model;
            _language = language;
        }

        public IHtmlString Render(
            TypeSignature signature,
            TypeRenderOption options)
        {
            var builder = new HtmlTextWriter(new StringWriter());
            Render(builder, signature, options, false);
            return MvcHtmlString.Create(builder.InnerWriter.ToString());
        }

        public void Render(
            HtmlTextWriter writer,
            TypeSignature signature,
            TypeRenderOption options)
        {
            Render(writer, signature, options, false);
        }

        private void Render(
            HtmlTextWriter writer,
            TypeSignature signature,
            TypeRenderOption options,
            bool isWritingLink)
        {
            var needToCloseLink = false;
            var documentedType = _model.FindType(signature.Identity);

            // Writing link?
            if ((options & TypeRenderOption.Link) == TypeRenderOption.Link)
            {
                if (!isWritingLink && documentedType != null)
                {
                    if (signature.Url != null)
                    {
                        isWritingLink = needToCloseLink = true;

                        writer.AddAttribute(HtmlTextWriterAttribute.Href, signature.Url);
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                    }
                }
            }

            // Write type namespace?
            if ((options & TypeRenderOption.Namespace) == TypeRenderOption.Namespace)
            {
                writer.WriteEncodedText(signature.Namespace.Name);
            }

            // Write type name?
            if ((options & TypeRenderOption.Name) == TypeRenderOption.Name)
            {
                if ((options & TypeRenderOption.Namespace) == TypeRenderOption.Namespace)
                {
                    writer.WriteEncodedText(".\u200B");
                }

                var alias = _language.GetAlias(signature.Identity);
                var name = alias ?? signature.Name;
                writer.WriteEncodedText(name);

                if (signature.GenericArguments.Count != 0)
                {
                    // Write generic arguments.
                    writer.WriteEncodedText("<");
                    var result = new List<string>();
                    foreach (var argument in signature.GenericArguments)
                    {
                        result.Add(argument);
                    }
                    writer.WriteEncodedText(string.Join(", \u200B", result));
                    writer.WriteEncodedText(">");
                }
                else if (signature.GenericParameters.Count != 0)
                {
                    // Write generic parameters.
                    writer.Write("<");
                    var result = new List<string>();
                    var parameterIndex = 0;
                    foreach (var parameter in signature.GenericParameters)
                    {
                        parameterIndex++;
                        Render(writer, parameter, options, isWritingLink);
                        if (parameterIndex != signature.GenericParameters.Count)
                        {
                            writer.WriteEncodedText(",\u200B");
                            writer.WriteEncodedText(" ");
                        }
                    }
                    writer.WriteEncodedText(string.Join(", \u200B", result));
                    writer.Write(">");
                }
            }

            // Writing link?
            if ((options & TypeRenderOption.Link) == TypeRenderOption.Link)
            {
                if (needToCloseLink)
                {
                    writer.RenderEndTag();
                }
            }
        }
    }
}

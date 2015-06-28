using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Cake.Web.Core;
using Cake.Web.Docs;

namespace Cake.Web.Helpers.Api
{
    public static class NamespaceHelper
    {
        public static IHtmlString NamespaceUrl(this ApiServices context, DocumentedNamespace @namespace)
        {
            if (@namespace != null)
            {
                var url = context.UrlResolver.GetUrl(@namespace.Identity);
                if (url != null)
                {
                    return MvcHtmlString.Create(url);
                }
            }
            return MvcHtmlString.Create(string.Empty);
        }

        public static IHtmlString NamespaceLink(this ApiServices context, DocumentedNamespace @namespace)
        {
            if (@namespace != null)
            {
                var url = context.UrlResolver.GetUrl(@namespace.Identity);
                if (url != null)
                {
                    var writer = new HtmlTextWriter(new StringWriter());
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, url);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.WriteEncodedText(@namespace.Name);
                    writer.RenderEndTag();
                    return MvcHtmlString.Create(writer.InnerWriter.ToString());
                }
                return MvcHtmlString.Create(@namespace.Name);
            }
            return MvcHtmlString.Create(string.Empty);
        }

        public static IHtmlString NamespaceName(this ApiServices context, DocumentedNamespace @namespace)
        {
            if (@namespace != null)
            {
                return MvcHtmlString.Create(@namespace.Name);
            }
            return MvcHtmlString.Create(string.Empty);
        }
    }
}
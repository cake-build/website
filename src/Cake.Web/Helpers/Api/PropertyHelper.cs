using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Cake.Web.Core;
using Cake.Web.Docs;

namespace Cake.Web.Helpers.Api
{
    public static class PropertyHelper
    {
        public static IHtmlString PropertyLink(this ApiServices context, DocumentedProperty property)
        {
            if (property != null)
            {
                var url = context.UrlResolver.GetUrl(property.Identity);
                if (url != null)
                {
                    var writer = new HtmlTextWriter(new StringWriter());
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, url);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.WriteEncodedText(property.Definition.Name);
                    writer.RenderEndTag();
                    return MvcHtmlString.Create(writer.InnerWriter.ToString());
                }
                return MvcHtmlString.Create(property.Definition.Name);
            }
            return MvcHtmlString.Create(string.Empty);
        }

        public static IHtmlString PropertyName(this ApiServices context, DocumentedProperty property)
        {
            if (property != null)
            {
                return MvcHtmlString.Create(property.Definition.Name);
            }
            return MvcHtmlString.Create(string.Empty);
        }
    }
}
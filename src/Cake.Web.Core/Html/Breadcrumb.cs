using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Cake.Web.Core.Html
{
    public sealed class Breadcrumb : IBreadcrumbItem
    {
        private readonly LinkedList<IBreadcrumbItem> _items;

        public Breadcrumb()
        {
            _items = new LinkedList<IBreadcrumbItem>();
        }

        public void Append(IBreadcrumbItem item)
        {
            _items.AddLast(item);
        }

        public IHtmlString Render()
        {
            var writer = new HtmlTextWriter(new StringWriter());
            Render(writer);
            return MvcHtmlString.Create(writer.InnerWriter.ToString());
        }

        public void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "breadcrumb");
            writer.RenderBeginTag(HtmlTextWriterTag.Ol);
            {
                foreach (var item in _items)
                {
                    item.Render(writer);
                }
            }
            writer.RenderEndTag();
        }
    }
}
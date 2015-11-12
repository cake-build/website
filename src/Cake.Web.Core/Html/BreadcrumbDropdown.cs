using System.Collections.Generic;
using System.Web.UI;

namespace Cake.Web.Core.Html
{
    public class BreadcrumbDropdown : IBreadcrumbItem
    {
        private readonly BreadcrumbItem _owner;
        private readonly LinkedList<IBreadcrumbItem> _items;

        public int Count => _items.Count;
        public IBreadcrumbItem this[int index] => _items.First.Value;

        public BreadcrumbDropdown(BreadcrumbItem owner)
        {
            _owner = owner;
            _items = new LinkedList<IBreadcrumbItem>();
        }

        public void Append(IBreadcrumbItem item)
        {
            _items.AddLast(item);
        }

        public void Render(HtmlTextWriter writer)
        {
            // <a class="dropdown-toggle" id="branches" role="button" data-toggle="dropdown" href="#">default <b class="caret"></b></a>

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown");
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown-toggle");
                writer.AddAttribute("role", "button");
                writer.AddAttribute("data-toggle", "dropdown");
                writer.AddAttribute("href", "#");
                writer.AddAttribute("aria-expanded", "true");
                writer.AddAttribute("id", "breadcrumbDropDown");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                {
                    writer.Write(_owner.Text);
                    writer.Write(" ");

                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-caret-down");
                    writer.RenderBeginTag(HtmlTextWriterTag.I);
                    {
                        writer.RenderEndTag();
                    }

                    writer.RenderEndTag();
                }

                // aria-labelledby="dropdownMenu1"
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown-menu");
                writer.AddAttribute("aria-labelledby", "breadcrumbDropDown");
                writer.RenderBeginTag(HtmlTextWriterTag.Ol);
                {
                    foreach (var item in _items)
                    {
                        item.Render(writer);
                    }

                    writer.RenderEndTag();
                }
            }

            writer.RenderEndTag();
        }
    }
}
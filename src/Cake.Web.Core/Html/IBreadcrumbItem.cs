using System.Web.UI;

namespace Cake.Web.Core.Html
{
    public interface IBreadcrumbItem
    {
        void Append(IBreadcrumbItem item);
        void Render(HtmlTextWriter writer);
    }
}
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Cake.Web.Core;
using Cake.Web.Core.Rendering;
using Cake.Web.Docs.Comments;

namespace Cake.Web.Helpers.Api
{
    public static class CommentHelper
    {
        public static IHtmlString Comment(this ApiServices context, IComment comment)
        {
            if (comment != null)
            {
                var writer = new HtmlTextWriter(new StringWriter());
                CommentRenderer.Render(comment, new CommentRendererContext(writer, context));
                return MvcHtmlString.Create(writer.InnerWriter.ToString());
            }
            return MvcHtmlString.Create(string.Empty);
        }
    }
}
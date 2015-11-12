using System.Web.UI;

namespace Cake.Web.Core.Rendering
{
    public sealed class CommentRendererContext
    {
        public HtmlTextWriter Writer { get; }
        public ApiServices Services { get; }

        public CommentRendererContext(HtmlTextWriter writer, ApiServices services)
        {
            Writer = writer;
            Services = services;
        }
    }
}
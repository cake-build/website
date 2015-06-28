using System.Web.UI;

namespace Cake.Web.Core.Rendering
{
    public sealed class CommentRendererContext
    {
        private readonly HtmlTextWriter _writer;
        private readonly ApiServices _services;

        public HtmlTextWriter Writer
        {
            get { return _writer; }
        }

        public ApiServices Services
        {
            get { return _services; }
        }

        public CommentRendererContext(HtmlTextWriter writer, ApiServices services)
        {
            _writer = writer;
            _services = services;
        }
    }
}
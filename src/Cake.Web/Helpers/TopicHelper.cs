using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Cake.Web.Core.Content.Documentation;

namespace Cake.Web.Helpers
{
    public static class TopicHelper
    {
        public static string GitHubLink(this HtmlHelper helper, Topic topic)
        {
            return LinkHelper.GetGitHubLink(topic);
        }

        public static IHtmlString RenderTree(this HtmlHelper helper, TopicTree tree)
        {
            var writer = new HtmlTextWriter(new StringWriter());

            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            foreach (var section in tree.Sections)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write(section.Name);
                if (section.Topics.Count > 0)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                    foreach (var topic in section.Topics)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Li);
                        WriteLink(writer, topic);
                        writer.RenderEndTag();
                    }
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            writer.RenderEndTag();

            return MvcHtmlString.Create(writer.InnerWriter.ToString());
        }

        public static IHtmlString RenderLink(this HtmlHelper helper, Topic topic)
        {
            var writer = new HtmlTextWriter(new StringWriter());
            WriteLink(writer, topic);
            return MvcHtmlString.Create(writer.InnerWriter.ToString());
        }

        public static void WriteDocumentationLink(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "/docs");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write("Documentation");
            writer.RenderEndTag();
        }

        public static void WriteLink(HtmlTextWriter writer, Topic topic)
        {
            if (string.IsNullOrWhiteSpace(topic.Body))
            {
                writer.Write(topic.Name);
            }
            else
            {
                var path = LinkHelper.GetLink(topic);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, path);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(topic.Name);
                writer.RenderEndTag();
            }
        }
    }
}
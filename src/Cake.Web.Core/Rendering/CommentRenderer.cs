using System.Web.UI;
using Cake.Web.Docs;
using Cake.Web.Docs.Comments;

namespace Cake.Web.Core.Rendering
{
    public class CommentRenderer : CommentVisitor<CommentRendererContext>
    {
        public static void Render(IComment comment, CommentRendererContext context)
        {
            comment.Accept(new CommentRenderer(), context);
        }

        public override void VisitCode(CodeComment comment, CommentRendererContext context)
        {
            if (!string.IsNullOrWhiteSpace(comment.Code))
            {
                var code = comment.Code;
                //code = code.Replace("{", "{{").Replace("}", "}}");
                code = code.UnintendCode();

                context.Writer.Write("<pre>");
                context.Writer.Write("<code>");
                context.Writer.WriteEncodedText(code);
                context.Writer.Write("</code>");
                context.Writer.Write("</pre>");
            }
        }

        public override void VisitSee(SeeComment comment, CommentRendererContext context)
        {
            var docs = context.Services.Model.FindType(comment.Member);
            if (docs != null)
            {
                var signature = context.Services.SignatureResolver.GetTypeSignature(docs);
                context.Services.SignatureRenderer.Render(context.Writer, signature, TypeRenderOption.Name | TypeRenderOption.Link);
            }
            else
            {
                // Scrub the CRef name.
                var name = comment.Member;
                var colonIndex = name.IndexOf(':');
                if (colonIndex != -1)
                {
                    name = name.Substring(colonIndex + 1);
                }
                var lastApostrophe = name.LastIndexOf('`');
                if (lastApostrophe != -1)
                {
                    name = name.Substring(0, lastApostrophe);
                }

                context.Writer.RenderBeginTag(HtmlTextWriterTag.I);
                context.Writer.Write(name);
                context.Writer.RenderEndTag();
            }
        }

        public override void VisitInlineCode(InlineCodeComment comment, CommentRendererContext context)
        {
            context.Writer.RenderBeginTag(HtmlTextWriterTag.I);
            context.Writer.Write(comment.Code);
            context.Writer.RenderEndTag();
        }

        public override void VisitInlineText(InlineTextComment comment, CommentRendererContext context)
        {
            context.Writer.WriteEncodedText(comment.Text);
        }
    }
}
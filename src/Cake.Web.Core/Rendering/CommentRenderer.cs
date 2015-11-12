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
                var code = comment.Code ?? string.Empty;
                code = code.UnintendCode();

                context.Writer.Write("<div class=\"panel-body comment-code-box\">");
                context.Writer.Write("<pre>");
                context.Writer.Write("<code class=\"language-csharp\">");
                context.Writer.WriteEncodedText(code);
                context.Writer.Write("</code>");
                context.Writer.Write("</pre>");
                context.Writer.Write("</div>");
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

        public override void VisitSeeExternalLink(SeeExternalLinkComment comment, CommentRendererContext context)
        {
            context.Writer.AddAttribute(HtmlTextWriterAttribute.Href, comment.Url);
            context.Writer.RenderBeginTag(HtmlTextWriterTag.A);
            context.Writer.WriteEncodedText(comment.Text);
            context.Writer.RenderEndTag();
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

        public override void VisitPara(ParaComment comment, CommentRendererContext context)
        {
            context.Writer.RenderBeginTag(HtmlTextWriterTag.P);
            foreach (var child in comment.Children)
            {
                child.Accept(this, context);
            }
            context.Writer.RenderEndTag();
        }
    }
}
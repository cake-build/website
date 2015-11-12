namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents an inline text comment.
    /// </summary>
    public sealed class InlineTextComment : Comment
    {
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineTextComment"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public InlineTextComment(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <typeparam name="T">The context type.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <param name="context">The context.</param>
        public override void Accept<T>(CommentVisitor<T> visitor, T context)
        {
            visitor.VisitInlineText(this, context);
        }
    }
}

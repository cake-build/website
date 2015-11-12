namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents a whitespace comment.
    /// </summary>
    public sealed class WhitespaceComment : Comment
    {
        /// <summary>
        /// Gets the whitespace text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhitespaceComment"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public WhitespaceComment(string text)
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
            visitor.VisitWhitespace(this, context);
        }
    }
}

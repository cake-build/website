namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents a <code>code</code> comment.
    /// </summary>
    public sealed class CodeComment : Comment
    {
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeComment"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public CodeComment(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <typeparam name="T">The context type.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <param name="context">The context.</param>
        public override void Accept<T>(CommentVisitor<T> visitor, T context)
        {
            visitor.VisitCode(this, context);
        }
    }
}

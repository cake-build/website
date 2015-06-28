namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents a comment.
    /// </summary>
    public interface IComment
    {
        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <typeparam name="T">The context type.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <param name="context">The context.</param>
        void Accept<T>(CommentVisitor<T> visitor, T context);
    }
}
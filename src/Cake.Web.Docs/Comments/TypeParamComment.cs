using System.Collections.Generic;

namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents a <code>typeparam</code> comment.
    /// </summary>
    public sealed class TypeParamComment : AggregateComment
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeParamComment"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="comments">The comments.</param>
        public TypeParamComment(string name, IEnumerable<IComment> comments)
            : base(comments)
        {
            Name = name;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <typeparam name="T">The context type.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <param name="context">The context.</param>
        public override void Accept<T>(CommentVisitor<T> visitor, T context)
        {
            visitor.VisitTypeParam(this, context);
        }
    }
}

namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents a <code>typeparamref</code> comment.
    /// </summary>
    public sealed class TypeParamRefComment : Comment
    {
        private readonly string _name;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeParamRefComment"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TypeParamRefComment(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <typeparam name="T">The context type.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <param name="context">The context.</param>
        public override void Accept<T>(CommentVisitor<T> visitor, T context)
        {
            visitor.VisitTypeParamRef(this, context);
        }
    }
}

namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents a <code>paramref</code> comment.
    /// </summary>
    public sealed class ParamRefComment : Comment
    {
        private readonly string _name;

        /// <summary>
        /// Gets the name of the parameter reference.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParamRefComment"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ParamRefComment(string name)
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
            visitor.VisitParamRef(this, context);
        }
    }
}

namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents a <code>see</code> comment.
    /// </summary>
    public class SeeExternalLinkComment : Comment
    {
        private readonly string _url;
        private readonly string _text;

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url
        {
            get { return _url; }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return _text; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeeComment"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="text">The text.</param>
        public SeeExternalLinkComment(string url, string text)
        {
            _url = url;
            _text = text;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <typeparam name="T">The context type.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <param name="context">The context.</param>
        public override void Accept<T>(CommentVisitor<T> visitor, T context)
        {
            visitor.VisitSeeExternalLink(this, context);
        }
    }
}
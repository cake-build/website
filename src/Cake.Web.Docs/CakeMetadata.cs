using System;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Cake metadata.
    /// </summary>
    public sealed class CakeMetadata : IDocumentationMetadata
    {
        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public Uri Url { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is owned by an addin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is owned by an addin; otherwise, <c>false</c>.
        /// </value>
        public bool IsOwnedByAddin => false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is an alias.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is an alias; otherwise, <c>false</c>.
        /// </value>
        public bool IsAlias => false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a property alias.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is a property alias; otherwise, <c>false</c>.
        /// </value>
        public bool IsPropertyAlias => false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CakeMetadata" /> class.
        /// </summary>
        public CakeMetadata()
        {
            Url = new Uri("http://cakebuild.net");
        }
    }
}

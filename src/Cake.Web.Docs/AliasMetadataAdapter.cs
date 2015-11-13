using System;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Metadata adapter for an alias.
    /// </summary>
    public sealed class AliasMetadataAdapter : IDocumentationMetadata
    {
        private readonly IDocumentationMetadata _metadata;

        /// <summary>
        /// Gets a value indicating whether this instance is a property alias.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a property alias; otherwise, <c>false</c>.
        /// </value>
        public bool IsPropertyAlias { get; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public Uri Url => _metadata.Url;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is owned by an addin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is owned by an addin; otherwise, <c>false</c>.
        /// </value>
        public bool IsOwnedByAddin => _metadata.IsOwnedByAddin;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is an alias.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is an alias; otherwise, <c>false</c>.
        /// </value>
        public bool IsAlias => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="AliasMetadataAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="isPropertyAlias">if set to <c>true</c> [is property alias].</param>
        public AliasMetadataAdapter(IDocumentationMetadata metadata, bool isPropertyAlias)
        {
            _metadata = metadata;
            IsPropertyAlias = isPropertyAlias;
        }
    }
}
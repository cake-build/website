using System;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Metadata for documentation members.
    /// </summary>
    public interface IDocumentationMetadata
    {
        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>The URI.</value>
        Uri Url { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is owned by an addin.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is owned by an addin; otherwise, <c>false</c>.
        /// </value>
        bool IsOwnedByAddin { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is an alias.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is an alias; otherwise, <c>false</c>.
        /// </value>
        bool IsAlias { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is a property alias.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a property alias; otherwise, <c>false</c>.
        /// </value>
        bool IsPropertyAlias { get; }
    }
}

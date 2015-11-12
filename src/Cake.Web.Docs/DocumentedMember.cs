using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Reflection;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents a documented member.
    /// </summary>
    public abstract class DocumentedMember
    {
        /// <summary>
        /// Gets the summary comment.
        /// </summary>
        /// <value>The summary comment.</value>
        public SummaryComment Summary { get; }

        /// <summary>
        /// Gets the remarks comment.
        /// </summary>
        /// <value>The remarks comment.</value>
        public RemarksComment Remarks { get; }

        /// <summary>
        /// Gets the example comment.
        /// </summary>
        /// <value>The example comment.</value>
        public IReadOnlyList<ExampleComment> Examples { get; }

        /// <summary>
        /// Gets the classification.
        /// </summary>
        /// <value>The classification.</value>
        public MemberClassification Classification { get; }

        /// <summary>
        /// Gets the metadata associated with this member.
        /// </summary>
        public IDocumentationMetadata Metadata { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentedMember"/> class.
        /// </summary>
        /// <param name="classification">The member classification.</param>
        /// <param name="summary">The summary comment.</param>
        /// <param name="remarks">The remarks comment.</param>
        /// <param name="examples">The example comments.</param>
        /// <param name="metadata">The metadata associated with the member.</param>
        protected DocumentedMember(
            MemberClassification classification,
            SummaryComment summary,
            RemarksComment remarks,
            IEnumerable<ExampleComment> examples,
            IDocumentationMetadata metadata)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            Classification = classification;
            Summary = summary;
            Remarks = remarks;
            Examples = new List<ExampleComment>(examples ?? Enumerable.Empty<ExampleComment>());
            Metadata = metadata;
        }
    }
}
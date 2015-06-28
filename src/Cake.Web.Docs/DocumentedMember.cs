using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Reflection;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents a documented member.
    /// </summary>
    public abstract class DocumentedMember
    {
        private readonly SummaryComment _summary;
        private readonly RemarksComment _remarks;
        private readonly ExampleComment _example;
        private readonly MemberClassification _classification;

        /// <summary>
        /// Gets the summary comment.
        /// </summary>
        /// <value>The summary comment.</value>
        public SummaryComment Summary
        {
            get { return _summary; }
        }

        /// <summary>
        /// Gets the remarks comment.
        /// </summary>
        /// <value>The remarks comment.</value>
        public RemarksComment Remarks
        {
            get { return _remarks; }
        }

        /// <summary>
        /// Gets the example comment.
        /// </summary>
        /// <value>The example comment.</value>
        public ExampleComment Example
        {
            get { return _example; }
        }

        /// <summary>
        /// Gets the classification.
        /// </summary>
        /// <value>The classification.</value>
        public MemberClassification Classification
        {
            get { return _classification; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentedMember"/> class.
        /// </summary>
        /// <param name="classification">The member classification.</param>
        /// <param name="summary">The summary comment.</param>
        /// <param name="remarks">The remarks comment.</param>
        /// <param name="example">The example comment.</param>
        protected DocumentedMember(
            MemberClassification classification, 
            SummaryComment summary, 
            RemarksComment remarks, 
            ExampleComment example)
        {
            _classification = classification;
            _summary = summary;
            _remarks = remarks;
            _example = example;
        }
    }
}
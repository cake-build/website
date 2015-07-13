using System.Diagnostics;
using Cake.Core.IO;

namespace Cake.Web.Core.Content.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public sealed class Topic
    {
        private readonly string _id;
        private readonly string _name;
        private readonly string _body;
        private readonly FilePath _path;

        public string Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Body
        {
            get { return _body; }
        }

        public Topic Previous { get; set; }
        public Topic Next { get; set; }
        public TopicSection Section { get; internal set; }

        public bool HasPrevious
        {
            get { return Previous != null && Previous.HasContent; }
        }

        public bool HasNext
        {
            get { return Next != null && Next.HasContent; }
        }

        public bool HasContent
        {
            get { return !string.IsNullOrWhiteSpace(Body); }
        }

        public FilePath Path
        {
            get { return _path; }
        }

        public Topic(string id, string name, string body, FilePath path)
        {
            _id = id;
            _name = name;
            _body = body;
            _path = path;
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private string DebuggerDisplay()
        {
            return string.Format("Topic: {0}", _name);
        }
    }
}

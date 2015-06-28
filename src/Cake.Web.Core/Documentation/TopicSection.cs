using System.Collections.Generic;
using System.Diagnostics;

namespace Cake.Web.Core.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public sealed class TopicSection
    {
        private readonly string _id;
        private readonly string _name;
        private readonly List<Topic> _topics;

        public string Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public List<Topic> Topics
        {
            get { return _topics; }
        }

        public TopicTree Tree { get; internal set; }

        public TopicSection(string id, string name, IEnumerable<Topic> topics)
        {
            _id = id;
            _name = name;
            _topics = new List<Topic>(topics);
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private string DebuggerDisplay()
        {
            return string.Format("Section: {0}", _name);
        }
    }
}

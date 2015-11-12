using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Cake.Web.Core.Content.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public sealed class TopicSection
    {
        private readonly string _id;
        private readonly string _name;
        private readonly bool _hidden;
        private readonly List<Topic> _topics;
        private bool _hasVisibleTopics;

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

        public bool Hidden
        {
            get { return _hidden; }
        }

        public bool HasVisibleTopics
        {
            get { return _hasVisibleTopics; }
        }

        public TopicTree Tree { get; internal set; }

        public TopicSection(string id, string name, bool hidden, IEnumerable<Topic> topics)
        {
            _id = id;
            _name = name;
            _hidden = hidden;
            _topics = new List<Topic>(topics);
            _hasVisibleTopics = _topics.Any(x => !x.Hidden);
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private string DebuggerDisplay()
        {
            return string.Format("Section: {0}", _name);
        }
    }
}

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Cake.Web.Core.Content.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public sealed class TopicSection
    {
        public string Id { get; }
        public string Name { get; }
        public IReadOnlyList<Topic> Topics { get; }
        public bool Hidden { get; }
        public bool HasVisibleTopics { get; }
        public TopicTree Tree { get; internal set; }

        public TopicSection(string id, string name, bool hidden, IEnumerable<Topic> topics)
        {
            Id = id;
            Name = name;
            Hidden = hidden;
            Topics = new List<Topic>(topics);
            HasVisibleTopics = Topics.Any(x => !x.Hidden);
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private string DebuggerDisplay()
        {
            return $"Section: {Name}";
        }
    }
}

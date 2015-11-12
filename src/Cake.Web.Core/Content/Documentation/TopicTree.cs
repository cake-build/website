using System.Collections.Generic;

namespace Cake.Web.Core.Content.Documentation
{
    public sealed class TopicTree
    {
        public IReadOnlyList<TopicSection> Sections { get; }

        public TopicTree(IEnumerable<TopicSection> sections)
        {
            Sections = new List<TopicSection>(sections);
        }

        public TopicSection FindSection(string sectionPath)
        {
            foreach (var section in Sections)
            {
                if (section.Id == sectionPath)
                {
                    return section;
                }
            }
            return null;
        }

        public Topic FindTopic(string sectionPath, string topicPath)
        {
            var section = FindSection(sectionPath);
            if (section != null)
            {
                foreach (var topic in section.Topics)
                {
                    if (topic.Id == topicPath)
                    {
                        return topic;
                    }
                }
            }
            return null;
        }
    }
}

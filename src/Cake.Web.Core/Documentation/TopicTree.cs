using System.Collections.Generic;

namespace Cake.Web.Core.Documentation
{
    public sealed class TopicTree
    {
        private readonly List<TopicSection> _sections;

        public IReadOnlyList<TopicSection> Sections
        {
            get { return _sections; }
        }

        public TopicTree(IEnumerable<TopicSection> sections)
        {
            _sections = new List<TopicSection>(sections);
        }

        public TopicSection FindSection(string sectionPath)
        {
            foreach (var section in _sections)
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

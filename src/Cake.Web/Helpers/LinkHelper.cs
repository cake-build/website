using Cake.Web.Core.Documentation;

namespace Cake.Web.Helpers
{
    public static class LinkHelper
    {
        private const string GitHubTemplate = "https://github.com/cake-build/website/blob/develop/src/Cake.Web/App_Data/docs/{0}";

        public static string GetLink(TopicSection section)
        {
            return string.Concat("/docs/", section.Id);
        }

        public static string GetLink(Topic topic)
        {
            return string.Concat("/docs/", topic.Section.Id, "/", topic.Id);
        }

        public static string GetGitHubLink(Topic topic)
        {
            return string.Format(GitHubTemplate, topic.Path.FullPath);
        }
    }
}
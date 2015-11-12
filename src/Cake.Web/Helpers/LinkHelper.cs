using System;
using Cake.Web.Core.Content.Blog;
using Cake.Web.Core.Content.Documentation;

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

        public static string GetLink(BlogPost post)
        {
            return $"/blog/{post.PostedAt.Year}/{post.PostedAt.Month:00}/{post.Slug}";
        }

        public static string GetLink(BlogCategory category)
        {
            return string.Concat("/blog/category/", category.Slug);
        }

        public static string GetArchiveLink(DateTime time)
        {
            return $"/blog/archive/{time.Year}/{time.Month:00}";
        }
    }
}
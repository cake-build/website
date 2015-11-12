namespace Cake.Web.Core.Content.Blog
{
    public sealed class BlogCategory
    {
        public string Slug { get; }
        public string Title { get; }

        public BlogCategory(string slug, string title)
        {
            Slug = slug;
            Title = title;
        }
    }
}

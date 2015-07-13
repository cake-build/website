namespace Cake.Web.Core.Content.Blog
{
    public sealed class BlogCategory
    {
        private readonly string _slug;
        private readonly string _title;

        public string Slug
        {
            get { return _slug; }
        }

        public string Title
        {
            get { return _title; }
        }

        public BlogCategory(string slug, string title)
        {
            _slug = slug;
            _title = title;
        }
    }
}

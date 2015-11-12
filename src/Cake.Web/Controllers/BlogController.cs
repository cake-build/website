using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using Cake.Web.Core.Content.Blog;
using Cake.Web.Helpers;
using Cake.Web.Models;

namespace Cake.Web.Controllers
{
    public class BlogController : Controller
    {
        private const int PostsPerPage = 5;

        private readonly BlogIndex _index;

        public BlogController(BlogIndex index)
        {
            _index = index;
        }

        public ActionResult Index(string category = null, int year = 0, int month = 0, int page = 1)
        {
            var allPosts = _index.GetBlogPosts(category, year, month);

            if (page < 1)
            {
                page = 1;
            }
            if ((page - 1)*PostsPerPage >= allPosts.Count && allPosts.Count > 0)
            {
                return new HttpNotFoundResult();
            }

            var posts = allPosts.OrderByDescending(x => x.PostedAt)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage).ToArray();

            return View(new BlogPageViewModel(posts, _index.GetCategories(), _index.GetArchive())
            {
                CurrentPage = page,
                HasOlderPosts = allPosts.Count > page * PostsPerPage,
                HasNewerPosts = page > 1,
                Category = category,
                Year = year,
                Month = month
            });
        }

        public ActionResult Details(int year, int month, string slug)
        {
            var post = _index.GetBlogPost(year, month, slug);
            if (post == null)
            {
                return new HttpNotFoundResult();
            }

            return View(new BlogPostViewModel(post, _index.GetCategories(), _index.GetArchive()));
        }

        public ActionResult Feed(string format)
        {
            var posts = _index.GetBlogPosts().OrderByDescending(x => x.PostedAt);

            // Create feed items.
            var items = new List<SyndicationItem>();
            foreach (var post in posts)
            {
                var feedItem = new SyndicationItem 
                {
                    Content = new TextSyndicationContent(post.FeedBody, TextSyndicationContentKind.Html),
                    Id = post.Id,
                    PublishDate = post.PostedAt,
                    Title = new TextSyndicationContent(post.Title, TextSyndicationContentKind.Plaintext),
                };

                var url = string.Concat("http://cakebuild.net", LinkHelper.GetLink(post));
                feedItem.Links.Add(new SyndicationLink(new Uri(url)));
                items.Add(feedItem);
            }

            // Create the feed.
            var feed = new SyndicationFeed(items)
            {
                Title = new TextSyndicationContent("Cake", TextSyndicationContentKind.Plaintext),
                Description = new TextSyndicationContent("The Cake blog feed", TextSyndicationContentKind.Plaintext)
            };

            // Write the feed as a response.
            // TODO: Cache this at start up?
            using (var ms = new MemoryStream())
            {
                var writer = XmlWriter.Create(ms);

                var contentType = "application/atom+xml";
                if (string.Equals("rss", format, StringComparison.InvariantCultureIgnoreCase))
                {
                    feed.SaveAsRss20(writer);
                    contentType = "application/rss+xml";
                }
                else
                {
                    feed.SaveAsAtom10(writer);
                }
                
                writer.Flush();
                var text = Encoding.UTF8.GetString(ms.ToArray());
                return Content(text, contentType, Encoding.UTF8);
            }
        }
    }
}
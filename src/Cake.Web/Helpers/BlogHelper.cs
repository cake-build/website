using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Cake.Web.Core.Content.Blog;
using Cake.Web.Models;

namespace Cake.Web.Helpers
{
    public static class BlogHelper
    {
        public static string GetLink(this HtmlHelper helper, BlogPost post)
        {
            return LinkHelper.GetLink(post);
        }

        public static string GetLink(this HtmlHelper helper, BlogCategory category)
        {
            return LinkHelper.GetLink(category);
        }

        public static string GetArchiveLink(this HtmlHelper helper, DateTime time)
        {
            return LinkHelper.GetArchiveLink(time);
        }

        public static string GetPreviousPageLink(this HtmlHelper helper, BlogPageViewModel model)
        {
            var parts = new List<string> { "blog" };

            if (model.Year != 0)
            {
                parts.Add(model.Year.ToString());
                if (model.Month != 0)
                {
                    parts.Add(model.Month.ToString("D2"));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(model.Category))
                {
                    parts.Add("category");
                    parts.Add(model.Category);
                }
            }

            return $"/{string.Join("/", parts)}/?page={Math.Max(model.CurrentPage - 1, 1)}";
        }

        public static string GetNextPageLink(this HtmlHelper helper, BlogPageViewModel model)
        {
            var parts = new List<string> { "blog" };

            if (model.Year != 0)
            {
                parts.Add("archive");
                parts.Add(model.Year.ToString());
                if (model.Month != 0)
                {
                    parts.Add(model.Month.ToString("D2"));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(model.Category))
                {
                    parts.Add("category");
                    parts.Add(model.Category);
                }
            }

            return $"/{string.Join("/", parts)}/?page={model.CurrentPage + 1}";
        }

        public static IHtmlString RenderCategoryList(this HtmlHelper helper, BlogPost post)
        {
            var writer = new HtmlTextWriter(new StringWriter());
            for (int i = 0; i < post.Categories.Count; i++)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Href, LinkHelper.GetLink(post.Categories[i]));
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(post.Categories[i].Title);
                writer.RenderEndTag();

                if (i != post.Categories.Count - 1)
                {
                    writer.Write(", ");
                }
            }
            return MvcHtmlString.Create(writer.InnerWriter.ToString());
        }
    }
}
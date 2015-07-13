using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Cake.Core.IO;

/////////////////////////////////////////////////////////////////////////////
// Filename parsing code taken from Sandra.Snow.
// https://github.com/Sandra/Sandra.Snow/blob/master/src/Snow/PostParser.cs 
/////////////////////////////////////////////////////////////////////////////

namespace Cake.Web.Core.Content.Blog
{
    internal sealed class BlogFilename
    {
        private static readonly Regex FilenameRegex =
            new Regex(@"^(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})-(?<slug>.+).(?:md|markdown)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Slug { get; private set; }
        public DateTime PostedAt { get; private set; }

        public static BlogFilename Parse(FilePath path)
        {
            var match = FilenameRegex.Match(path.GetFilename().FullPath);
            if (!match.Success)
            {
                return null;
            }

            var year = match.Groups["year"].Value;
            var month = match.Groups["month"].Value;
            var day = match.Groups["day"].Value;

            return new BlogFilename
            {
                Slug = match.Groups["slug"].Value.ToSlug(),
                PostedAt = DateTime.ParseExact(year + month + day, "yyyyMMdd", CultureInfo.InvariantCulture)
            };
        }
    }
}

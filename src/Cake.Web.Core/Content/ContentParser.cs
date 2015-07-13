using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cake.Core.IO;

namespace Cake.Web.Core.Content
{
    internal sealed class ContentParser
    {
        private readonly IFileSystem _fileSystem;

        public ContentParser(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public ContentParseResult Parse(FilePath path)
        {
            var file = _fileSystem.GetFile(path);
            if (file.Exists)
            {
                using (var stream = file.OpenRead())
                using (var reader = new StreamReader(stream))
                {
                    return ParseContent(reader.ReadToEnd());
                }
            }
            return new ContentParseResult(new Dictionary<string, string>(), string.Empty);
        }

        private static ContentParseResult ParseContent(string content)
        {
            var lines = SplitLines(content);
            if (!lines[0].StartsWith("---"))
            {
                // No front matter.
                return new ContentParseResult(new Dictionary<string, string>(), content);
            }

            var index = 1;
            var header = new Dictionary<string, string>();
            foreach (var rline in lines.Skip(1))
            {
                var line = rline.Trim();
                if (line.StartsWith("---"))
                {
                    break;
                }

                var keyIndex = line.IndexOf(':');
                if (keyIndex == -1)
                {
                    throw new InvalidOperationException("Invalid post format!");
                }

                var key = line.Substring(0, keyIndex);
                var value = line.Substring(keyIndex + 1, line.Length - keyIndex - 1);

                header.Add(key.Trim(), value.Trim());

                index++;
            }

            var body = (index < lines.Length)
                ? string.Join(Environment.NewLine, lines.Skip(index + 1).Take(lines.Length - index))
                : string.Empty;

            // <!--excerpt-->
            var excerpt = string.Empty;
            var excerptParts = body.Split(new[] { "<!--excerpt-->" }, StringSplitOptions.RemoveEmptyEntries);
            if (excerptParts.Length > 1)
            {
                excerpt = excerptParts[0];
            }

            return new ContentParseResult(header, body, excerpt);
        }

        private static string[] SplitLines(string content)
        {
            content = NormalizeLineEndings(content);
            return content.Split(new[] { "\r\n" }, StringSplitOptions.None);
        }

        private static string NormalizeLineEndings(string value)
        {
            if (value != null)
            {
                value = value.Replace("\r\n", "\n");
                value = value.Replace("\r", string.Empty);
                return value.Replace("\n", "\r\n");
            }
            return string.Empty;
        }
    }
}

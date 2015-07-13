using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Cake.Web.Core
{
    internal static class StringExtensions
    {
        public static string ToSlug(this string text)
        {
            var builder = new StringBuilder();
            foreach (var character in text)
            {
                if (char.IsLetterOrDigit(character))
                {
                    builder.Append(char.ToLower(character));
                }
                else if (character == ' ')
                {
                    builder.Append('-');
                }
                else if (character == '.')
                {
                    builder.Append('-');
                }
                else if (character == '-')
                {
                    builder.Append('-');
                }
            }
            return builder.ToString();
        }

        public static string[] SplitLines(this string content)
        {
            content = NormalizeLineEndings(content);
            return content.Split(new[] { "\r\n" }, StringSplitOptions.None);
        }

        public static string NormalizeLineEndings(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            value = value.Replace("\r\n", "\n");
            value = value.Replace("\r", string.Empty);
            return value.Replace("\n", "\r\n");
        }

        public static string UnintendCode(this string code)
        {
            var lines = TrimEnd(TrimBeginning(SplitLines(code)));
            var index = GetIndexOfFirstNonWhitespace(lines[0]);
            var result = new List<string>();
            foreach (var line in lines)
            {
                result.Add(line.Length > index
                    ? line.Substring(index, line.Length - index)
                    : line);
            }

            return string.Join("\r\n", result);
        }

        private static int GetIndexOfFirstNonWhitespace(string text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                if (!char.IsWhiteSpace(text[i]))
                {
                    return i;
                }
            }
            throw new InvalidOperationException("Should not start with only whitespace...");
        }

        private static string[] TrimBeginning(string[] lines)
        {
            var range = Enumerable.Range(0, lines.Length).ToArray();
            return Trim(lines, range, enumerable => enumerable.ToArray());
        }

        private static string[] TrimEnd(string[] lines)
        {
            var range = Enumerable.Range(0, lines.Length).Reverse().ToArray();
            return Trim(lines, range, enumerable => enumerable.Reverse().ToArray());
        }

        private static string[] Trim(string[] lines, int[] range, Func<IEnumerable<string>, string[]> transformer)
        {
            var result = new List<string>();
            var foundContent = false;
            foreach (var line in range.Select(index => lines[index]))
            {
                if (!foundContent)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        foundContent = true;
                        result.Add(line);
                    }
                }
                else
                {
                    result.Add(line);
                }
            }
            return transformer(result);
        }
    }
}

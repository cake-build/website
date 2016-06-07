using System;
using System.Diagnostics;
using Cake.Core.IO;

namespace Cake.Web.Core.Content.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public sealed class Topic
    {
        public string Id { get; }
        public string Name { get; }
        public string Body { get; }
        public FilePath Path { get; }
        public string Url { get; set; }
        public bool Hidden { get; }

        public Topic Previous { get; set; }
        public Topic Next { get; set; }
        public TopicSection Section { get; internal set; }

        public bool HasPrevious => Previous != null && Previous.HasContent;
        public bool HasNext => Next != null && Next.HasContent;
        public bool HasContent => !string.IsNullOrWhiteSpace(Body);

        public Topic(string id, string name, string body, bool hidden, string path, string url)
        {
            Id = id;
            Name = name;
            Body = body;
            Hidden = hidden;
            Path = path != null ? path : (FilePath)null;
            Url = url;
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private string DebuggerDisplay()
        {
            return $"Topic: {Name}";
        }
    }
}

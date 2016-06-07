using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Cake.Core.IO;
using Path = System.IO.Path;

namespace Cake.Web.Core.Content.Documentation
{
    internal sealed class TopicReader : ITopicReader
    {
        private readonly IFileSystem _fileSystem;
        private readonly ContentParser _contentParser;
        private readonly ContentConverter _contentConverter;
        private readonly ContentProcessor _contentProcessor;

        public TopicReader(
            IFileSystem fileSystem,
            ContentParser contentParser,
            ContentConverter contentConverter,
            ContentProcessor contentProcessor)
        {
            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }
            if (contentParser == null)
            {
                throw new ArgumentNullException(nameof(contentParser));
            }
            if (contentConverter == null)
            {
                throw new ArgumentNullException(nameof(contentConverter));
            }
            _fileSystem = fileSystem;
            _contentParser = contentParser;
            _contentConverter = contentConverter;
            _contentProcessor = contentProcessor;
        }

        public TopicTree Read(FilePath path)
        {
            var root = path.GetDirectory();
            root = root.Combine("docs");
            var file = _fileSystem.GetFile(path);
            if (file.Exists)
            {
                using (var stream = file.OpenRead())
                {
                    return FixLinks(Read(root, stream));
                }
            }
            var message = $"Could not find toc file ({path.FullPath}).";
            throw new FileNotFoundException(message);
        }

        private TopicTree Read(DirectoryPath root, Stream stream)
        {
            var result = new List<TopicSection>();
            var reader = new XmlTextReader(stream);
            while (reader.Read())
            {
                if (reader.IsStartElement() && reader.Name == "section")
                {
                    var section = ReadSection(reader, root);
                    if (section != null)
                    {
                        result.Add(section);
                    }
                }
            }
            return new TopicTree(result);
        }

        private TopicTree FixLinks(TopicTree tree)
        {
            Topic previous = null;
            foreach (var section in tree.Sections)
            {
                section.Tree = tree;
                foreach (var topic in section.Topics)
                {
                    if (topic.HasContent)
                    {
                        if (previous != null)
                        {
                            topic.Previous = previous;
                            previous.Next = topic;
                        }
                        previous = topic;
                    }
                }
            }
            return tree;
        }

        private TopicSection ReadSection(XmlReader reader, DirectoryPath root)
        {
            var id = reader.GetAttribute("id");
            var title = reader.GetAttribute("title");
            var hidden = string.Equals("true", reader.GetAttribute("hidden"), StringComparison.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(id))
            {
                id = title.ToSlug();
            }

            var topics = new List<Topic>();
            while (reader.Read())
            {
                if (!reader.IsStartElement() && reader.Name == "section")
                {
                    break;
                }
                if (reader.IsStartElement() && reader.Name == "item")
                {
                    var topic = ReadTopic(reader, root);
                    if (topic != null)
                    {
                        topics.Add(topic);
                    }
                }
            }

            var section = new TopicSection(id, title, hidden, topics);
            foreach (var topic in topics)
            {
                topic.Section = section;
            }
            return section;
        }

        private Topic ReadTopic(XmlReader reader, DirectoryPath root)
        {
            var file = reader.GetAttribute("file");
            var url = reader.GetAttribute("importurl");
            if (string.IsNullOrWhiteSpace(file) &&
                string.IsNullOrWhiteSpace(url))
            {
                return null;
            }

            var id = reader.GetAttribute("id");
            var title = reader.GetAttribute("title");
            var hidden = string.Equals("true", reader.GetAttribute("hidden"), StringComparison.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(id))
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    throw new InvalidOperationException("A remote document require a local ID.");
                }
                if (!string.IsNullOrWhiteSpace(file))
                {
                    id = Path.GetFileNameWithoutExtension(file);
                }
                else if(!string.IsNullOrWhiteSpace(title))
                {
                    id = title.ToSlug();
                }
                else
                {
                    throw new InvalidOperationException("Could not generate ID from topic.");
                }
            }

            var body = ReadBody(root, file, url) ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(body))
            {
                // Read the file and separate front matter from content.
                var content = _contentParser.ParseString(body);
                if (content != null)
                {
                    body = _contentProcessor.PreProcess(content.Body);
                    body = _contentConverter.ConvertToHtml(content, body);
                }

                // Process the content.
                body = _contentProcessor.PostProcess(body) ?? body;
            }

            return new Topic(id, title, body, hidden, file, url);
        }

        private string ReadBody(DirectoryPath root, string path, string uri)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                var file = _fileSystem.GetFile(root.CombineWithFilePath(path));
                if (file.Exists)
                {
                    using (var stream = file.OpenRead())
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            else if (!string.IsNullOrWhiteSpace(uri))
            {
                return new System.Net.WebClient().DownloadString(uri);
            }
            return null;
        }
    }
}

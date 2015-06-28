using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Cake.Core.IO;
using Cake.Web.Core.Documentation.Processing;
using Path = System.IO.Path;

namespace Cake.Web.Core.Documentation
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
                throw new ArgumentNullException("fileSystem");
            }
            if (contentParser == null)
            {
                throw new ArgumentNullException("contentParser");
            }
            if (contentConverter == null)
            {
                throw new ArgumentNullException("contentConverter");
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
            var message = string.Format("Could not find toc file ({0}).", path.FullPath);
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

            var section = new TopicSection(id, title, topics);
            foreach (var topic in topics)
            {
                topic.Section = section;
            }
            return section;
        }

        private Topic ReadTopic(XmlReader reader, DirectoryPath root)
        {
            var file = reader.GetAttribute("file");
            if (string.IsNullOrWhiteSpace(file))
            {
                return null;
            }

            var id = reader.GetAttribute("id");
            var title = reader.GetAttribute("title");
            var body = string.Empty;

            if (string.IsNullOrWhiteSpace(id))
            {
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

            var path = root.CombineWithFilePath(file);
            if (_fileSystem.Exist(path))
            {
                // Parse the file and separate front matter from content.
                var parseResult = _contentParser.Parse(path);
                if (parseResult != null)
                {
                    body = _contentConverter.ConvertToHtml(parseResult);
                }

                // Process the content.
                body = _contentProcessor.Process(body) ?? body;
            }

            return new Topic(id, title, body, file);
        }
    }
}

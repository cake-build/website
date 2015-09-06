using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Web.Core.Content
{
    public sealed class PackagesConfigContent
    {
        private readonly string _body;
        private readonly byte[] _data;

        public string Body
        {
            get { return _body; }
        }

        public byte[] Data
        {
            get { return _data; }
        }

        public PackagesConfigContent(string version)
        {
            _body = GenerateBody(version);
            _data = Encoding.UTF8.GetBytes(_body);
        }

        private static string GenerateBody(string version)
        {
            var builder = new StringBuilder();
            builder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            builder.AppendLine("<packages>");
            builder.AppendLine(string.Format("\t<package id=\"Cake\" version=\"{0}\" />", version));
            builder.AppendLine("</packages>");
            return builder.ToString();
        }
    }
}

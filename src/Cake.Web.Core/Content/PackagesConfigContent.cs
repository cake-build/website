using System.Text;

namespace Cake.Web.Core.Content
{
    public sealed class PackagesConfigContent
    {
        public string Body { get; }
        public byte[] Data { get; }

        public PackagesConfigContent(string version)
        {
            Body = GenerateBody(version);
            Data = Encoding.UTF8.GetBytes(Body);
        }

        private static string GenerateBody(string version)
        {
            var builder = new StringBuilder();
            builder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            builder.AppendLine("<packages>");
            builder.AppendLine($"\t<package id=\"Cake\" version=\"{version}\" />");
            builder.AppendLine("</packages>");
            return builder.ToString();
        }
    }
}

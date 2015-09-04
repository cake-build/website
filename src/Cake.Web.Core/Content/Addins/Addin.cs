using System;
using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Web.Docs;

namespace Cake.Web.Core.Content.Addins
{
    public sealed class Addin
    {
        public string Name { get; set; }
        public string PackageId { get; set; }
        public Uri Website { get; set; }
        public Uri Repository { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public List<string> Categories { get; set; }

        public Addin()
        {
            Categories = new List<string>();
        }
    }
}

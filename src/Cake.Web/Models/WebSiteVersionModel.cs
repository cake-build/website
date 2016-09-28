using System;
using System.Linq;
using System.Reflection;

namespace Cake.Web.Models
{
    public class WebSiteVersionModel
    {
        public string VersionInfo { get; }

        public static WebSiteVersionModel Current { get; } = new WebSiteVersionModel();

        public WebSiteVersionModel()
        {
            var assembly = typeof (WebSiteVersionModel).Assembly;
            var informationalVersion = assembly
                .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute))
                .OfType<AssemblyInformationalVersionAttribute>()
                .Select(version=>version.InformationalVersion)
                .FirstOrDefault() ?? assembly.GetName().Version.ToString();

            VersionInfo = $"{Environment.MachineName} {informationalVersion}";
        }
    }
}
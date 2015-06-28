using System.Web;
using System.Web.Mvc;
using Cake.Web.Core;
using Cake.Web.Docs;

namespace Cake.Web.Helpers.Api
{
    public static class AssemblyHelper
    {
        public static IHtmlString AssemblyName(this ApiServices context, DocumentedAssembly assembly)
        {
            return MvcHtmlString.Create(assembly.Name);
        }
    }
}
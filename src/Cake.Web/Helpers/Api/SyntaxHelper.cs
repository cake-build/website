using System.Web;
using Cake.Web.Core;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection;

namespace Cake.Web.Helpers.Api
{
    public static class SyntaxHelper
    {
        public static IHtmlString Syntax(this ApiServices context, DocumentedType type)
        {
            var signature = context.SignatureResolver.GetTypeSignature(type);
            return context.SyntaxRenderer.Render(signature);
        }

        public static IHtmlString Syntax(this ApiServices context, DocumentedMethod method)
        {
            return context.SyntaxRenderer.Render(method.Definition.GetMethodSignature(null));
        }
    }
}
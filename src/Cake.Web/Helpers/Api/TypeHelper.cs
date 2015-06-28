using System.Web;
using System.Web.Mvc;
using Cake.Web.Core;
using Cake.Web.Core.Rendering;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection;
using Mono.Cecil;

namespace Cake.Web.Helpers.Api
{
    public static class TypeHelper
    {
        public static IHtmlString TypeUrl(this ApiServices context, DocumentedType type)
        {
            if (type != null)
            {
                return TypeUrl(context, type.Identity);
            }
            return MvcHtmlString.Create(string.Empty);
        }

        public static IHtmlString TypeUrl(this ApiServices context, string identity)
        {
            if (!string.IsNullOrWhiteSpace(identity))
            {
                var url = context.UrlResolver.GetUrl(identity);
                if (url != null)
                {
                    return MvcHtmlString.Create(url);
                }
            }
            return MvcHtmlString.Create(string.Empty);
        }

        public static IHtmlString TypeName(this ApiServices context, DocumentedType type)
        {
            if (type != null)
            {
                var signature = context.SignatureResolver.GetTypeSignature(type);
                return context.SignatureRenderer.Render(signature, TypeRenderOption.Name);
            }
            return MvcHtmlString.Empty;
        }

        public static IHtmlString TypeLink(this ApiServices context, DocumentedType type)
        {
            if (type != null)
            {
                var signature = context.SignatureResolver.GetTypeSignature(type);
                return context.SignatureRenderer.Render(signature, TypeRenderOption.Name | TypeRenderOption.Link);
            }
            return MvcHtmlString.Empty;
        }

        public static IHtmlString TypeLink(this ApiServices context, TypeReference type)
        {
            if (type != null)
            {
                var signature = context.SignatureResolver.GetTypeSignature(type);
                return context.SignatureRenderer.Render(signature, TypeRenderOption.Name | TypeRenderOption.Link);
            }
            return MvcHtmlString.Empty;
        }

        public static IHtmlString TypeClassificationName(this ApiServices context, DocumentedType type)
        {
            if (type != null)
            {
                switch (type.TypeClassification)
                {
                    case TypeClassification.Class:
                        return MvcHtmlString.Create("Class");
                    case TypeClassification.Enum:
                        return MvcHtmlString.Create("Enumeration");
                    case TypeClassification.Interface:
                        return MvcHtmlString.Create("Interface");
                    case TypeClassification.Struct:
                        return MvcHtmlString.Create("Struct");
                    default:
                        return MvcHtmlString.Create("Unknown");
                }
            }
            return MvcHtmlString.Create(string.Empty);
        }

        public static IHtmlString TypeClassificationName(this HtmlHelper helper, TypeClassification classification)
        {
            switch (classification)
            {
                case TypeClassification.Class:
                    return MvcHtmlString.Create("Class");
                case TypeClassification.Enum:
                    return MvcHtmlString.Create("Enumeration");
                case TypeClassification.Interface:
                    return MvcHtmlString.Create("Interface");
                case TypeClassification.Struct:
                    return MvcHtmlString.Create("Struct");
                default:
                    return MvcHtmlString.Create("Unknown");
            }
        }
    }
}
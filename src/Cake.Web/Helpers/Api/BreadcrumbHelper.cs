using System.Web;
using Cake.Web.Core;
using Cake.Web.Core.Html;
using Cake.Web.Docs;

namespace Cake.Web.Helpers.Api
{
    public static class BreadcrumbHelper
    {
        public static IHtmlString BreadCrumb(this ApiServices context, DocumentedNamespace @namespace)
        {
            var breadcrumb = new Breadcrumb();
            breadcrumb.AppendApiRoot();
            breadcrumb.AppendNamespaces(context, @namespace, false);
            return breadcrumb.Render();
        }

        public static IHtmlString BreadCrumb(this ApiServices context, DocumentedType type)
        {
            var breadcrumb = new Breadcrumb();
            breadcrumb.AppendApiRoot();
            breadcrumb.AppendNamespaces(context, type.Namespace, true);
            breadcrumb.AppendType(context, type, false);
            return breadcrumb.Render();
        }

        public static IHtmlString BreadCrumb(this ApiServices context, DocumentedMethod method)
        {
            var breadcrumb = new Breadcrumb();
            breadcrumb.AppendApiRoot();
            breadcrumb.AppendNamespaces(context, method.Type.Namespace, true);
            breadcrumb.AppendType(context, method.Type, true);
            breadcrumb.AppendMethod(context, method);
            return breadcrumb.Render();
        }

        public static IHtmlString BreadCrumb(this ApiServices context, DocumentedProperty property)
        {
            var breadcrumb = new Breadcrumb();
            breadcrumb.AppendApiRoot();
            breadcrumb.AppendNamespaces(context, property.Type.Namespace, true);
            breadcrumb.AppendType(context, property.Type, true);
            breadcrumb.AppendProperty(context, property);
            return breadcrumb.Render();
        }
    }
}
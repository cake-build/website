using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cake.Web.Core.Rendering;
using Cake.Web.Docs;

namespace Cake.Web.Core.Html
{
    public static class BreadCrumbExtensions
    {
        public static void AppendApiRoot(this IBreadcrumbItem breadcrumb)
        {
            breadcrumb.Append(new BreadcrumbItem(MvcHtmlString.Create("API Reference"), new Uri("/api", UriKind.Relative)));
        }

        public static void AppendNamespaces(this IBreadcrumbItem breadcrumb, ApiServices context, DocumentedNamespace @namespace, bool link)
        {
            var current = CreateNamespace(context, @namespace, link);

            var parentNamespaces = new List<DocumentedNamespace>();
            var parent = @namespace.Tree.Parent;
            while (parent != null)
            {
                parentNamespaces.Add(parent.Namespace);
                parent = parent.Parent;
            }

            if (!link)
            {
                if (parentNamespaces.Count == 1)
                {
                    // Add the item in the drop down as an item.
                    breadcrumb.AppendNamespace(context, parentNamespaces[0], true);
                    // Append the current namespace.
                    breadcrumb.Append(current);
                }
                else if(parentNamespaces.Count > 1)
                {
                    // Create a dropdown with the first parent namespace as the owner.
                    var dropdown = new BreadcrumbDropdown(CreateNamespace(context, parentNamespaces[0], true));
                    foreach (var parentNamespace in parentNamespaces)
                    {
                        dropdown.AppendNamespace(context, parentNamespace, true);
                    }
                    // Append the dropdown.
                    breadcrumb.Append(dropdown);
                    // Append the current namespace.
                    breadcrumb.Append(current);
                }
                else
                {
                    // No parents. Just show the current namespace.
                    breadcrumb.Append(current);
                }
            }
            else
            {
                if (parentNamespaces.Count >= 1)
                {
                    // Create a dropdown with the current namespace as the owner.
                    var dropdown = new BreadcrumbDropdown(current);
                    dropdown.Append(current);
                    foreach (var parentNamespace in parentNamespaces)
                    {
                        dropdown.AppendNamespace(context, parentNamespace, true);
                    }
                    // Append the dropdown.
                    breadcrumb.Append(dropdown);
                }
                else
                {
                    // No parents. Just show the current namespace.
                    breadcrumb.Append(current);
                }
            }
        }

        public static void AppendNamespacesOld(this IBreadcrumbItem breadcrumb, ApiServices context, DocumentedNamespace @namespace, bool link)
        {
            var owner = CreateNamespace(context, @namespace, link);
            var dropdown = new BreadcrumbDropdown(owner);
            var parts = @namespace.Identity.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
            for (int index = 0; index < parts.Length - 1; index++)
            {
                var temp = string.Join(".", parts.Take(index + 1));
                var documentedNamespace = context.ModelResolver.FindNamespace(temp);
                if (documentedNamespace != null)
                {
                    dropdown.AppendNamespace(context, documentedNamespace, true);
                }
            }
            if (link)
            {
                dropdown.Append(owner);
            }

            if (dropdown.Count > 1)
            {
                breadcrumb.Append(dropdown);
            }
            else
            {
                breadcrumb.Append(owner);
            }
        }

        public static void AppendNamespace(this IBreadcrumbItem breadcrumb, ApiServices context, DocumentedNamespace @namespace, bool link)
        {
            breadcrumb.Append(CreateNamespace(context, @namespace, link));
        }

        private static BreadcrumbItem CreateNamespace(ApiServices context, DocumentedNamespace @namespace, bool link)
        {
            Uri uri = null;
            if (link)
            {
                uri = new Uri(context.UrlResolver.GetUrl(@namespace.Identity), UriKind.Relative);
            }
            return new BreadcrumbItem(MvcHtmlString.Create(@namespace.Name), uri);
        }

        public static void AppendType(this IBreadcrumbItem breadcrumb, ApiServices context, DocumentedType type, bool link)
        {
            Uri uri = null;
            if (link)
            {
                uri = new Uri(context.UrlResolver.GetUrl(type.Identity), UriKind.Relative);
            }
            var signature = context.SignatureResolver.GetTypeSignature(type);
            breadcrumb.Append(new BreadcrumbItem(context.SignatureRenderer.Render(signature, TypeRenderOption.Name), uri));
        }

        public static void AppendMethod(this IBreadcrumbItem breadcrumb, ApiServices context, DocumentedMethod method)
        {
            if (method.Definition.IsConstructor)
            {
                breadcrumb.Append(new BreadcrumbItem("Constructor"));
            }
            else
            {
                var methodSignature = context.SignatureResolver.GetMethodSignature(method);
                breadcrumb.Append(new BreadcrumbItem(context.SignatureRenderer.Render(methodSignature, MethodRenderOption.Name), null));
            }
        }

        public static void AppendProperty(this IBreadcrumbItem breadcrumb, ApiServices context, DocumentedProperty property)
        {
            breadcrumb.Append(new BreadcrumbItem(MvcHtmlString.Create(property.Definition.Name)));
        }
    }
}

using System;

namespace Cake.Web.Core.Rendering
{
    /// <summary>
    /// Represent options for rendering methods.
    /// </summary>
    [Flags]
    public enum MethodRenderOption
    {
        /// <summary>
        /// Render nothing.
        /// </summary>
        None = 1 << 0,

        /// <summary>
        /// Render link.
        /// </summary>
        Link = 1 << 1,

        /// <summary>
        /// Render method name.
        /// </summary>
        Name = 1 << 2,

        /// <summary>
        /// Render parameters.
        /// </summary>
        Parameters = 1 << 3,

        /// <summary>
        /// Render return type.
        /// </summary>
        ReturnType = 1 << 4,

        /// <summary>
        /// The name of the declaring type.
        /// </summary>
        TypeName = 1 << 5,

        /// <summary>
        /// Render namespace.
        /// </summary>
        TypeFullName = 1 << 6,

        /// <summary>
        /// Render like an extension method
        /// </summary>
        ExtensionMethod = 1 << 7,

        /// <summary>
        /// Render like a method alias.
        /// </summary>
        MethodAlias = 1 << 8,

        /// <summary>
        /// Render like a property alias.
        /// </summary>
        PropertyAlias = 1 << 9,

        /// <summary>
        /// The default rendering options.
        /// </summary>
        Default = Name | Parameters
    }
}
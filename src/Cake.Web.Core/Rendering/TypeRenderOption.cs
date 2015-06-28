using System;

namespace Cake.Web.Core.Rendering
{
    /// <summary>
    /// Represent options for rendering types.
    /// </summary>
    [Flags]
    public enum TypeRenderOption
    {
        /// <summary>
        /// Renders nothing.
        /// </summary>
        None = 1 << 0,

        /// <summary>
        /// Renders the link.
        /// </summary>
        Link = 1 << 1,

        /// <summary>
        /// Renders the namespace.
        /// </summary>
        Namespace = 1 << 2,

        /// <summary>
        /// Renders the type name.
        /// </summary>
        Name = 1 << 3,

        /// <summary>
        /// The default rendering options.
        /// </summary>
        Default = Namespace | Name
    }
}
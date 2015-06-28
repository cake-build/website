using System;

namespace Sparrow.Reflection
{
    /// <summary>
    /// Represents the description type.
    /// </summary>
    [Flags]
    public enum DescriptionType : int
    {
        /// <summary>
        /// The namespace.
        /// </summary>
        Namespace = 1 << 0,

        /// <summary>
        /// The type name
        /// </summary>
        TypeName = 1 << 1,
    }
}
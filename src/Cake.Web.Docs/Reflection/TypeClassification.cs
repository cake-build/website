namespace Cake.Web.Docs.Reflection
{
    /// <summary>
    /// Represents a type classification.
    /// </summary>
    public enum TypeClassification
    {
        /// <summary>
        /// Represents an unknown classification.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Represents a struct.
        /// </summary>
        Struct,

        /// <summary>
        /// Represents a class.
        /// </summary>
        Class,

        /// <summary>
        /// Represents an interface.
        /// </summary>
        Interface,

        /// <summary>
        /// Represents an enum.
        /// </summary>
        Enum
    }
}

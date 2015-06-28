namespace Cake.Web.Docs.Reflection
{
    /// <summary>
    /// Represents a method classification.
    /// </summary>
    public enum MethodClassification
    {
        /// <summary>
        /// Represents a constructor.
        /// </summary>
        Constructor,

        /// <summary>
        /// Represents a regular method.
        /// </summary>
        Method,

        /// <summary>
        /// Represents an extension method.
        /// </summary>
        ExtensionMethod,

        /// <summary>
        /// Represents an operator.
        /// </summary>
        Operator,

        /// <summary>
        /// Represents an event accessor.
        /// </summary>
        EventAccessor,
    }
}

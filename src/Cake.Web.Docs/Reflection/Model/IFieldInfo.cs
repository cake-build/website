using Mono.Cecil;

namespace Cake.Web.Docs.Reflection.Model
{
    /// <summary>
    /// Represents reflected field information.
    /// </summary>
    public interface IFieldInfo
    {
        /// <summary>
        /// Gets the type identity.
        /// </summary>
        /// <value>The identity.</value>
        string Identity { get; }

        /// <summary>
        /// Gets the field definition.
        /// </summary>
        /// <value>
        /// The field definition.
        /// </value>
        FieldDefinition Definition { get; }
    }
}

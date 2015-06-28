namespace Cake.Web.Docs.Reflection
{
    /// <summary>
    /// Represents available operator types.
    /// </summary>
    public enum OperatorType
    {
        /// <summary>
        /// Represents an unknown operator.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Represents an addition operator.
        /// </summary>
        Addition,

        /// <summary>
        /// Represents a subtraction operator.
        /// </summary>
        Subtraction,

        /// <summary>
        /// Represents a multiply operator.
        /// </summary>
        Multiply,

        /// <summary>
        /// Represents a division operator.
        /// </summary>
        Division,

        /// <summary>
        /// Represents a modulus operator.
        /// </summary>
        Modulus,

        /// <summary>
        /// Represents a bitwise or operator.
        /// </summary>
        BitwiseOr,

        /// <summary>
        /// Represents an exclusive or operator.
        /// </summary>
        ExclusiveOr,

        /// <summary>
        /// Represents a left shift operator.
        /// </summary>
        LeftShift,

        /// <summary>
        /// Represents a right shift operator.
        /// </summary>
        RightShift,

        /// <summary>
        /// Represents a logical not operator.
        /// </summary>
        LogicalNot,

        /// <summary>
        /// Represents an increment operator.
        /// </summary>
        Increment,

        /// <summary>
        /// Represents a decrement operator.
        /// </summary>
        Decrement,

        /// <summary>
        /// Represents a true operator.
        /// </summary>
        True,

        /// <summary>
        /// Represents a false operator.
        /// </summary>
        False
    }
}

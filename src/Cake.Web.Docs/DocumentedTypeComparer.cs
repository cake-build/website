using System;
using System.Collections.Generic;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Defines methods to support the comparison of <see cref="DocumentedType"/> for equality.
    /// </summary>
    public class DocumentedTypeComparer : IEqualityComparer<DocumentedType>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <see cref="DocumentedType"/> to compare.</param>
        /// <param name="y">The second object of type <see cref="DocumentedType"/> to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(DocumentedType x, DocumentedType y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return x.Identity.Equals(y.Identity, StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns a hash code for the specified instance.
        /// </summary>
        /// <param name="obj">The <see cref="DocumentedType"/> to get the hash code for.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(DocumentedType obj)
        {
            return obj.Identity.GetHashCode();
        }
    }
}

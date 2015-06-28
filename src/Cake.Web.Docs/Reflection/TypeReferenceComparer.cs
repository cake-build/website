using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Cake.Web.Docs.Reflection
{
    /// <summary>
    /// Defines methods to support the comparison of <see cref="TypeReference"/> for equality.
    /// </summary>
    public class TypeReferenceComparer : IEqualityComparer<TypeReference>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <see cref="TypeReference"/> to compare.</param>
        /// <param name="y">The second object of type <see cref="TypeReference"/> to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(TypeReference x, TypeReference y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return x.FullName.Equals(y.FullName, StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns a hash code for the specified instance.
        /// </summary>
        /// <param name="obj">The <see cref="TypeReference"/> to get the hash code for.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(TypeReference obj)
        {
            return obj.FullName.GetHashCode();
        }
    }
}

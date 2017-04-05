using System.Collections;
using System.Collections.Generic;

namespace Mentula.Utilities.Core.Collections
{
    /// <summary>
    /// Defines a base class for IEnumerable types that use arrays.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public abstract class ArrayEnumerable<T> : IEnumerable<T>
    {
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns> An <see cref="ArrayEnumerator{T}"/> object that can be used to iterate through the collection. </returns>
        public abstract ArrayEnumerator<T> GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns> An <see cref="IEnumerator"/> object that can be used to iterate through the collection. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns> An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection. </returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
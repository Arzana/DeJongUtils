using System.Collections;
using System.Collections.Generic;

namespace Mentula.Utilities.Collections
{
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public abstract class ArrayEnumerable<T> : IEnumerable<T>
    {
        public abstract ArrayEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
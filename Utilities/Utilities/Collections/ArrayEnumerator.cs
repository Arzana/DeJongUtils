using Mentula.Utilities.Logging;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mentula.Utilities.Collections
{
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ArrayEnumerator<T> : IEnumerator<T>
    {
        public T Current { get { return array[index]; } }
        object IEnumerator.Current { get { return Current; } }
        public bool Disposed { get; private set; }

        private T[] array;
        private int index;

        public ArrayEnumerator(T[] source)
        {
            array = source;
            Reset();
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                Reset();
                array = null;
                Disposed = true;
            }
        }

        public bool MovePrevious()
        {
            CheckDisposed();
            return --index >= 0;
        }

        public bool MoveNext()
        {
            CheckDisposed();
            return ++index < array.Length;
        }

        public void Reset()
        {
            CheckDisposed();
            index = -1;
        }

        private void CheckDisposed()
        {
            if (Disposed)
            {
                ObjectDisposedException e = new ObjectDisposedException("ArrayEnumerator");
                Log.Fatal(nameof(ArrayEnumerator), e);
                throw e;
            }
        }
    }

    public static class ArrayEnumerator
    {
        public static ArrayEnumerator<char> FromString(string source)
        {
            return new ArrayEnumerator<char>(source.ToCharArray());
        }
    }
}
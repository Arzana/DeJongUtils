using DeJong.Utilities.Core;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DeJong.Utilities.Core.Collections
{
    /// <summary>
    /// Defines an enumarator that can be used to iterate through a collection.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ArrayEnumerator<T> : IEnumerator<T>, IFullyDisposable
    {
        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public T Current { get { return array[index]; } }
        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        object IEnumerator.Current { get { return Current; } }
        /// <summary>
        /// Gets a value indicating if the <see cref="ArrayEnumerator{T}"/> has beed disposed.
        /// </summary>
        public bool Disposed { get; private set; }
        /// <inheritdoc/>
        public bool Disposing { get; private set; }

        private T[] array;
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayEnumerator{T}"/> class with a specified array to use.
        /// </summary>
        /// <param name="source"></param>
        public ArrayEnumerator(T[] source)
        {
            array = source;
            Reset();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (!(Disposed || Disposing))
            {
                Disposing = true;

                Reset();
                if (disposing) array = null;

                Disposing = false;
                Disposed = true;
            }
        }

        /// <summary>
        /// Decrease the enumerator to the previous element of the collection.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the enumerator was successfully decreased to the previous element;
        /// <see langword="false"/> if the enumerator has passed the beginning of the collection.
        /// </returns>
        /// <exception cref="LoggedException"> The enumerator was disposed before calling this method. </exception>
        public bool MovePrevious()
        {
            CheckDisposed();
            return --index >= 0;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the enumerator was successfully advanced to the next element;
        /// <see langword="false"/> if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="LoggedException"> The enumerator was disposed before calling this method. </exception>
        public bool MoveNext()
        {
            CheckDisposed();
            return ++index < array.Length;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, wich is before the first element in the collection.
        /// </summary>
        /// <exception cref="LoggedException"> The enumerator was disposed before calling this method. </exception>
        public void Reset()
        {
            CheckDisposed();
            index = -1;
        }

        private void CheckDisposed()
        {
            LoggedException.RaiseIf(Disposed, nameof(ArrayEnumerator), "Cannot excecute method!", new ObjectDisposedException("ArrayEnumerator"));
        }
    }

    /// <summary>
    /// Contains functions for building <see cref="ArrayEnumerator{T}"/>.
    /// </summary>
    public static class ArrayEnumerator
    {
        /// <summary>
        /// Creates a new <see cref="ArrayEnumerator{T}"/> from a <see cref="string"/> source.
        /// </summary>
        /// <param name="source"> The specific source. </param>
        /// <returns> An <see cref="ArrayEnumerator{T}"/>. </returns>
        public static ArrayEnumerator<char> FromString(string source)
        {
            return new ArrayEnumerator<char>(source.ToCharArray());
        }
    }
}
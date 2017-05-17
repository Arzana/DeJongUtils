namespace DeJong.Utilities.Threading
{
    using Core;
    using Core.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System;

    /// <summary>
    /// Represent a generic size thread safe collection of objects.
    /// </summary>
    /// <typeparam name="T"> The type of elements in the list. </typeparam>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    [DebuggerDisplay("{GetDebuggerString()}")]
    public class ThreadSafeList<T> : ArrayEnumerable<T>, IList<T>, IFullyDisposable
    {
        /// <summary>
        /// The current capacity of the list.
        /// </summary>
        public int Capacity
        {
            get
            {
                locker.EnterReadLock();
                int result = data.Length;
                locker.ExitReadLock();
                return result;
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get
            {
                locker.EnterReadLock();
                int result = size;
                locker.ExitReadLock();
                return result;
            }
        }

        /// <inheritdoc/>
        public bool IsReadOnly { get { return false; } }
        /// <inheritdoc/>
        public bool Disposed { get; private set; }
        /// <inheritdoc/>
        public bool Disposing { get; private set; }

        private const int SIZE_ADDER = 8;
        private readonly ReaderWriterLockSlim locker;
        private T[] data;
        private int size;

        /// <summary>
        /// Gets or sets a value at the specified index.
        /// </summary>
        /// <param name="index"> The secified index. </param>
        /// <returns> The value at the specified index. </returns>
        /// <exception cref="LoggedException"> The index was out of range. </exception>
        public T this[int index]
        {
            get
            {
                LoggedException.RaiseIf(index >= Count, nameof(ThreadSafeList<T>), "index out of range");

                locker.EnterReadLock();
                T result = data[index];
                locker.ExitReadLock();

                return result;
            }
            set
            {
                LoggedException.RaiseIf(index >= Count, nameof(ThreadSafeList<T>), "index out of range");

                locker.EnterWriteLock();
                data[index] = value;
                locker.ExitWriteLock();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeList{T}"/> class with a initial size of zero.
        /// </summary>
        public ThreadSafeList()
            : this(0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeList{T}"/> class with a specified initial size.
        /// </summary>
        /// <param name="initialCapacity"> The initial size of the list. </param>
        public ThreadSafeList(int initialCapacity)
        {
            locker = new ReaderWriterLockSlim();
            EnsureCapacity(initialCapacity);
        }

        /// <summary>
        /// Terminates this instance of <see cref="ThreadSafeList{T}"/>.
        /// </summary>
        ~ThreadSafeList()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the managed and unmanaged data of the <see cref="ThreadSafeList{T}"/>.
        /// </summary>
        /// <param name="disposing"> Whether to dispose unmanaged types. </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!(Disposed | Disposing))
            {
                Disposing = true;
                locker.Dispose();
                Disposing = false;
                Disposed = true;
            }
        }

        /// <summary>
        /// Gets the index of a specified item.
        /// </summary>
        /// <param name="item"> The item to search for. </param>
        /// <returns> Returns the index of the item, if the item is not found; -1. </returns>
        public int IndexOf(T item)
        {
            int result = -1;
            locker.EnterReadLock();

            for (int i = 0; i < size; i++)
            {
                if(data[i] == null && item == null)
                {
                    result = i;
                    break;
                }
                else if(data[i].Equals(item))
                {
                    result = i;
                    break;
                }
            }

            locker.ExitReadLock();
            return result;
        }

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index"> The position of the new item. </param>
        /// <param name="item"> The item to be added. </param>
        /// <exception cref="LoggedException"> The index was out of range. </exception>
        public void Insert(int index, T item)
        {
            LoggedException.RaiseIf(index >= Count, nameof(ThreadSafeList<T>), "index out of range");
            locker.EnterWriteLock();

            ShiftRight(index);
            data[index] = item;
            ++size;

            locker.ExitWriteLock();
        }

        /// <summary>
        /// Removes an item at the specified index.
        /// </summary>
        /// <param name="index"> The specified index. </param>
        /// <exception cref="LoggedException"> The index was out of range. </exception>
        public void RemoveAt(int index)
        {
            LoggedException.RaiseIf(index >= Count, nameof(ThreadSafeList<T>), "index out of range");
            locker.EnterWriteLock();

            ShiftLeft(index);
            --size;

            locker.ExitWriteLock();
        }

        /// <summary>
        /// Adds a specified item at the end of the list.
        /// </summary>
        /// <param name="item"> The item to be added. </param>
        public void Add(T item)
        {
            locker.EnterWriteLock();

            EnsureCapacity(size + 1);
            data[size] = item;
            ++size;

            locker.ExitWriteLock();
        }

        /// <summary>
        /// Clears the list and sets its capacity to zero.
        /// </summary>
        public void Clear()
        {
            locker.EnterWriteLock();

            data = new T[0];
            size = 0;

            locker.ExitWriteLock();
        }

        /// <summary>
        /// Returns whether the specified item is inside the list.
        /// </summary>
        /// <param name="item"> The specified item to check for. </param>
        /// <returns> <see langword="true"/> if the item has been found, otherwise; <see langword="false"/>. </returns>
        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        /// <summary>
        /// Copies the underlying data of the list to a specified array.
        /// </summary>
        /// <param name="array"> The specified destination array. </param>
        /// <param name="arrayIndex"> The index from where to start pasting. </param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            locker.EnterReadLock();
            Array.Copy(data, 0, array, arrayIndex, size);
            locker.ExitReadLock();
        }

        /// <summary>
        /// Removes a specified item from the list.
        /// </summary>
        /// <param name="item"> The item to be removed. </param>
        /// <returns> <see langword="true"/> if the item was successfully removed, otherwise; <see langword="false"/>. </returns>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1) return false;

            RemoveAt(index);
            return true;
        }

        /// <inheritdoc/>
        public override ArrayEnumerator<T> GetEnumerator()
        {
            return new ArrayEnumerator<T>(data);
        }

        private void ShiftLeft(int index)
        {
            Array.Copy(data, index + 1, data, index, data.Length - index - 1);
            data[size - 1] = default(T);
        }

        private void ShiftRight(int index)
        {
            EnsureCapacity(size + 1);
            Array.Copy(data, index, data, index + 1, data.Length - index - 1);
        }

        private void EnsureCapacity(int newCapacity)
        {
            if (data == null) data = new T[newCapacity];
            else if (data.Length < newCapacity) Array.Resize(ref data, newCapacity);
        }

        private string GetDebuggerString()
        {
            return $"{{Count ={Count}, Capacity={Capacity}}}";
        }
    }
}
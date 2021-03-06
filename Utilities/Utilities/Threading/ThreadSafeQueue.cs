﻿namespace DeJong.Utilities.Threading
{
    using Core;
    using Core.Collections;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    /// <summary>
    /// Represents a first-in, first-out thread safe collection of objects.
    /// </summary>
    /// <typeparam name="T"> The type of elements in the queue. </typeparam>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    [DebuggerDisplay("{GetDebuggerString()}")]
    public class ThreadSafeQueue<T> : LockableObject, ICollection
    {
        /// <summary>
        /// The current capacity of the queue.
        /// </summary>
        public int Capacity
        {
            get
            {
                EnterReadLock();
                int result = data.Length;
                ExitReadLock();
                return result;
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get
            {
                EnterReadLock();
                int result = size;
                ExitReadLock();
                return result;
            }
        }

        /// <inheritdoc/>
        public object SyncRoot { get { return null; } }
        /// <inheritdoc/>
        public bool IsSynchronized { get { return true; } }

        private const int SIZE_ADDER = 8;
        private T[] data;
        private int size, head;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeQueue{T}"/> class with default parameters.
        /// </summary>
        public ThreadSafeQueue()
            : this(SIZE_ADDER)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeQueue{T}"/> class woth a specific initial capacity.
        /// </summary>
        /// <param name="initialCapacity"></param>
        public ThreadSafeQueue(int initialCapacity)
        {
            data = new T[initialCapacity];
        }

        /// <summary>
        /// Enqueue's a specified item to the back of the queue.
        /// </summary>
        /// <param name="item"> The item to enqueue. </param>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public void Enqueue(T item)
        {
            EnterWriteLock();
            if (size == data.Length) SetCapacity(data.Length + SIZE_ADDER);

            data[(head + size) % data.Length] = item;
            ++size;

            ExitWriteLock();
        }

        /// <summary>
        /// Enqueue's multiple specified items to the back of the queue in the given order.
        /// </summary>
        /// <param name="items"> The items to enqueue. </param>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public void Enqueue(ICollection<T> items)
        {
            EnterWriteLock();
            if (size + items.Count >= data.Length) SetCapacity(data.Length + items.Count + SIZE_ADDER);

            foreach (T item in items)
            {
                data[(head + size) % data.Length] = item;
                ++size;
            }

            ExitWriteLock();
        }

        /// <summary>
        /// Enqueue's a specified item to the front of the queue.
        /// </summary>
        /// <param name="item"> The item to enqueue. </param>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public void SkipQueue(T item)
        {
            EnterWriteLock();
            if (size >= data.Length) SetCapacity(data.Length + SIZE_ADDER);
            if (--head < 0) head = data.Length - 1;

            data[head] = item;
            ++size;

            ExitWriteLock();
        }

        /// <summary>
        /// Dequeue's the first item in the queue.
        /// </summary>
        /// <returns> The item at the front of the queue. </returns>
        /// <exception cref="LoggedException"> The queue was empty. </exception>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public T Dequeue()
        {
            EnterWriteLock();
            if (size == 0)
            {
                ExitWriteLock();
                throw new InvalidOperationException("The queue is empty");
            }

            T result = data[head];
            data[head] = default(T);
            head = (head + 1) % data.Length;
            --size;

            ExitWriteLock();
            return result;
        }

        /// <summary>
        /// Attempts to dequeue the first item in the queue.
        /// </summary>
        /// <param name="item"> The item at the front of the queue, if the queue was empty; default(T). </param>
        /// <returns> Whether an item was successfully dequeued. </returns>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public bool TryDequeue(out T item)
        {
            EnterWriteLock();
            if (size == 0)
            {
                ExitWriteLock();
                item = default(T);
                return false;
            }

            item = data[head];
            data[head] = default(T);

            head = (head + 1) % data.Length;
            --size;

            ExitWriteLock();
            return true;
        }

        /// <summary>
        /// Attemps to drain the queue in a specified <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="desination"> The destination of the queue data. </param>
        /// <returns> The amount of items drained in the destination. </returns>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public int Drain(IList<T> desination)
        {
            EnterWriteLock();
            int added = size;

            while (size > 0)
            {
                desination.Add(data[head]);
                data[head] = default(T);
                head = (head + 1) % data.Length;
                --size;
            }

            ExitWriteLock();
            return added;
        }

        /// <summary>
        /// Gets the specified element without removing it.
        /// </summary>
        /// <param name="index"> The index of the specific element to peek. </param>
        /// <returns> The element at the specified position or default(T) when the queue was empty. </returns>
        /// <exception cref="LoggedException"> The index was out of bounds. </exception>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public T Peek(int index)
        {
            EnterWriteLock();

            if (size == 0)
            {
                ExitWriteLock();
                return default(T);
            }
            if (index >= size)
            {
                ExitWriteLock();
                LoggedException.Raise(nameof(ThreadSafeQueue<T>), "index must be smaller than the size of the queue", new IndexOutOfRangeException());
            }

            T result = data[(head + index) % data.Length];
            ExitWriteLock();
            return result;
        }

        /// <summary>
        /// Check whether a specified element is in the queue.
        /// </summary>
        /// <param name="item"> The specific element to check. </param>
        /// <returns> Whether the element was in the queue. </returns>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public bool Contains(T item)
        {
            EnterReadLock();
            for (int i = 0, ptr = head; i < size; i++, ptr = (ptr + 1) % data.Length)
            {
                if ((data[ptr] == null && item == null) || data[ptr].Equals(item))
                {
                    ExitReadLock();
                    return true;
                }
            }

            ExitReadLock();
            return false;
        }

        /// <summary>
        /// Converts the queue to an array.
        /// </summary>
        /// <returns> The queue as an array. </returns>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public T[] ToArray()
        {
            EnterReadLock();
            T[] result = new T[size];

            for (int i = 0, ptr = head; i < size; i++)
            {
                result[i] = data[ptr++];
                if (ptr >= data.Length) ptr = 0;
            }

            ExitReadLock();
            return result;
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public void Clear()
        {
            EnterWriteLock();

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = default(T);
            }

            head = 0;
            size = 0;

            ExitWriteLock();
        }

        /// <summary>
        /// Copies the underlying array to the specified array.
        /// </summary>
        /// <param name="array"> The destination array. </param>
        /// <param name="index"> The offset for the destination array. </param>
        public void CopyTo(Array array, int index)
        {
            T[] src = ToArray();
            Array.Copy(src, 0, array, index, src.Length);
        }

        /// <inheritdoc/>
        public ArrayEnumerator<T> GetEnumerator()
        {
            return new ArrayEnumerator<T>(ToArray());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void SetCapacity(int newCapacity)
        {
            if (size == 0)
            {
                data = new T[newCapacity];
                head = 0;
            }
            else
            {
                T[] newData = new T[newCapacity];

                if (head + size - 1 < data.Length) Array.Copy(data, head, newData, 0, size);
                else
                {
                    Array.Copy(data, head, newData, 0, data.Length - head);
                    Array.Copy(data, 0, newData, data.Length - head, size - (data.Length - head));
                }

                data = newData;
                head = 0;
            }
        }

        private string GetDebuggerString()
        {
            return Disposed ? "Disposed" : $"{{Count ={Count}, Capacity={Capacity}}}";
        }
    }
}
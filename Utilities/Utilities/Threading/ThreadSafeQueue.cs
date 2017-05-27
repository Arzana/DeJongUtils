namespace DeJong.Utilities.Threading
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
    public class ThreadSafeQueue<T> : ArrayEnumerable<T>, ICollection, IFullyDisposable
    {
        /// <summary>
        /// The current capacity of the queue.
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
        public object SyncRoot { get { return null; } }
        /// <inheritdoc/>
        public bool IsSynchronized { get { return true; } }
        /// <inheritdoc/>
        public bool Disposed { get; private set; }
        /// <inheritdoc/>
        public bool Disposing { get; private set; }

        private const int SIZE_ADDER = 8;
        private readonly ReaderWriterLockSlim locker;
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
            locker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            data = new T[initialCapacity];
        }
        /// <summary>
        /// Terminates this instance of <see cref="ThreadSafeQueue{T}"/>.
        /// </summary>
        ~ThreadSafeQueue()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the managed and unmanaged data of the <see cref="ThreadSafeQueue{T}"/>.
        /// </summary>
        /// <param name="disposing"> Whether to dispose unmanaged types. </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!(Disposed || Disposing))
            {
                Disposing = true;
                locker.Dispose();
                Disposing = false;
                Disposed = true;
            }
        }

        /// <summary>
        /// Enqueue's a specified item to the back of the queue.
        /// </summary>
        /// <param name="item"> The item to enqueue. </param>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public void Enqueue(T item)
        {
            RunInSafeWriteMode(Enqueue_internal, item, "Unable to enqueue item");
        }

        /// <summary>
        /// Enqueue's multiple specified items to the back of the queue in the given order.
        /// </summary>
        /// <param name="items"> The items to enqueue. </param>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public void Enqueue(ICollection<T> items)
        {
            RunInSafeWriteMode(Enqueue_internal, items, "Unable to enqueue multiple items");
        }

        /// <summary>
        /// Enqueue's a specified item to the front of the queue.
        /// </summary>
        /// <param name="item"> The item to enqueue. </param>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public void SkipQueue(T item)
        {
            RunInSafeWriteMode(SkipQueue_internal, item, "Unable to enqueue at head");
        }

        /// <summary>
        /// Dequeue's the first item in the queue.
        /// </summary>
        /// <returns> The item at the front of the queue. </returns>
        /// <exception cref="LoggedException"> The queue was empty. </exception>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public T Dequeue()
        {
            return RunInSafeWriteMode(Dequeue_internal, "Unable to dequeue item");
        }

        /// <summary>
        /// Attempts to dequeue the first item in the queue.
        /// </summary>
        /// <param name="item"> The item at the front of the queue, if the queue was empty; default(T). </param>
        /// <returns> Whether an item was successfully dequeued. </returns>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public bool TryDequeue(out T item)
        {
            return RunInSafeWriteMode(TryDequeue_internal, out item, "Unable to dequeue item");
        }

        /// <summary>
        /// Attemps to drain the queue in a specified <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="desination"> The destination of the queue data. </param>
        /// <returns> The amount of items drained in the destination. </returns>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public int TryDrain(IList<T> desination)
        {
            return RunInSafeWriteMode(TryDrain_internal, desination, "Unable to drain queue");
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
            return RunInSafeWriteMode(Peek_internal, index, "Unable to peek queue");
        }

        /// <summary>
        /// Check whether a specified element is in the queue.
        /// </summary>
        /// <param name="item"> The specific element to check. </param>
        /// <returns> Whether the element was in the queue. </returns>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public bool Contains(T item)
        {
            return RunInSafeReadMode(Contains_internal, item, "Unable to check for item");
        }

        /// <summary>
        /// Converts the queue to an array.
        /// </summary>
        /// <returns> The queue as an array. </returns>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public T[] ToArray()
        {
            return RunInSafeReadMode(ToArray_internal, "Unable to convert queue to array");
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        /// <exception cref="LoggedException"> An unhandled exception occured whilst excecuting the method. </exception>
        public void Clear()
        {
            RunInSafeWriteMode(Clear_internal, "Unable to clear queue");
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
        public override ArrayEnumerator<T> GetEnumerator()
        {
            return new ArrayEnumerator<T>(ToArray());
        }

        private void Clear_internal()
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = default(T);
            }

            head = 0;
            size = 0;
        }

        private T[] ToArray_internal()
        {
            T[] result = new T[size];

            for (int i = 0, ptr = head; i < size; i++)
            {
                result[i] = data[ptr++];
                if (ptr >= data.Length) ptr = 0;
            }

            return result;
        }

        private bool Contains_internal(T item)
        {
            for (int i = 0, ptr = head; i < size; i++, ptr = (ptr + 1) % data.Length)
            {
                if (data[ptr] == null && item == null) return true;
                else if (data[ptr].Equals(item)) return true;
            }

            return false;
        }

        private T Peek_internal(int index)
        {
            if (size == 0) return default(T);
            LoggedException.RaiseIf(index >= size, nameof(ThreadSafeQueue<T>), "index must be smaller than the size of the queue", new IndexOutOfRangeException());
            return data[(head + index) % data.Length];
        }

        private int TryDrain_internal(IList<T> desination)
        {
            int added = size;

            while (size > 0)
            {
                desination.Add(data[head]);
                data[head] = default(T);
                head = (head + 1) % data.Length;
                --size;
            }

            return added;
        }

        private bool TryDequeue_internal(out T item)
        {
            if (size == 0)
            {
                item = default(T);
                return false;
            }

            item = data[head];
            data[head] = default(T);

            head = (head + 1) % data.Length;
            --size;

            return true;
        }

        private T Dequeue_internal()
        {
            if (size == 0) throw new InvalidOperationException("The queue is empty");

            T result = data[head];
            data[head] = default(T);
            head = (head + 1) % data.Length;
            --size;

            return result;
        }

        private void SkipQueue_internal(T item)
        {
            if (size >= data.Length) SetCapacity(data.Length + SIZE_ADDER);
            if (--head < 0) head = data.Length - 1;
            data[head] = item;
            ++size;
        }

        private void Enqueue_internal(ICollection<T> items)
        {
            if (size + items.Count >= data.Length) SetCapacity(data.Length + items.Count + SIZE_ADDER);
            foreach (T item in items)
            {
                data[(head + size) % data.Length] = item;
                ++size;
            }
        }

        private void Enqueue_internal(T item)
        {
            if (size == data.Length) SetCapacity(data.Length + SIZE_ADDER);
            data[(head + size) % data.Length] = item;
            ++size;
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

        private TResult RunInSafeReadMode<TResult, TArg>(Func<TArg, TResult> func, TArg arg, string errorMsg)
        {
            locker.EnterReadLock();

            try
            {
                return func.Invoke(arg);
            }
            catch (Exception e)
            {
                LoggedException.Raise(nameof(ThreadSafeQueue<T>), errorMsg, e);
                return default(TResult);
            }
            finally
            {
                locker.ExitReadLock();
            }
        }

        private TResult RunInSafeReadMode<TResult>(Func<TResult> func, string errorMsg)
        {
            locker.EnterReadLock();

            try
            {
                return func.Invoke();
            }
            catch (Exception e)
            {
                LoggedException.Raise(nameof(ThreadSafeQueue<T>), errorMsg, e);
                return default(TResult);
            }
            finally
            {
                locker.ExitReadLock();
            }
        }

        private TResult RunInSafeWriteMode<TResult, TArg>(Func<TArg, TResult> func, TArg arg, string errorMsg)
        {
            locker.EnterWriteLock();

            try
            {
                return func.Invoke(arg);
            }
            catch (Exception e)
            {
                LoggedException.Raise(nameof(ThreadSafeQueue<T>), errorMsg, e);
                return default(TResult);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }

        private TResult RunInSafeWriteMode<TResult>(Func<TResult> func, string errorMsg)
        {
            locker.EnterWriteLock();

            try
            {
                return func.Invoke();
            }
            catch (Exception e)
            {
                LoggedException.Raise(nameof(ThreadSafeQueue<T>), errorMsg, e);
                return default(TResult);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }

        private void RunInSafeWriteMode<TArg>(Action<TArg> func, TArg arg, string errorMsg)
        {
            locker.EnterWriteLock();

            try
            {
                func.Invoke(arg);
            }
            catch (Exception e)
            {
                LoggedException.Raise(nameof(ThreadSafeQueue<T>), errorMsg, e);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }

        private TResult RunInSafeWriteMode<TResult, TArg>(OutFunc<TResult, TArg> func, out TArg arg, string errorMsg)
        {
            locker.EnterWriteLock();

            try
            {
                return func.Invoke(out arg);
            }
            catch (Exception e)
            {
                LoggedException.Raise(nameof(ThreadSafeQueue<T>), errorMsg, e);
                arg = default(TArg);
                return default(TResult);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }

        private void RunInSafeWriteMode(Action func, string errorMsg)
        {
            locker.EnterWriteLock();

            try
            {
                func.Invoke();
            }
            catch (Exception e)
            {
                LoggedException.Raise(nameof(ThreadSafeQueue<T>), errorMsg, e);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }

        private string GetDebuggerString()
        {
            return $"{{Count ={Count}, Capacity={Capacity}}}";
        }
    }
}
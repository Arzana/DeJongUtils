namespace DeJong.Utilities.Threading
{
    using Core;
    using Logging;
    using System.Threading;

    /// <summary>
    /// Defines a base class for object that need to be locked and unlocked often.
    /// </summary>
    public abstract class LockableObject : IFullyDisposable
    {
        /// <summary>
        /// Gets or sets the maximum time the <see cref="LockableObject"/> can wait before entering lock mode.
        /// </summary>
        public int LockTimeout { get; set; }

        /// <inheritdoc/>
        public bool Disposed { get; protected set; }
        /// <inheritdoc/>
        public bool Disposing { get; protected set; }

        private readonly ReaderWriterLockSlim locker;
        private bool readActive, writeActive;

        /// <summary>
        /// Initializes a new instance of the <see cref="LockableObject"/> class.
        /// </summary>
        protected LockableObject()
        {
            locker = new ReaderWriterLockSlim();
            LockTimeout = 100;
        }

        /// <summary>
        /// Disposes and finalizes this <see cref="LockableObject"/>.
        /// </summary>
        ~LockableObject()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(false);
        }

        /// <summary>
        /// Diposes the object.
        /// </summary>
        /// <param name="disposing"> Not used. </param>
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
        /// Attempts to enter a read lock.
        /// </summary>
        /// <exception cref="LoggedException"> Method timed out. </exception>
        protected void EnterReadLock()
        {
            LoggedException.RaiseIf(!locker.TryEnterReadLock(LockTimeout), nameof(LockableObject), "Unable to enter read lock");
            readActive = true;
        }

        /// <summary>
        /// Attemps to enter a write lock.
        /// </summary>
        /// <exception cref="LoggedException"> Method timed out. </exception>
        protected void EnterWriteLock()
        {
            LoggedException.RaiseIf(!locker.TryEnterWriteLock(LockTimeout), nameof(LockableObject), "Unable to enter write lock");
            writeActive = true;
        }

        /// <inheritdoc cref="ReaderWriterLockSlim.ExitReadLock"/>
        protected void ExitReadLock()
        {
            if (readActive)
            {
                locker.ExitReadLock();
                readActive = false;
            }
        }
        /// <inheritdoc cref="ReaderWriterLockSlim.ExitWriteLock"/>
        protected void ExitWriteLock()
        {
            if (writeActive)
            {
                locker.ExitWriteLock();
                writeActive = false;
            }
        }
    }
}
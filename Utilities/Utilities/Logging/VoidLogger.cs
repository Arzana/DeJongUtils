namespace DeJong.Utilities.Logging
{
    using Core;
    using Threading;

    /// <summary>
    /// Defines a handler that voids logged messages.
    /// </summary>
    public sealed class VoidLogger : IFullyDisposable
    {
        /// <summary>
        /// Gets or sets whether the <see cref="VoidLogger"/> should automaticly update itself.
        /// </summary>
        public bool AutoUpdate
        {
            get { return autoUpd; }
            set
            {
                if (value == autoUpd) return;

                if (value)
                {
                    updThread = new StopableThread(null, null, Update, $"{nameof(ConsoleLogger)}Thread");
                    updThread.Start();
                }
                else
                {
                    if (updThread != null) updThread.Stop();
                }

                autoUpd = value;
            }
        }

        /// <inheritdoc/>
        public bool Disposed { get; private set; }
        /// <inheritdoc/>
        public bool Disposing { get; private set; }

        private StopableThread updThread;
        private bool autoUpd;

        /// <summary>
        /// Disposes and finalizes the <see cref="VoidLogger"/>
        /// </summary>
        ~VoidLogger()
        {
            Dispose(false);
        }

        /// <summary>
        /// Voids the logs.
        /// </summary>
        public void Update()
        {
            LogMessage msg;
            while ((msg = Log.PopLog()).Type != LogMessageType.None)
            {
                Log.FlushLog(msg);
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the managed and unmanaged data of the <see cref="VoidLogger"/>.
        /// </summary>
        /// <param name="disposing"> Whether the global log should be disposed. </param>
        private void Dispose(bool disposing)
        {
            if (!(Disposed || Disposing))
            {
                Disposing = true;

                if (disposing) Log.Dispose();
                if (updThread != null) updThread.Dispose();

                Disposing = false;
                Disposed = true;
            }
        }
    }
}
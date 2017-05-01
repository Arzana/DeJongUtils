namespace DeJong.Utilities.Threading
{
    using Logging;
    using Core;
    using System;
    using System.Threading;
    using System.Diagnostics;

    /// <summary>
    /// Contains a continually ticking STA background thread.
    /// </summary>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    [DebuggerDisplay("TID={thread.ManagedThreadId} {(running ? \"running\" : \"stopped\")}")]
    public sealed class StopableThread : IFullyDisposable
    {
        /// <inheritdoc/>
        public bool Disposed { get; private set; }
        /// <inheritdoc/>
        public bool Disposing { get; private set; }

        private Thread thread;
        private bool running, stop;
        private ThreadStart init, term, tick;

        /// <summary>
        /// Initializes a new instance of the <see cref="StopableThread"/> class.
        /// </summary>
        /// <param name="init"> The function to call when initialzing the run loop (can be <see langword="null"/>). </param>
        /// <param name="term"> The function to call when terminating the run loop (can be <see langword="null"/>). </param>
        /// <param name="tick"> The function that handles the thread tick, cannot be <see langword="null"/>. </param>
        /// <param name="name"> The name of the thread. </param>
        public StopableThread(ThreadStart init, ThreadStart term, ThreadStart tick, string name = null)
        {
            LoggedException.RaiseIf(tick == null, nameof(StopableThread), "Unable to create thread!", new ArgumentNullException("tick", "tick cannot be null!"));
            this.init = init;
            this.term = term;
            this.tick = tick;
            thread = ThreadBuilder.CreateSTA(Run);
            if (!string.IsNullOrEmpty(name)) thread.Name = name;
        }

        /// <summary>
        /// Disposes and finalizes the <see cref="StopableThread"/>.
        /// </summary>
        ~StopableThread()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopableThread"/> class and starts it.
        /// </summary>
        /// <param name="init"> The function to call when initialzing the run loop (can be <see langword="null"/>). </param>
        /// <param name="term"> The function to call when terminating the run loop (can be <see langword="null"/>). </param>
        /// <param name="tick"> The function that handles the thread tick, cannot be <see langword="null"/>. </param>
        /// <param name="name"> The name of the thread. </param>
        /// <returns> The newly created thread. </returns>
        public static StopableThread StartNew(ThreadStart init, ThreadStart term, ThreadStart tick, string name = null)
        {
            StopableThread result = new StopableThread(init, term, tick, name);
            result.Start();
            return result;
        }


        /// <summary>
        /// Disposes the thread unsafely.
        /// </summary>
        public void Dispose()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the thread safely or unsafely.
        /// </summary>
        /// <param name="disposing"> Whether the method should wait for the thread to exit. </param>
        private void Dispose(bool disposing)
        {
            if (!(Disposed || Disposing))
            {
                Disposing = true;

                if (disposing) StopWait();
                else Stop();

                Disposing = false;
                Disposed = true;
            }
        }

        /// <summary>
        /// Starts the thread.
        /// </summary>
        public void Start()
        {
            if (running)
            {
                Log.Warning(nameof(StopableThread), "Attempted to start already running thread, call ignored.");
                return;
            }

            thread.Start();
        }

        /// <summary>
        /// Stops the thread.
        /// </summary>
        /// <remarks>
        /// Thread will continue running parallel untill it has completed it's current task.
        /// </remarks>
        public void Stop()
        {
            if (!running)
            {
                Log.Warning(nameof(StopableThread), "Attempted to stop not running thread, call ignored.");
                return;
            }

            stop = true;
        }

        /// <summary>
        /// Stops the thread and wait for it to close.
        /// </summary>
        public void StopWait()
        {
            Stop();
            while (running) Thread.Sleep(100);
        }

        [STAThread]
        private void Run()
        {
            running = true;
            Log.Info(nameof(StopableThread), $"Initializing thread({thread.ManagedThreadId})");
            init?.Invoke();

            while (!stop)
            {
                tick();
                Thread.Sleep(10);
            }

            Log.Info(nameof(StopableThread), $"Terminating thread({thread.ManagedThreadId})");
            term?.Invoke();
            running = false;
        }
    }
}

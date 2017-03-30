namespace Mentula.Utilities.Threading
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
    public sealed class StopableThread : IDisposable
    {
        private Thread thread;
        private bool running, stop;
        private ThreadStart init, term, tick;

        /// <summary>
        /// Initializes a new instance of the <see cref="StopableThread"/> class.
        /// </summary>
        /// <param name="init"> The function to call when initialzing the run loop (can be <see langword="null"/>). </param>
        /// <param name="term"> The function to call when terminating the run loop (can be <see langword="null"/>). </param>
        /// <param name="tick"> The function that handles the thread tick, cannot be <see langword="null"/>. </param>
        public StopableThread(ThreadStart init, ThreadStart term, ThreadStart tick)
        {
            LoggedException.RaiseIf(tick == null, nameof(StopableThread), "Unable to create thread!", new ArgumentNullException("tick", "tick cannot be null!"));
            this.init = init;
            this.term = term;
            this.tick = tick;
            thread = ThreadBuilder.CreateSTA(Run);
        }

        ~StopableThread()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Disposes the thread safely.
        /// </summary>
        public void Dispose()
        {
            StopWait();
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
            if (init != null) init();

            while (!stop)
            {
                tick();
                Thread.Sleep(10);
            }

            Log.Info(nameof(StopableThread), $"Terminating thread({thread.ManagedThreadId})");
            if (term != null) term();
            running = false;
        }
    }
}

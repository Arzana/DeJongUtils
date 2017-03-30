namespace Mentula.Utilities.Threading
{
    using Logging;
    using Core;
    using System;
    using System.Threading;

    public sealed class StopAbleThread : IDisposable
    {
        private Thread thread;
        private bool running, stop;
        private ThreadStart init, term, tick;

        public StopAbleThread(ThreadStart init, ThreadStart term, ThreadStart tick)
        {
            LoggedException.RaiseIf(tick == null, nameof(StopAbleThread), "Unable to create thread!", new ArgumentNullException("tick", "tick cannot be null!"));
            this.init = init;
            this.term = term;
            this.tick = tick;
            thread = ThreadBuilder.CreateSTA(Run);
        }

        ~StopAbleThread()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            StopWait();
        }

        public void Start()
        {
            if (running)
            {
                Log.Warning(nameof(StopAbleThread), "Attempted to start already running thread, call ignored.");
                return;
            }

            thread.Start();
        }

        public void Stop()
        {
            stop = true;
        }

        public void StopWait()
        {
            Stop();
            while (running) Thread.Sleep(10);
        }

        [STAThread]
        private void Run()
        {
            running = true;
            Log.Info(nameof(StopAbleThread), $"Initializing thread({thread.ManagedThreadId})");
            if (init != null) init();

            while (!stop)
            {
                tick();
            }

            Log.Info(nameof(StopAbleThread), $"Terminating thread({thread.ManagedThreadId})");
            if (term != null) term();
            running = false;
        }
    }
}

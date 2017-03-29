namespace Mentula.Utilities.Logging
{
    using System;
    using System.Threading;

    public static partial class Log
    {
        private static EnsureDisposeObj obj;

        internal static void WaitTillStop()
        {
            stop = true;
            while (running) Thread.Sleep(100);
        }

        private class EnsureDisposeObj : IDisposable
        {
            public bool Disposed { get; private set; }
            public bool Disposing { get; private set; }

            private object locker;

            public EnsureDisposeObj()
            {
                locker = new object();
            }

            public void Dispose()
            {
                lock (locker)
                {
                    if (Disposed || Disposing) return;
                    Disposing = true;
                }

                WaitTillStop();
                Disposed = true;
            }
        }
    }
}
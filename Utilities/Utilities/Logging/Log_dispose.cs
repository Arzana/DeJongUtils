namespace Mentula.Utilities.Logging
{
    using System;

    public static partial class Log
    {
        private static EnsureDisposeObj obj;

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

                logThread.StopWait();
                Disposed = true;
            }
        }
    }
}
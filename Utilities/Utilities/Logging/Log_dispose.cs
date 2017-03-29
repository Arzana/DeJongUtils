namespace Mentula.Utilities.Logging
{
    using System;
    using System.Threading;
    using static NativeMethods;

    public static partial class Log
    {
        private static EnsureDisposeObj obj;
        private static ConsoleExitHandler handler;

        private static bool OnConsoleExit(CtrlType sig)
        {
            obj.Dispose();

            switch (sig)
            {
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                    return true;
                case CtrlType.CTRL_BREAK_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                default:
                    return false;
            }
        }

        private static void WaitTillStop()
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
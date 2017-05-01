namespace DeJong.Utilities.Threading
{
    using Logging;
    using System.Diagnostics;
    using System.Threading;

    /// <summary>
    /// Contains general functions for creating basic threads.
    /// </summary>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    public static class ThreadBuilder
    {
        internal static int ProcessID { get { return Process.GetCurrentProcess().Id; } }
        internal static int ThreadID { get { return Thread.CurrentThread.ManagedThreadId; } }

        /// <summary>
        /// Runs a specified function in a STA thread.
        /// </summary>
        /// <param name="function"> The function for the thread to process. </param>
        public static void RunInSTA(ThreadStart function)
        {
            CreateSTA(function).Start();
        }

        /// <summary>
        /// Creates a background thread with its appartment set to <see cref="ApartmentState.STA"/>.
        /// </summary>
        /// <param name="function"> The function for the thread to process. </param>
        public static Thread CreateSTA(ThreadStart function)
        {
            Thread t = CreateBackground(function);
            t.SetApartmentState(ApartmentState.STA);
            return t;
        }

        /// <summary>
        /// Runs a specified function in a background thread.
        /// </summary>
        /// <param name="function"> The function for the thread to process. </param>
        public static void RunInBackground(ThreadStart function)
        {
            CreateBackground(function).Start();
        }

        /// <summary>
        /// Creates a thread with <see cref="Thread.IsBackground"/> set to <see langword="true"/>.
        /// </summary>
        /// <param name="func"> The function for the thread to process. </param>
        public static Thread CreateBackground(ThreadStart func)
        {
            Thread t = new Thread(func) { IsBackground = true };
            Log.Info(nameof(ThreadBuilder), $"Created background thread({t.ManagedThreadId})");
            return t;
        }
    }
}
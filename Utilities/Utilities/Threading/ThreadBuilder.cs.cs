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
        /// <param name="name"> The name of the thread (Optional). </param>
        public static Thread CreateSTA(ThreadStart function, string name = "")
        {
            Thread t = CreateBackground(function, name);
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
        /// <param name="name"> The name of the thread (Optional). </param>
        public static Thread CreateBackground(ThreadStart func, string name = "")
        {
            Thread t = new Thread(func) { IsBackground = true };
            if (!string.IsNullOrEmpty(name))
            {
                t.Name = name;
                Log.Info(nameof(ThreadBuilder), $"Created background thread '{t.Name}'({t.ManagedThreadId})");
            }
            else Log.Info(nameof(ThreadBuilder), $"Created background thread({t.ManagedThreadId})");
            return t;
        }
    }
}
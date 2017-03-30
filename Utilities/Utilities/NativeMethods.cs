namespace Mentula.Utilities
{
    using Logging;
    using System;
    using System.Runtime.InteropServices;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal static class NativeMethods
    {
        internal delegate bool ConsoleExitHandler(CtrlType sig);

        internal enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        internal static void AddConsoleHandle(ConsoleExitHandler handler)
        {
            if (SetConsoleCtrlHandler(handler, true))
            {
                Log.Info(nameof(Console), $"Added {handler?.Method.Name} to the console handlers");
            }
            else Log.Error(nameof(Console), $"Could not add handler errorcode: {GetLastError()}");
        }

        internal static void RemoveConsoleHandle(ConsoleExitHandler handler)
        {
            if (SetConsoleCtrlHandler(handler, false))
            {
                Log.Info(nameof(Console), $"Removed {handler?.Method.Name} to the console handlers");
            }
            else Log.Error(nameof(Console), $"Could not remove handler errorcode: {GetLastError()}");
        }

        [DllImport("Kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleExitHandler handler, bool add);

        [DllImport("Kernell32.dll")]
        private static extern int GetLastError();
    }
}

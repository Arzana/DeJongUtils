namespace Mentula.Utilities
{
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

        [DllImport("Kernel32.dll")]
        internal static extern bool SetConsoleCtrlHandler(ConsoleExitHandler handler, bool add);
    }
}

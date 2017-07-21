namespace DeJong.Utilities.Logging
{
    using Core;
    using System;

    public static partial class Log
    {
        private static bool loggerActive;
        private static event Action loggerDispose;

        internal static void AddLogger(IDisposable sender)
        {
            LoggedException.RaiseIf(loggerActive, nameof(ConsoleLogger), "A logger is already active!");
            loggerActive = true;
            loggerDispose += sender.Dispose;
        }

        internal static void RemoveLogger()
        {
            if (!loggerActive) Warning(nameof(Log), "Attempted to remove non excisting logger!");
            loggerActive = false;
            loggerDispose = null;
        }

        internal static void Dispose()
        {
            if (loggerActive) loggerDispose();
            logThread?.Dispose();
            garbage.Dispose();
            msgbuffer.Dispose();
            listener.Dispose();
        }
    }
}
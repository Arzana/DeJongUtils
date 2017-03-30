namespace Mentula.Utilities.Logging
{
    using Collections;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Threading;

    public static partial class Log
    {
        private static StopableThread logThread;
        private static List<LogMessage> preBuffer;
        private static Queue<LogMessage> msgbuffer;

        private static LogTraceListener listener;

        static Log()
        {
            obj = new EnsureDisposeObj();

            preBuffer = new List<LogMessage>();
            msgbuffer = new Queue<LogMessage>();

            AddDebug();

            logThread = new StopableThread(null, null, PipeTick);
            logThread.Start();
        }

        [Conditional("DEBUG")]
        private static void AddDebug()
        {
            System.Diagnostics.Debug.Listeners.Add(listener = new LogTraceListener());
            EventLog.GetEventLogs().First((l) => l.Log == "Application").EntryWritten += LogEvent;
        }

        private static void Message(LogMessageType type, string tag, string message)
        {
            lock (preBuffer)
            {
                preBuffer.Add(new LogMessage(type, ThreadBuilder.GetCurrentProcessId(), ThreadBuilder.GetCurrentThreadId(), tag, message));
            }
        }

        [STAThread]
        private static void PipeTick()
        {
            lock (preBuffer)
            {
                lock (msgbuffer)
                {
                    while (preBuffer.Count > 0)
                    {
                        msgbuffer.Enqueue(GetNext());
                    }
                }
            }
        }

        private static LogMessage GetNext()
        {
            LogMessage result = preBuffer[0];

            for (int i = 0; i < preBuffer.Count; i++)
            {
                LogMessage cur = preBuffer[i];
                if ((int)cur.Type > (int)result.Type) result = cur;
            }

            preBuffer.Remove(result);
            return result;
        }

        private static void LogEvent(object sender, EntryWrittenEventArgs e)
        {
            Verbose(e.Entry.Source, e.Entry.Message);
        }

        private static void LogException(string tag, int pId, int tId, Exception e)
        {
            Fatal_private(tag, pId, tId, $"Exception: {e.GetType().Name}");
            Fatal_private(tag, pId, tId, $"Full name: {e.GetType().AssemblyQualifiedName}");
            Fatal_private(tag, pId, tId, $"Message: {e.Message}");

            if (e.Data.Count > 0)
            {
                Fatal_private(tag, pId, tId, $"Additional information:");
                foreach (object cur in e.Data)
                {
                    Fatal_private(tag, pId, tId, cur.ToString());
                }
            }

            if (!string.IsNullOrEmpty(e.StackTrace))
            {
                Fatal_private(tag, pId, tId, "Stacktrace:");
                Fatal_private(tag, pId, tId, e.StackTrace);
            }

            if (e.InnerException != null)
            {
                Fatal_private(tag, pId, tId, "Inner exception:");
                LogException(tag, pId, tId, e.InnerException);
            }
        }

        private static void Fatal_private(string tag, int pId, int tId, string message)
        {
            preBuffer.Add(new LogMessage(LogMessageType.Fatal, pId, tId, tag, message));
        }
    }
}
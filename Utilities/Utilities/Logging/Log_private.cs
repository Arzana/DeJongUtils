namespace DeJong.Utilities.Logging
{
    using Core.Collections;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Threading;

    public static partial class Log
    {
        private static StopableThread logThread;
        private static ThreadSafeQueue<LogMessage> garbage;
        private static List<LogMessage> preBuffer;
        private static ThreadSafeQueue<LogMessage> msgbuffer;
        private static LogTraceListener listener;
        private static int recycleBufferSize;
        private static bool autoProcess;

        static Log()
        {
            garbage = new ThreadSafeQueue<LogMessage>();
            preBuffer = new List<LogMessage>();
            msgbuffer = new ThreadSafeQueue<LogMessage>();

            AddDebug();
            AutoProcess = false;
            RecycleBufferSize = 64;
        }

        [Conditional("DEBUG")]
        private static void AddDebug()
        {
            System.Diagnostics.Debug.Listeners.Add(listener = new LogTraceListener());
            EventLog.GetEventLogs().First((l) => l.Log == "Application").EntryWritten += LogEvent;
        }

        private static void Message(LogMessageType type, string tag, string message)
        {
            if (garbage.Disposed)
            {
                // If an object that logs in its finalizer disposes before the log
                // the logs that come after that need to be removed because the garbage
                // buffer and message buffer will have been disposed.
                Dispose();
                return;
            }

            lock (preBuffer)
            {
                LogMessage msg;
                if (garbage.TryDequeue(out msg)) msg.ReInit(type, ThreadBuilder.ProcessID, ThreadBuilder.ThreadID, tag, message);
                else msg = new LogMessage(type, ThreadBuilder.ProcessID, ThreadBuilder.ThreadID, tag, message);

                preBuffer.Add(msg);
            }
        }

        private static void SetLoggingThread()
        {
            if (autoProcess)
            {
                if (logThread == null) logThread = StopableThread.StartNew(null, null, PipeTick, "DeJong Logging");
                else if (!logThread.Running) logThread.Start();
            }
            else if (logThread != null && logThread.Running)
            {
                logThread.Stop();
            }
        }

        private static void PipeTick()
        {
            while (preBuffer.Count > 0)
            {
                lock (preBuffer)
                {
                    LogMessage msg = GetNext();
                    msg.SetMsgPreffix();
                    msg.SetMsgSuffix();
                    msgbuffer.Enqueue(msg);
                }
            }
        }

        private static LogMessage GetNext()
        {
            LogMessage result = preBuffer[0];
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
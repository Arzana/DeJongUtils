namespace DeJong.Utilities.Logging
{
    using Core.Collections;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using Threading;

    public static partial class Log
    {
        private static StopableThread logThread;
        private static ThreadSafeQueue<LogMessage> garbage;
        private static List<LogMessage> preBuffer;
        private static ThreadSafeQueue<LogMessage> msgbuffer;
        private static LogTraceListener listener;
        private static int recycleBufferSize;
        private static StackTrace trace;

        static Log()
        {
            obj = new EnsureDisposeObj();

            garbage = new ThreadSafeQueue<LogMessage>();
            preBuffer = new List<LogMessage>();
            msgbuffer = new ThreadSafeQueue<LogMessage>();

            AddDebug();
            RecycleBufferSize = 64;

            logThread = StopableThread.StartNew(null, null, PipeTick, "LoggingPipelineThread");
        }

        [Conditional("DEBUG")]
        private static void AddDebug()
        {
            System.Diagnostics.Debug.Listeners.Add(listener = new LogTraceListener());
            EventLog.GetEventLogs().First((l) => l.Log == "Application").EntryWritten += LogEvent;
            trace = new StackTrace(true);
        }

        private static void Message(LogMessageType type, string tag, string message)
        {
            lock (preBuffer)
            {
                LogMessage msg;
                if (garbage.TryDequeue(out msg)) msg.ReInit(type, ThreadBuilder.ProcessID, ThreadBuilder.ThreadID, tag, message);
                else msg = new LogMessage(type, ThreadBuilder.ProcessID, ThreadBuilder.ThreadID, tag, message);

                preBuffer.Add(msg);
            }
        }

        [STAThread]
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
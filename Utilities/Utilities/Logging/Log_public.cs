namespace Mentula.Utilities.Logging
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a general logging pipeline.
    /// </summary>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    public static partial class Log
    {
        /// <summary>
        /// Logs a message with debug priority.
        /// </summary>
        /// <param name="tag"> The tag of the caller. </param>
        /// <param name="message"> The specified message to be logged. </param>
        [Conditional("DEBUG")]  // TODO: Fix
        public static void Debug(string tag, string message) => Message(LogMessageType.Debug, tag, message);
        /// <summary>
        /// Logs a message with error priority.
        /// </summary>
        /// <param name="tag"> The tag of the caller. </param>
        /// <param name="message"> The specified message to be logged. </param>
        public static void Error(string tag, string message) => Message(LogMessageType.Error, tag, message);
        /// <summary>
        /// Logs a message with info priority.
        /// </summary>
        /// <param name="tag"> The tag of the caller. </param>
        /// <param name="message"> The specified message to be logged. </param>
        public static void Info(string tag, string message) => Message(LogMessageType.Info, tag, message);
        /// <summary>
        /// Logs a message with verbose priority.
        /// </summary>
        /// <param name="tag"> The tag of the caller. </param>
        /// <param name="message"> The specified message to be logged. </param>
        public static void Verbose(string tag, string message) => Message(LogMessageType.Verbose, tag, message);
        /// <summary>
        /// Logs a message with warning priority.
        /// </summary>
        /// <param name="tag"> The tag of the caller. </param>
        /// <param name="message"> The specified message to be logged. </param>
        public static void Warning(string tag, string message) => Message(LogMessageType.Warning, tag, message);
        /// <summary>
        /// Logs a exception with fatal priority.
        /// </summary>
        /// <param name="tag"> The tag of the caller. </param>
        /// <param name="message"> The specified message to be logged. </param>
        public static void Fatal(string tag, Exception e)
        {
            int pid = Threading.GetCurrentProcessId(), tid = Threading.GetCurrentThreadId();

            lock (preBuffer)
            {
                Fatal_private(tag, pid, tid, $"Unhandled exception raised!");
                LogException(tag, pid, tid, e);
            }
        }

        /// <summary>
        /// Gets the next message to be displayed.
        /// </summary>
        /// <returns> The next message, if there is one; otherwise, <see cref="LogMessage.Empty"/>. </returns>
        public static LogMessage PopLog()
        {
            lock (msgbuffer)
            {
                return msgbuffer.Count > 0 ? msgbuffer.Dequeue() : LogMessage.Empty;
            }
        }
    }
}
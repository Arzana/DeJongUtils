namespace Mentula.Utilities.Logging
{
    using Collections;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Defines a container for log messages.
    /// </summary>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    [DebuggerDisplay("{Type}: {Message}")]
    public sealed class LogMessage
    {
        /// <summary>
        /// The type or priority of the message.
        /// </summary>
        public LogMessageType Type { get; private set; }
        /// <summary>
        /// The date and time of logging.
        /// </summary>
        public DateTime OccuredAt { get; private set; }
        /// <summary>
        /// The process Id of the caller.
        /// </summary>
        public int PId { get; private set; }
        /// <summary>
        /// The thread Id of the caller.
        /// </summary>
        public int TId { get; private set; }
        /// <summary>
        /// The tag of the caller.
        /// </summary>
        public string Tag { get; private set; }
        /// <summary>
        /// The actual message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// This message is returned by <see cref="Log.PopLog"/> when no message is left.
        /// </summary>
        public static readonly LogMessage Empty = new LogMessage();

        private static readonly char[] VALID_SUFFIX_CHARS = { '.', '?', '!' };

        private LogMessage()
        {
            Type = LogMessageType.None;
            OccuredAt = DateTime.MinValue;
            PId = -1;
            TId = -1;
            Tag = "NULL";
            Message = string.Empty;
        }

        internal LogMessage(LogMessageType type, int pId, int tId, string tag, string message)
        {
            Type = type;
            OccuredAt = DateTime.UtcNow;
            PId = pId;
            TId = tId;
            Tag = tag;
            Message = message;
        }

        /// <summary>
        /// Gets the visual representation of <see cref="OccuredAt"/>.
        /// </summary>
        /// <returns></returns>
        public string GetTimeStamp()
        {
            return OccuredAt.ToString("yyyy-MM-dd H:mm:ss");
        }

        /// <summary>
        /// Gets the line that should be printed with the specified output type.
        /// </summary>
        /// <param name="type"> The way the message should be represented. </param>
        /// <returns> A string containing the message in the specified output format. </returns>
        public string GetLogLine(LogOutputType type)
        {
            switch (type)
            {
                case LogOutputType.Brief:
                    return $"[{PId}][{Type}][{Tag}]: {Message}";
                case LogOutputType.Process:
                    return $"[{PId}]: {Message}";
                case LogOutputType.Tag:
                    return $"[{Type}][{Tag}]: {Message}";
                case LogOutputType.Raw:
                    return Message;
                case LogOutputType.Time:
                    return $"[{GetTimeStamp()}][{PId}][{Type}][{Tag}]: {Message}";
                case LogOutputType.ThreadTime:
                    return $"[{GetTimeStamp()}][{PId}/{TId}][{Type}][{Tag}]: {Message}";
                default:
                    return string.Empty;
            }
        }

        internal void SetMsgSuffix()
        {
            char suffix;

            switch (Type)
            {
                case LogMessageType.Verbose:
                case LogMessageType.Debug:
                case LogMessageType.Info:
                    suffix = '.';
                    break;
                case LogMessageType.Warning:
                case LogMessageType.Error:
                case LogMessageType.Fatal:
                    suffix = '!';
                    break;
                default:
                    suffix = '?';
                    break;
            }

            Message = Message.AppendIfNecessary(VALID_SUFFIX_CHARS, suffix);
        }
    }
}
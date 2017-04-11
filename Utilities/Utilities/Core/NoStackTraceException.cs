namespace DeJong.Utilities.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents errors that have no stack trace.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    [Serializable]
    public class NoStackTraceException : LoggedException
    {
        /// <inheritdoc/>
        public override string StackTrace { get { return null; } }
        /// <inheritdoc/>
        new public NoStackTraceException InnerException { get { return (NoStackTraceException)base.InnerException; } }
        /// <summary>
        /// Gets the original stacktrace.
        /// </summary>
        public string BaseStackTrace { get; private set; }
        /// <summary>
        /// Gets the original type of exception.
        /// </summary>
        public Type BaseType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoStackTraceException"/> class.
        /// </summary>
        public NoStackTraceException(string tag) : base(tag) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoStackTraceException"/> class with a specified messsage.
        /// </summary>
        /// <param name="tag"> The object that caused the exception. </param>
        /// <param name="message"> The specified message. </param>
        public NoStackTraceException(string tag, string message)
            : base(tag, message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoStackTraceException"/> class with a specified message and inner exception.
        /// </summary>
        /// <param name="tag"> The object that caused the exception. </param>
        /// <param name="message"> The specified message. </param>
        /// <param name="inner"> The exception that caused this exception. </param>
        public NoStackTraceException(string tag, string message, Exception inner)
            : base(tag, message, RetroGenException(inner))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoStackTraceException"/> class with a parent exception.
        /// </summary>
        /// <param name="tag"> The object that caused the exception. </param>
        /// <param name="source"> The exception to use as a base. </param>
        public NoStackTraceException(string tag, Exception source)
            : base(tag, source.Message, RetroGenException(source.InnerException))
        {
            BaseType = source.GetType();
            BaseStackTrace = source.StackTrace;
        }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("OriginalType", BaseType);
            info.AddValue("BaseStacktrace", BaseStackTrace);
            base.GetObjectData(info, context);
        }

        /// <inheritdoc/>
        protected NoStackTraceException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        private static NoStackTraceException RetroGenException(Exception e)
        {
            return e != null ? new NoStackTraceException(nameof(NoStackTraceException), e) : null;
        }
    }
}
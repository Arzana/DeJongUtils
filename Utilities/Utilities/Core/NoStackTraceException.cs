namespace Mentula.Utilities.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents errors that have no stack trace.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class NoStackTraceException : LoggedException
    {
        /// <inheritdoc/>
        public override string StackTrace { get { return null; } }
        /// <inheritdoc/>
        new public NoStackTraceException InnerException { get; private set; }
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
        public NoStackTraceException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoStackTraceException"/> class with a specified messsage.
        /// </summary>
        /// <param name="message"> The specified message. </param>
        public NoStackTraceException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoStackTraceException"/> class with a specified message and inner exception.
        /// </summary>
        /// <param name="message"> The specified message. </param>
        /// <param name="inner"> The exception that caused this exception. </param>
        public NoStackTraceException(string message, Exception inner)
            : base(message, RetroGenException(inner))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoStackTraceException"/> class with a parent exception.
        /// </summary>
        /// <param name="source"> The exception to use as a base. </param>
        public NoStackTraceException(Exception source)
            : base(source.Message, RetroGenException(source.InnerException))
        {
            BaseType = source.GetType();
            BaseStackTrace = source.StackTrace;
        }

        /// <inheritdoc/>
        protected NoStackTraceException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        private static NoStackTraceException RetroGenException(Exception e)
        {
            return e != null ? new NoStackTraceException(e) : null;
        }
    }
}
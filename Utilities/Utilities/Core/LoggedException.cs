using Mentula.Utilities.Logging;
using System;
using System.Runtime.Serialization;

namespace Mentula.Utilities.Core
{
    /// <summary>
    /// Represents errors that are logged to the <see cref="Log"/>.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    [Serializable]
    public class LoggedException : Exception
    {
        /// <inheritdoc/>
        public override string StackTrace { get { return string.IsNullOrEmpty(stackTrace) ? base.StackTrace : stackTrace; } }
        private string stackTrace;
        private string tag;
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggedException"/> class.
        /// </summary>
        /// <param name="tag"> The object that caused the exception. </param>
        public LoggedException(string tag)
            : base("An unspecified error occured")
        {
            Init(tag);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggedException"/> class with a specified message.
        /// </summary>
        /// <param name="tag"> The object that caused the exception. </param>
        /// <param name="message"> The specific message. </param>
        public LoggedException(string tag, string message) 
            : base(message)
        {
            Init(tag);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggedException"/> class with a specfied message and an inner exception.
        /// </summary>
        /// <param name="message"> The specific message. </param>
        /// <param name="tag"> The object that caused the exception. </param>
        /// <param name="inner"> The exception that caused this exception. </param>
        public LoggedException(string tag, string message, Exception inner) 
            : base(message, inner)
        {
            Init(tag);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tag", tag);
            base.GetObjectData(info, context);
        }

        /// <inheritdoc/>
        protected LoggedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Throws and logges a <see cref="LoggedException"/>.
        /// </summary>
        /// <param name="tag"> The object that caused the exception. </param>
        public static void Raise(string tag)
        {
            throw new LoggedException(tag);
        }

        /// <summary>
        /// Throws and logges a <see cref="LoggedException"/> with a specified message.
        /// </summary>
        /// <param name="tag"> The object that caused the exception. </param>
        /// <param name="message"> The specified message. </param>
        public static void Raise(string tag, string message)
        {
            throw new LoggedException(tag, message);
        }

        /// <summary>
        /// Throws and logges a <see cref="LoggedException"/> with a specified message and an inner exception.
        /// </summary>
        /// <param name="tag"> The object that caused the exception. </param>
        /// <param name="message"> The specified message. </param>
        /// <param name="inner"> The exception that caused the exception. </param>
        public static void Raise(string tag, string message, Exception inner)
        {
            throw new LoggedException(tag, message, inner);
        }

        /// <summary>
        /// Throws and logges a <see cref="LoggedException"/> if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"> The condition to check. </param>
        /// <param name="tag"> The object that caused the exception. </param>
        public static void RaiseIf(bool condition, string tag)
        {
            if (condition) Raise(tag);
        }

        /// <summary>
        /// Throws and logges a <see cref="LoggedException"/> with a specified message if the condition is <see langword="true"/>s.
        /// </summary>
        /// <param name="condition"> The condition to check. </param>
        /// <param name="tag"> The object that caused the exception. </param>
        /// <param name="message"> The specified message. </param>
        public static void RaiseIf(bool condition, string tag, string message)
        {
            if (condition) Raise(tag, message);
        }

        /// <summary>
        /// Throws and logges a <see cref="LoggedException"/> with a specified message and an inner exception if the condition is <see langword="true"/>s.
        /// </summary>
        /// <param name="condition"> The condition to check. </param>
        /// <param name="tag"> The object that caused the exception. </param>
        /// <param name="message"> The specified message. </param>
        /// <param name="inner"> The exception that caused the exception. </param>
        public static void RaiseIf(bool condition, string tag, string message, Exception inner)
        {
            if (condition) Raise(tag, message, inner);
        }

        private void Init(string tag)
        {
            stackTrace = Environment.StackTrace;
            Log.Fatal(this.tag = tag, this);
        }
    }
}
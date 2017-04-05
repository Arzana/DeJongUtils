namespace Mentula.Utilities.Core
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents errors that occur during event invokation.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    [Serializable]
    public sealed class InvokeException : LoggedException
    {
        /// <inheritdoc/>
        public override string StackTrace { get { return stackTrace + base.StackTrace; } }

        private string stackTrace;
        private TargetInvocationException ex;

        internal InvokeException(string tag, TargetInvocationException e)
            : base(tag, e.InnerException.Message, e.InnerException.InnerException)
        {
            ex = e;
            CreateStackTrace();
        }

        internal static void Raise(string tag, TargetInvocationException e)
        {
            throw new InvokeException(tag, e);
        }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Internal exception", ex);
            base.GetObjectData(info, context);
        }

        private void CreateStackTrace()
        {
            stackTrace = ex.InnerException.StackTrace;
            stackTrace += Environment.NewLine;
            stackTrace += ex.StackTrace;
            stackTrace += Environment.NewLine;
        }
    }
}
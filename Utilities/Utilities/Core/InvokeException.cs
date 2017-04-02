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
    public sealed class InvokeException : LoggedException
    {
        /// <inheritdoc/>
        public override string StackTrace { get { return stackTrace + base.StackTrace; } }

        private string stackTrace;
        private bool setObjData;
        private TargetInvocationException ex;

        internal InvokeException(TargetInvocationException e)
            : base(e.InnerException.Message, e.InnerException.InnerException)
        {
            ex = e;
            CreateStackTrace();
        }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (!setObjData)
            {
                setObjData = true;
                info.AddValue("Internal exception", ex);
            }
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
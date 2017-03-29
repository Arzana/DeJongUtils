using Mentula.Utilities.Logging;
using System;
using System.Runtime.Serialization;

namespace Mentula.Utilities.Core
{
    [Serializable]
    public class LoggedException : Exception
    {
        public LoggedException() { }
        public LoggedException(string message) : base(message) { }
        public LoggedException(string message, Exception inner) : base(message, inner) { }
        protected LoggedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public static void Raise(string tag)
        {
            LoggedException e = new LoggedException();
            Log.Fatal(tag, e);
            throw e;
        }

        public static void Raise(string tag, string message)
        {
            LoggedException e = new LoggedException(message);
            Log.Fatal(tag, e);
            throw e;
        }

        public static void Raise(string tag, string message, Exception inner)
        {
            LoggedException e = new LoggedException(message, inner);
            Log.Fatal(tag, e);
            throw e;
        }
    }
}

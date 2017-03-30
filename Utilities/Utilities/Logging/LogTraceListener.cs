namespace Mentula.Utilities.Logging
{
    using System.Diagnostics;
    using System.Text;

    internal sealed class LogTraceListener : TraceListener
    {
        private StringBuilder sb;

        public LogTraceListener()
        {
            sb = new StringBuilder();
        }

        public override void Write(string message)
        {
            Log.Verbose("Trace", message);
            Log.Warning(nameof(LogTraceListener), $"{nameof(LogTraceListener)} does not implement fully writing, use {nameof(WriteLine)} instead!");
        }

        public override void WriteLine(string message)
        {
            Log.Verbose("Trace", message);
        }
    }
}
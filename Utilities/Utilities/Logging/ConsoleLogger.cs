namespace Mentula.Utilities.Logging
{
    using System;
    using static NativeMethods;

    /// <summary>
    /// Defines a handler that displays logged messages to the console.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ConsoleLogger
    {
        /// <summary>
        /// The color used for logging verbose messages; default value=White
        /// </summary>
        public static ConsoleColor VerboseColor { get; set; } = ConsoleColor.White;
        /// <summary>
        /// The color used for logging debug messages; default value=Blue
        /// </summary>
        public static ConsoleColor DebugColor { get; set; } = ConsoleColor.Blue;
        /// <summary>
        /// The color used for logging info messages; default value=Green
        /// </summary>
        public static ConsoleColor InfoColor { get; set; } = ConsoleColor.Green;
        /// <summary>
        /// The color used for logging warning messages; default value=Yellow
        /// </summary>
        public static ConsoleColor WarningColor { get; set; } = ConsoleColor.Yellow;
        /// <summary>
        /// The color used for logging error messages; default value=Red
        /// </summary>
        public static ConsoleColor ErrorColor { get; set; } = ConsoleColor.Red;
        /// <summary>
        /// The color used for logging fatal messages; default value=DarkRed
        /// </summary>
        public static ConsoleColor FatalColor { get; set; } = ConsoleColor.DarkRed;

        private LogOutputType type;

        /// <summary>
        /// Creates a new instance of the <see cref="ConsoleLogger"/> class with a specified output type.
        /// </summary>
        /// <param name="type"> The output type of the logger. </param>
        public ConsoleLogger(LogOutputType type = LogOutputType.ThreadTime)
        {
            this.type = type;
        }

        /// <summary>
        /// Writes the buffer contents to the console.
        /// </summary>
        public void Update()
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            LogMessage msg;
            while ((msg = Log.PopLog()).Type != LogMessageType.None)
            {
                switch (msg.Type)
                {
                    case LogMessageType.Verbose: Console.ForegroundColor = VerboseColor; break;
                    case LogMessageType.Debug: Console.ForegroundColor = DebugColor; break;
                    case LogMessageType.Info: Console.ForegroundColor = InfoColor; break;
                    case LogMessageType.Warning: Console.ForegroundColor = WarningColor; break;
                    case LogMessageType.Error: Console.ForegroundColor = ErrorColor; break;
                    case LogMessageType.Fatal: Console.ForegroundColor = FatalColor; break;
                }

                Console.WriteLine(msg.GetLogLine(type));
            }

            Console.ForegroundColor = oldColor;
        }

        private static bool OnConsoleExit(CtrlType sig)
        {
            Log.WaitTillStop();

            switch (sig)
            {
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                    return true;
                case CtrlType.CTRL_BREAK_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                default:
                    return false;
            }
        }
    }
}
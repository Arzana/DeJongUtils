namespace DeJong.Utilities.Logging
{
    /// <summary>
    /// Defines the priority of a <see cref="LogMessage"/>.
    /// </summary>
    public enum LogMessageType : byte
    {
        /// <summary>
        /// Empty, no message available.
        /// </summary>
        None = 0,
        /// <summary>
        /// Lowest priority.
        /// </summary>
        Verbose = 1,
        /// <summary>
        /// Low priority (DEBUG only).
        /// </summary>
        Debug = 2,
        /// <summary>
        /// Normal priority.
        /// </summary>
        Info = 3,
        /// <summary>
        /// Medium priority.
        /// </summary>
        Warning = 4,
        /// <summary>
        /// High priority
        /// </summary>
        Error = 5,
        /// <summary>
        /// Only used for exceptions.
        /// </summary>
        Fatal = 6
    }
}
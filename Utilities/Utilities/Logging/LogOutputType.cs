namespace DeJong.Utilities.Logging
{
    /// <summary>
    /// Defines how a <see cref="LogMessage"/> should be printed.
    /// </summary>
    public enum LogOutputType : byte
    {
        /// <summary>
        /// Displays only the <see cref="LogMessage.PId"/>, <see cref="LogMessage.Type"/> and <see cref="LogMessage.Tag"/> in addition to the message.
        /// </summary>
        Brief,
        /// <summary>
        /// Displays only the <see cref="LogMessage.PId"/> in addition to the message.
        /// </summary>
        Process,
        /// <summary>
        /// Displays only the <see cref="LogMessage.Type"/> and <see cref="LogMessage.Tag"/> in addition to the message.
        /// </summary>
        Tag,
        /// <summary>
        /// Displays only the message.
        /// </summary>
        Raw,
        /// <summary>
        /// Displays only the <see cref="LogMessage.OccuredAt"/>, <see cref="LogMessage.PId"/>, <see cref="LogMessage.Type"/>, <see cref="LogMessage.Tag"/> in addition to the message.
        /// </summary>
        Time,
        /// <summary>
        /// Displays all properties of the message.
        /// </summary>
        ThreadTime,
    }
}
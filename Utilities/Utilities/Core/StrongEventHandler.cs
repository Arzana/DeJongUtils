namespace DeJong.Utilities.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a method that will handle strongly typed events.
    /// </summary>
    /// <typeparam name="Tsender"> The type of sender. </typeparam>
    /// <typeparam name="TArgs"> The type of arguments. </typeparam>
    /// <param name="sender"> The object that is attempting to invoke the handler. </param>
    /// <param name="e"> The argument for this call. </param>
    [SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "Using strong-typed StrongEventHandler event handler pattern.")]
    public delegate void StrongEventHandler<Tsender, TArgs>(Tsender sender, TArgs e) where TArgs : EventArgs;
}
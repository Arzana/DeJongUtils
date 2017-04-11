namespace DeJong.Utilities.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    //
    // Summary:
    //     Provides a mechanism for releasing unmanaged resources.To browse the .NET Framework
    //     source code for this type, see the Reference Source.
    /// <summary>
    /// Improves upon the mechanism for releasing unmanaged resources in the .NET Framework
    /// with implementing more information and options whilst releasing.
    /// </summary>
    public interface IFullyDisposable : IDisposable
    {
        /// <summary>
        /// Whether the object has been disposed.
        /// </summary>
        bool Disposed { get; }
        /// <summary>
        /// Whether the object is currently disposing.
        /// </summary>
        bool Disposing { get; }
    }
}
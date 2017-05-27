namespace DeJong.Utilities.Core
{
    using Logging;
    using System;
    using System.Reflection;

    /// <summary>
    /// Defines function for safe event invokation.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public static class EventInvoker
    {
        /// <summary>
        /// Dynamicly invokes the specified handler if it has been set.
        /// </summary>
        /// <param name="handler"> The event handler or <see langword="null"/>. </param>
        /// <param name="sender"> The object that is attempting to invoke the handler. </param>
        /// <param name="args"> The arguments for this call. </param>
        /// <exception cref="LoggedException"> The sender was <see langword="null"/>. </exception>
        /// <exception cref="InvokeException"> An unhandled exception was thrown in the event handler. </exception>
        public static void Invoke(EventHandler handler, object sender, EventArgs args)
        {
            LoggedException.RaiseIf(sender == null, nameof(EventInvoker), "sender cannot be null", new ArgumentNullException("sender"));

            if (handler != null)
            {
                Log.Debug($"{sender} called invoke for {handler.Method.Name}");

                try { handler.DynamicInvoke(new object[2] { sender, args }); }
                catch (TargetInvocationException e) { InvokeException.Raise(nameof(EventInvoker), e); }
            }
        }

        /// <summary>
        /// Dynamicly invokes the specified handler if it has been set.
        /// </summary>
        /// <typeparam name="TEventArgs"> The type of arguments. </typeparam>
        /// <param name="handler"> The event handler or <see langword="null"/>. </param>
        /// <param name="sender"> The object that is attempting to invoke the handler. </param>
        /// <param name="args"> The arguments for this call. </param>
        /// <exception cref="LoggedException"> The sender was <see langword="null"/>. </exception>
        /// <exception cref="InvokeException"> An unhandled excpetion was thrown in the event handler. </exception>
        public static void Invoke<TEventArgs>(EventHandler<TEventArgs> handler, object sender, TEventArgs args)
            where TEventArgs : EventArgs
        {
            LoggedException.RaiseIf(sender == null, nameof(EventInvoker), "sender cannot be null", new ArgumentNullException("sender"));

            if (handler != null)
            {
                Log.Debug($"{sender} called invoke for {handler.Method.Name}");

                try { handler.DynamicInvoke(new object[2] { sender, args }); }
                catch (TargetInvocationException e) { InvokeException.Raise(nameof(EventInvoker), e); }
            }
        }

        /// <summary>
        /// Dynamicly invokes the specified handler if it has been set.
        /// </summary>
        /// <typeparam name="TSender"> The type of sender. </typeparam>
        /// <typeparam name="TEventArgs"> The type of arguments. </typeparam>
        /// <param name="handler"> The event handler or <see langword="null"/>. </param>
        /// <param name="sender"> The object that is attempting to invoke the handler. </param>
        /// <param name="args"> The arguments for this call. </param>
        /// <exception cref="LoggedException"> The sender was <see langword="null"/>. </exception>
        /// <exception cref="InvokeException"> An unhandled exception was thrown in the event handler. </exception>
        public static void Invoke<TSender, TEventArgs>(StrongEventHandler<TSender, TEventArgs> handler, TSender sender, TEventArgs args)
            where TEventArgs : EventArgs
            where TSender : class
        {
            LoggedException.RaiseIf(sender == null, nameof(EventInvoker), "sender cannot be null", new ArgumentNullException("sender"));

            if (handler != null)
            {
                Log.Debug($"{sender} called invoke for {handler.Method.Name}");

                try { handler.DynamicInvoke(new object[2] { sender, args }); }
                catch (TargetInvocationException e) { InvokeException.Raise(nameof(EventInvoker), e); }
            }
        }

        /// <summary>
        /// Dynamicly invokes the specified handler if it has been set.
        /// </summary>
        /// <param name="handler"> The event handler or <see langword="null"/>. </param>
        /// <param name="sender"> The object that is attempting to invoke the handler or <see langword="null"/>. </param>
        /// <param name="args"> The arguments for this call. </param>
        public static void InvokeSafe(EventHandler handler, object sender, EventArgs args)
        {
            if (handler != null)
            {
                Log.Debug($"{sender ?? "NULL"} called invoke for {handler.Method.Name}");

                try { handler.DynamicInvoke(new object[2] { sender, args }); }
                catch (TargetInvocationException e) { Log.Error(nameof(EventInvoker), e.Message); }
            }
        }

        /// <summary>
        /// Dynamicly invokes the specified handler if it has been set.
        /// </summary>
        /// <typeparam name="TEventArgs"> The type of arguments. </typeparam>
        /// <param name="handler"> The event handler or <see langword="null"/>. </param>
        /// <param name="sender"> The object that is attempting to invoke the handler or <see langword="null"/>.  </param>
        /// <param name="args"> The arguments for this call. </param>
        public static void InvokeSafe<TEventArgs>(EventHandler<TEventArgs> handler, object sender, TEventArgs args)
            where TEventArgs : EventArgs
        {
            if (handler != null)
            {
                Log.Debug($"{sender ?? "NULL"} called invoke for {handler.Method.Name}");

                try { handler.DynamicInvoke(new object[2] { sender, args }); }
                catch (TargetInvocationException e) { Log.Error(nameof(EventInvoker), e.Message); }
            }
        }

        /// <summary>
        /// Dynamicly invokes the specified handler if it has been set.
        /// </summary>
        /// <typeparam name="TSender"> The type of sender. </typeparam>
        /// <typeparam name="TEventArgs"> The type of arguments. </typeparam>
        /// <param name="handler"> The event handler or <see langword="null"/>. </param>
        /// <param name="sender"> The object that is attempting to invoke the handler or <see langword="null"/>. </param>
        /// <param name="args"> The arguments for this call. </param>
        public static void InvokeSafe<TSender, TEventArgs>(StrongEventHandler<TSender, TEventArgs> handler, TSender sender, TEventArgs args)
            where TEventArgs : EventArgs
            where TSender : class
        {
            if (handler != null)
            {
                Log.Debug($"{(sender != null ? sender.ToString() : "NULL")} called invoke for {handler.Method.Name}");

                try { handler.DynamicInvoke(new object[2] { sender, args }); }
                catch (TargetInvocationException e) { Log.Error(nameof(EventInvoker), e.Message); }
            }
        }
    }
}
namespace DeJong.Utilities.Core
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public delegate TResult OutFunc<out TResult, T2>(out T2 arg2);
    public delegate TResult OutFunc<out TResult, in T2, T3>(T2 arg2, out T3 arg3);
    public delegate TResult OutFunc<out TResult, in T2, in T3, T4>(T2 arg2, T3 arg3, out T4 arg4);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, T5>(T2 arg2, T3 arg3, T4 arg4, out T5 arg5);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, in T5, T6>(T2 arg2, T3 arg3, T4 arg4, T5 arg5, out T6 arg6);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, in T5, in T6, T7>(T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, out T7 arg7);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, in T5, in T6, in T7, T8>(T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, out T8 arg8);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, in T5, in T6, in T7, in T8, T9>(T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, out T9 arg9);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, T10>(T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, out T10 arg10);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, T11>(T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, out T11 arg11);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, T12>(T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, out T12 arg12);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, T13>(T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, out T13 arg13);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, T14>(T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, out T14 arg14);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, T15>(T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, out T15 arg15);
    public delegate TResult OutFunc<out TResult, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, T16>(T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, out T16 arg16);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
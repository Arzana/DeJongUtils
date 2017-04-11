namespace DeJong.Utilities.Core
{
    /// <summary>
    /// Encapsulates a method that has a single out parameter and does not return a value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the parameter of the method that this delegate encapsulates.
    /// This type parameter is contravariant.
    /// That is, you can use either the type you specified or any type that is less derived.
    /// </typeparam>
    /// <param name="arg"> The out parameter of the method that this delegate encapsulates. </param>
    public delegate void OutAction<T>(out T arg);
    /// <summary>
    /// Encapsulates a method that has one normal parameter, one out parameter and does not return a value.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the first parameter of the method that this delegate encapsulates.
    /// This type parameter is contravariant.
    /// That is, you can use either the type you specified or any type that is less derived.
    /// </typeparam>
    /// <typeparam name="T2"> The type of the out parameter of the method that this delegate encapsulates. </typeparam>
    /// <param name="arg1"> The first parameter of the method that this delegate encapsulates. </param>
    /// <param name="arg2"> The out parameter of the method that this delegate encapsulates. </param>
    public delegate void OutAction<in T1, T2>(T1 arg1, out T2 arg2);
    /// <summary>
    /// Encapsulates a method that has two normal parameter, one out parameter and does not return a value.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the first parameter of the method that this delegate encapsulates.
    /// This type parameter is contravariant.
    /// That is, you can use either the type you specified or any type that is less derived.
    /// </typeparam>
    /// <typeparam name="T2"> The second parameter of the method that this delegate encapsulates. </typeparam>
    /// <typeparam name="T3"> The type of the out parameter of the method that this delegate encapsulates. </typeparam>
    /// <param name="arg1"> The first parameter of the method that this delegate encapsulates. </param>
    /// <param name="arg2"> The second parameter of the method that this delegate encapsulates. </param>
    /// <param name="arg3"> The out parameter of the method that this delegate encapsulates. </param>
    public delegate void OutAction<in T1, in T2, T3>(T1 arg1, T2 arg2, out T3 arg3);
    public delegate void OutAction<in T1, in T2, in T3, T4>(T1 arg1, T2 arg2, T3 arg3, out T4 arg4);
    public delegate void OutAction<in T1, in T2, in T3, in T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, out T5 arg5);
    public delegate void OutAction<in T1, in T2, in T3, in T4, in T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, out T6 arg6);
    public delegate void OutAction<in T1, in T2, in T3, in T4, in T5, in T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, out T7 arg7);
    public delegate void OutAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, out T8 arg8);
    public delegate void OutAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, out T9 arg9);
    public delegate void OutAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, T10>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, out T10 arg10);
    public delegate void OutAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, T11>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, out T11 arg11);
    public delegate void OutAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, T12>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, out T12 arg12);
    public delegate void OutAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, T13>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, out T13 arg13);
    public delegate void OutAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, T14>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, out T14 arg14);
    public delegate void OutAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, T15>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, out T15 arg15);
    public delegate void OutAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, T16>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, out T16 arg16);
}
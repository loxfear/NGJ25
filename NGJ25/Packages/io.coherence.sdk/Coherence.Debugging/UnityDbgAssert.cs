﻿// Copyright (c) coherence ApS.
// See the license file in the package root for more information.

#if UNITY_5_3_OR_NEWER

namespace Coherence.Debugging
{
    using Debug = UnityEngine.Debug;

    static class UnityDbgAssert
    {
        public static void That(bool condition, string message)
        {
            if (condition)
            {
                return;
            }

            Debug.Assert(false, $"[coherence] {message}");
        }

        public static void ThatFmt<T1>(bool condition, string messageToFormat, in T1 arg1)
        {
            if (condition)
            {
                return;
            }

            Debug.Assert(false, $"[coherence] {string.Format(messageToFormat, arg1)}");
        }

        public static void ThatFmt<T1, T2>(bool condition, string messageToFormat, in T1 arg1, in T2 arg2)
        {
            if (condition)
            {
                return;
            }

            Debug.Assert(false, $"[coherence] {string.Format(messageToFormat, arg1, arg2)}");
        }

        public static void ThatFmt<T1, T2, T3>(bool condition, string messageToFormat, in T1 arg1, in T2 arg2, in T3 arg3)
        {
            if (condition)
            {
                return;
            }

            Debug.Assert(false, $"[coherence] {string.Format(messageToFormat, arg1, arg2, arg3)}");
        }

        public static void ThatFmt<T1, T2, T3, T4>(bool condition, string messageToFormat, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
        {
            if (condition)
            {
                return;
            }

            Debug.Assert(false, $"[coherence] {string.Format(messageToFormat, arg1, arg2, arg3, arg4)}");
        }
    }
}

#endif

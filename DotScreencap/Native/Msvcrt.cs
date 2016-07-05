using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace DotScreencap.Native
{
    /// <summary>
    ///     Static class containing pinvoke methods for the msvcrt.dll api. Review msdn and other existing msvcrt.dll
    ///     documentation if needed.
    ///     for usage.
    /// </summary>
    public static class Msvcrt
    {
        // Warnings disabled due to the nature of native structs.
        // Review msdn and other existing documentation for more information on contents contained here.
        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl,
            SetLastError = false)]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);
    }
}
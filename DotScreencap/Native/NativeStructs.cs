using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace DotScreencap.Native
{
    // Warnings disabled due to the nature of native structs.
    // Review msdn and other existing documentation for more information on contents contained here.

    // Define the coordinates of the upper-left and
    // lower-right corners of a rectangle
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Rect
    {
        [MarshalAs(UnmanagedType.I4)] public int left;
        [MarshalAs(UnmanagedType.I4)] public int top;
        [MarshalAs(UnmanagedType.I4)] public int right;
        [MarshalAs(UnmanagedType.I4)] public int bottom;
    }

    // Contains information for a single stream
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct Avistreaminfo
    {
        [MarshalAs(UnmanagedType.I4)] public int fccType;
        [MarshalAs(UnmanagedType.I4)] public int fccHandler;
        [MarshalAs(UnmanagedType.I4)] public int dwFlags;
        [MarshalAs(UnmanagedType.I4)] public int dwCaps;
        [MarshalAs(UnmanagedType.I2)] public short wPriority;
        [MarshalAs(UnmanagedType.I2)] public short wLanguage;
        [MarshalAs(UnmanagedType.I4)] public int dwScale;
        [MarshalAs(UnmanagedType.I4)] public int dwRate; // dwRate / dwScale == samples/second
        [MarshalAs(UnmanagedType.I4)] public int dwStart;
        [MarshalAs(UnmanagedType.I4)] public int dwLength;
        [MarshalAs(UnmanagedType.I4)] public int dwInitialFrames;
        [MarshalAs(UnmanagedType.I4)] public int dwSuggestedBufferSize;
        [MarshalAs(UnmanagedType.I4)] public int dwQuality;
        [MarshalAs(UnmanagedType.I4)] public int dwSampleSize;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 16)] public Rect rcFrame;
        [MarshalAs(UnmanagedType.I4)] public int dwEditCount;
        [MarshalAs(UnmanagedType.I4)] public int dwFormatChangeCount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)] public string szName;
    }

    // Contains information about the dimensions and color format of a DIB
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Bitmapinfoheader
    {
        [MarshalAs(UnmanagedType.I4)] public int biSize;
        [MarshalAs(UnmanagedType.I4)] public int biWidth;
        [MarshalAs(UnmanagedType.I4)] public int biHeight;
        [MarshalAs(UnmanagedType.I2)] public short biPlanes;
        [MarshalAs(UnmanagedType.I2)] public short biBitCount;
        [MarshalAs(UnmanagedType.I4)] public int biCompression;
        [MarshalAs(UnmanagedType.I4)] public int biSizeImage;
        [MarshalAs(UnmanagedType.I4)] public int biXPelsPerMeter;
        [MarshalAs(UnmanagedType.I4)] public int biYPelsPerMeter;
        [MarshalAs(UnmanagedType.I4)] public int biClrUsed;
        [MarshalAs(UnmanagedType.I4)] public int biClrImportant;
    }

    // Contains information about a stream and how it is compressed and saved
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Avicompressoptions
    {
        [MarshalAs(UnmanagedType.I4)] public int fccType;
        [MarshalAs(UnmanagedType.I4)] public int fccHandler;
        [MarshalAs(UnmanagedType.I4)] public int dwKeyFrameEvery;
        [MarshalAs(UnmanagedType.I4)] public int dwQuality;
        [MarshalAs(UnmanagedType.I4)] public int dwBytesPerSecond;
        [MarshalAs(UnmanagedType.I4)] public int dwFlags;
        [MarshalAs(UnmanagedType.I4)] public int lpFormat;
        [MarshalAs(UnmanagedType.I4)] public int cbFormat;
        [MarshalAs(UnmanagedType.I4)] public int lpParms;
        [MarshalAs(UnmanagedType.I4)] public int cbParms;
        [MarshalAs(UnmanagedType.I4)] public int dwInterleaveEvery;
    }

    // --- enumerations
    // ---
}
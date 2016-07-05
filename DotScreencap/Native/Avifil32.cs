using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace DotScreencap.Native
{
    /// <summary>
    ///     Static class containing pinvoke methods for the avifil32.dll api. Review msdn and other avifil32.dll documentation
    ///     for usage.
    /// </summary>
    public static class Avifil32
    {
        // Warnings disabled due to the nature of pinvokes.
        // Review msdn and other existing documentation for more information on contents contained here.
        // AVI Functions.

        // Initialize the AVIFile library
        [DllImport("avifil32.dll")]
        public static extern void AVIFileInit();

        // Exit the AVIFile library 
        [DllImport("avifil32.dll")]
        public static extern void AVIFileExit();

        // Open an AVI file
        [DllImport("avifil32.dll", CharSet = CharSet.Unicode)]
        public static extern int AVIFileOpen(
            out IntPtr ppfile,
            string szFile,
            OpenFileMode mode,
            IntPtr pclsidHandler);

        // Release an open AVI stream
        [DllImport("avifil32.dll")]
        public static extern int AVIFileRelease(
            IntPtr pfile);

        // Get address of a stream interface that is associated
        // with a specified AVI file
        [DllImport("avifil32.dll")]
        public static extern int AVIFileGetStream(
            IntPtr pfile,
            out IntPtr ppavi,
            int fccType,
            int lParam);

        // Create a new stream in an existing file and creates an interface to the new stream
        [DllImport("avifil32.dll")]
        public static extern int AVIFileCreateStream(
            IntPtr pfile,
            out IntPtr ppavi,
            ref Avistreaminfo psi);

        // Release an open AVI stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamRelease(
            IntPtr pavi);

        // Set the format of a stream at the specified position
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamSetFormat(
            IntPtr pavi,
            int lPos,
            ref Bitmapinfoheader lpFormat,
            int cbFormat);

        // Get the starting sample number for the stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamStart(
            IntPtr pavi);

        // Get the length of the stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamLength(
            IntPtr pavi);

        // Obtain stream header information
        [DllImport("avifil32.dll", CharSet = CharSet.Unicode)]
        public static extern int AVIStreamInfo(
            IntPtr pavi,
            ref Avistreaminfo psi,
            int lSize);

        // Prepare to decompress video frames from the specified video stream
        [DllImport("avifil32.dll")]
        public static extern IntPtr AVIStreamGetFrameOpen(
            IntPtr pavi,
            ref Bitmapinfoheader lpbiWanted);

        [DllImport("avifil32.dll")]
        public static extern IntPtr AVIStreamGetFrameOpen(
            IntPtr pavi,
            int lpbiWanted);

        // Releases resources used to decompress video frames
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamGetFrameClose(
            IntPtr pget);

        // Return the address of a decompressed video frame
        [DllImport("avifil32.dll")]
        public static extern IntPtr AVIStreamGetFrame(
            IntPtr pget,
            int lPos);

        // Write data to a stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamWrite(
            IntPtr pavi,
            int lStart,
            int lSamples,
            IntPtr lpBuffer,
            int cbBuffer,
            int dwFlags,
            IntPtr plSampWritten,
            IntPtr plBytesWritten);

        // Retrieve the save options for a file and returns them in a buffer
        [DllImport("avifil32.dll")]
        public static extern int AVISaveOptions(
            IntPtr hwnd,
            int flags,
            int streams,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IntPtr[] ppavi,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IntPtr[] plpOptions);

        // Free the resources allocated by the AVISaveOptions function
        [DllImport("avifil32.dll")]
        public static extern int AVISaveOptionsFree(
            int streams,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IntPtr[] plpOptions);

        // Create a compressed stream from an uncompressed stream and a
        // compression filter, and returns the address of a pointer to
        // the compressed stream
        [DllImport("avifil32.dll")]
        public static extern int AVIMakeCompressedStream(
            out IntPtr ppsCompressed,
            IntPtr psSource,
            ref Avicompressoptions lpOptions,
            IntPtr pclsidHandler);

        // Replacement of mmioFOURCC macros
        public static int MmioFourcc(string str)
        {
            return (byte) str[0] |
                   ((byte) str[1] << 8) |
                   ((byte) str[2] << 16) |
                   ((byte) str[3] << 24);
        }

        // Inverse of mmioFOURCC
        public static string decode_mmioFOURCC(int code)
        {
            var chs = new char[4];

            for (var i = 0; i < 4; i++)
            {
                chs[i] = (char) (byte) ((code >> (i << 3)) & 0xFF);
                if (!char.IsLetterOrDigit(chs[i]))
                {
                    chs[i] = ' ';
                }
            }
            return new string(chs);
        }

        // --- public methods

        // Version of AVISaveOptions for one stream only
        //
        // I don't find a way to interop AVISaveOptions more likely, so the
        // usage of original version is not easy. The version makes it more
        // usefull.
        //
        public static int AviSaveOptions(IntPtr stream, ref Avicompressoptions opts, IntPtr owner)
        {
            var streams = new IntPtr[1];
            var infPtrs = new IntPtr[1];

            // alloc unmanaged memory
            var mem = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Avicompressoptions)));

            // copy from managed structure to unmanaged memory
            Marshal.StructureToPtr(opts, mem, false);

            streams[0] = stream;
            infPtrs[0] = mem;

            // show dialog with a list of available compresors and configuration
            var ret = AVISaveOptions(IntPtr.Zero, 0, 1, streams, infPtrs);

            // copy from unmanaged memory to managed structure
            opts = (Avicompressoptions) Marshal.PtrToStructure(mem, typeof(Avicompressoptions));

            // free AVI compression options
            AVISaveOptionsFree(1, infPtrs);

            // clear it, because the information already freed by AVISaveOptionsFree
            opts.cbFormat = 0;
            opts.cbParms = 0;
            opts.lpFormat = 0;
            opts.lpParms = 0;

            // free unmanaged memory
            Marshal.FreeHGlobal(mem);

            return ret;
        }
    }
}
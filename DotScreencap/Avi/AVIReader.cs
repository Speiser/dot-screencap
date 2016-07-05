using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using DotScreencap.Native;

#pragma warning disable 1591

namespace DotScreencap.Avi
{ 
    /// <summary>
    ///     Reading AVI files using Video for Windows
    /// </summary>
    public class AviReader : IDisposable
    {
        private IntPtr _file;
        private IntPtr _getFrame;

        private int _position;
        private int _start;
        private IntPtr _stream;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AviReader" /> class.
        /// </summary>
        public AviReader()
        {
            Avifil32.AVIFileInit();
        }

        /// <summary>
        ///     Gets the width.
        /// </summary>
        /// <value>
        ///     The width.
        /// </value>
        public int Width { get; private set; }

        /// <summary>
        ///     Gets the height.
        /// </summary>
        /// <value>
        ///     The height.
        /// </value>
        public int Height { get; private set; }

        /// <summary>
        ///     Gets the frame rate.
        /// </summary>
        /// <value>
        ///     The frame rate.
        /// </value>
        public float FrameRate { get; private set; }

        /// <summary>
        ///     Gets or sets the current position.
        /// </summary>
        /// <value>
        ///     The current position.
        /// </value>
        public int CurrentPosition
        {
            get { return _position; }
            set
            {
                if ((value < _start) || (value >= _start + Length))
                {
                    _position = _start;
                }
                else
                {
                    _position = value;
                }
            }
        }

        /// <summary>
        ///     Gets the length.
        /// </summary>
        /// <value>
        ///     The length.
        /// </value>
        public int Length { get; private set; }

        /// <summary>
        ///     Gets the codec.
        /// </summary>
        /// <value>
        ///     The codec.
        /// </value>
        public string Codec { get; private set; }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // Remove me from the Finalization queue 
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="AviReader" /> class.
        /// </summary>
        ~AviReader()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources
                // there is nothing managed yet
            }
            Close();
            Avifil32.AVIFileExit();
        }

        /// <summary>
        ///     Opens the specified avi file.
        /// </summary>
        /// <param name="fname">The fname.</param>
        /// <exception cref="ApplicationException">
        ///     Failed opening file
        ///     or
        ///     Failed getting video stream
        ///     or
        ///     Failed initializing decompressor
        /// </exception>
        public void Open(string fname)
        {
            // close previous file
            Close();

            // open file
            if (Avifil32.AVIFileOpen(out _file, fname, OpenFileMode.ShareDenyWrite, IntPtr.Zero) != 0)
            {
                throw new ApplicationException("Failed opening file");
            }

            // get first video stream
            if (Avifil32.AVIFileGetStream(_file, out _stream, Avifil32.MmioFourcc("vids"), 0) != 0)
            {
                throw new ApplicationException("Failed getting video stream");
            }

            // get stream info
            var info = new Avistreaminfo();
            Avifil32.AVIStreamInfo(_stream, ref info, Marshal.SizeOf(info));

            Width = info.rcFrame.right;
            Height = info.rcFrame.bottom;
            _position = info.dwStart;
            _start = info.dwStart;
            Length = info.dwLength;
            FrameRate = info.dwRate/(float) info.dwScale;
            Codec = Avifil32.decode_mmioFOURCC(info.fccHandler);

            // prepare decompressor
            var bih = new Bitmapinfoheader();

            bih.biSize = Marshal.SizeOf(bih.GetType());
            bih.biWidth = Width;
            bih.biHeight = Height;
            bih.biPlanes = 1;
            bih.biBitCount = 24;
            bih.biCompression = 0; // BI_RGB

            // get frame open object
            if ((_getFrame = Avifil32.AVIStreamGetFrameOpen(_stream, ref bih)) == IntPtr.Zero)
            {
                bih.biHeight = -Height;

                if ((_getFrame = Avifil32.AVIStreamGetFrameOpen(_stream, ref bih)) == IntPtr.Zero)
                {
                    throw new ApplicationException("Failed initializing decompressor");
                }
            }
        }

        /// <summary>
        ///     Closes the avi file.
        /// </summary>
        public void Close()
        {
            // release frame open object
            if (_getFrame != IntPtr.Zero)
            {
                Avifil32.AVIStreamGetFrameClose(_getFrame);
                _getFrame = IntPtr.Zero;
            }
            // release stream
            if (_stream != IntPtr.Zero)
            {
                Avifil32.AVIStreamRelease(_stream);
                _stream = IntPtr.Zero;
            }
            // release file
            if (_file != IntPtr.Zero)
            {
                Avifil32.AVIFileRelease(_file);
                _file = IntPtr.Zero;
            }
        }

        /// <summary>
        ///     Gets the next frame.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Failed getting frame</exception>
        public Bitmap GetNextFrame()
        {
            // get frame at specified position
            var pdib = Avifil32.AVIStreamGetFrame(_getFrame, _position);
            if (pdib == IntPtr.Zero)
            {
                throw new ApplicationException("Failed getting frame");
            }

            // copy BITMAPINFOHEADER from unmanaged memory
            var bih = (Bitmapinfoheader) Marshal.PtrToStructure(pdib, typeof(Bitmapinfoheader));

            // create new bitmap
            var bmp = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

            // lock bitmap data
            var bmData = bmp.LockBits(
                new Rectangle(0, 0, Width, Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            // copy image data
            var srcStride = bmData.Stride; // width * 3;
            var dstStride = bmData.Stride;

            // check image direction
            if (bih.biHeight > 0)
            {
                // it`s a bottom-top image
                var dst = bmData.Scan0 + dstStride*(Height - 1);
                var src = pdib + Marshal.SizeOf(typeof(Bitmapinfoheader));

                for (var y = 0; y < Height; y++)
                {
                    Msvcrt.memcpy(dst, src, srcStride);
                    dst -= dstStride;
                    src += srcStride;
                }
            }
            else
            {
                // it`s a top bootom image
                var dst = bmData.Scan0;
                var src = pdib + Marshal.SizeOf(typeof(Bitmapinfoheader));

                if (srcStride != dstStride)
                {
                    // copy line by line
                    for (var y = 0; y < Height; y++)
                    {
                        Msvcrt.memcpy(dst, src, srcStride);
                        dst += dstStride;
                        src += srcStride;
                    }
                }
                else
                {
                    // copy the whole image
                    Msvcrt.memcpy(dst, src, srcStride*Height);
                }
            }

            // unlock bitmap data
            bmp.UnlockBits(bmData);

            _position++;

            return bmp;
        }
    }
}
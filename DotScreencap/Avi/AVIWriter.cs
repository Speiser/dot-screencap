using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using DotScreencap.Native;

#pragma warning disable 1591

namespace DotScreencap.Avi
{
    /// <summary>
    ///     Writing AVI files using Video for Windows
    ///     Note: I am stucked with non even frame width. AVIs with non even
    ///     width are playing well in WMP, but not in BSPlayer (it's for "DIB " codec).
    ///     Some other codecs does not work with non even width or height at all.
    /// </summary>
    public class AviWriter : IDisposable
    {
        private IntPtr _buf = IntPtr.Zero;
        private IntPtr _file;
        private int _height;
        private IntPtr _stream;
        private IntPtr _streamCompressed;
        private int _stride;

        private int _width;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AviWriter" /> class.
        /// </summary>
        public AviWriter()
        {
            Avifil32.AVIFileInit();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AviWriter" /> class.
        /// </summary>
        /// <param name="codec">The codec.</param>
        public AviWriter(string codec) : this()
        {
            Codec = codec;
        }

        /// <summary>
        ///     Gets the current position.
        /// </summary>
        /// <value>
        ///     The current position.
        /// </value>
        public int CurrentPosition { get; private set; }

        /// <summary>
        ///     Gets the width.
        /// </summary>
        /// <value>
        ///     The width.
        /// </value>
        public int Width => _buf != IntPtr.Zero ? _width : 0;

        /// <summary>
        ///     Gets the height.
        /// </summary>
        /// <value>
        ///     The height.
        /// </value>
        public int Height => _buf != IntPtr.Zero ? _height : 0;

        /// <summary>
        ///     Gets or sets the codec.
        /// </summary>
        /// <value>
        ///     The codec.
        /// </value>
        public string Codec { get; set; } = "DIB ";

        /// <summary>
        ///     Gets or sets the quality.
        /// </summary>
        /// <value>
        ///     The quality.
        /// </value>
        public int Quality { get; set; } = -1;

        /// <summary>
        ///     Gets or sets the frame rate.
        /// </summary>
        /// <value>
        ///     The frame rate.
        /// </value>
        public int FrameRate { get; set; } = 25;

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
        ///     Finalizes an instance of the <see cref="AviWriter" /> class.
        /// </summary>
        ~AviWriter()
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
        ///     Creates and Opens a new avi file the specified fname.
        /// </summary>
        /// <param name="fname">The fname.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <exception cref="ApplicationException">
        ///     Failed opening file
        ///     Failed creating compressed stream
        /// </exception>
        public void Open(string fname, int width, int height)
        {
            // close previous file
            Close();

            // calculate stride
            _stride = width*3;
            var r = _stride%4;
            if (r != 0)
            {
                _stride += 4 - r;
            }

            // create new file
            if (Avifil32.AVIFileOpen(out _file, fname, OpenFileMode.Create | OpenFileMode.Write, IntPtr.Zero) != 0)
            {
                throw new ApplicationException("Failed opening file");
            }

            _width = width;
            _height = height;

            // describe new stream
            var info = new Avistreaminfo
            {
                fccType = Avifil32.MmioFourcc("vids"),
                fccHandler = Avifil32.MmioFourcc(Codec),
                dwScale = 1,
                dwRate = FrameRate,
                dwSuggestedBufferSize = _stride*height
            };

            // create stream
            if (Avifil32.AVIFileCreateStream(_file, out _stream, ref info) != 0)
            {
                var error = Marshal.GetLastWin32Error();
                throw new ApplicationException($"Failed creating compressed stream - {error}");
            }

            // describe compression options
            var opts = new Avicompressoptions
            {
                fccHandler = Avifil32.MmioFourcc(Codec),
                dwQuality = Quality
            };

            //
            // Avifil32.AVISaveOptions(stream, ref opts, IntPtr.Zero);

            // create compressed stream
            if (Avifil32.AVIMakeCompressedStream(out _streamCompressed, _stream, ref opts, IntPtr.Zero) != 0)
            {
                var error = Marshal.GetLastWin32Error();
                throw new ApplicationException($"Failed creating compressed stream - {error}");
            }

            // describe frame format
            var bih = new Bitmapinfoheader
            {
                biSize = Marshal.SizeOf(typeof(Bitmapinfoheader)),
                biWidth = width,
                biHeight = height,
                biPlanes = 1,
                biBitCount = 24,
                biSizeImage = 0,
                biCompression = 0
            };

            // BI_RGB

            // set frame format
            if (Avifil32.AVIStreamSetFormat(_streamCompressed, 0, ref bih, Marshal.SizeOf(typeof(Bitmapinfoheader))) !=
                0)
            {
                throw new ApplicationException("Failed creating compressed stream");
            }

            // alloc unmanaged memory for frame
            _buf = Marshal.AllocHGlobal(_stride*height);

            CurrentPosition = 0;
        }

        /// <summary>
        ///     Closes the file.
        /// </summary>
        public void Close()
        {
            // free unmanaged memory
            if (_buf != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_buf);
                _buf = IntPtr.Zero;
            }
            // release compressed stream
            if (_streamCompressed != IntPtr.Zero)
            {
                Avifil32.AVIStreamRelease(_streamCompressed);
                _streamCompressed = IntPtr.Zero;
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
        ///     Add new frame to the AVI file.
        /// </summary>
        /// <param name="bmp">The BMP.</param>
        /// <exception cref="ApplicationException">
        ///     Invalid image dimension
        ///     or
        ///     Failed adding frame
        /// </exception>
        public void AddFrame(Bitmap bmp)
        {
            // check image dimension
            if ((bmp.Width != _width) || (bmp.Height != _height))
            {
                Console.WriteLine(bmp.Width + " - " + _width);
                Console.WriteLine(bmp.Height + " - " + _height);
                throw new ApplicationException("Invalid image dimension");
            }

            // lock bitmap data
            var bmData = bmp.LockBits(
                new Rectangle(0, 0, _width, _height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            // copy image data
            var srcStride = bmData.Stride;
            var dstStride = _stride;

            var src = bmData.Scan0 + srcStride*(_height - 1);
            var dst = _buf;

            for (var y = 0; y < _height; y++)
            {
                Msvcrt.memcpy(dst, src, dstStride);
                dst += dstStride;
                src -= srcStride;
            }

            // unlock bitmap data
            bmp.UnlockBits(bmData);

            // write to stream
            if (Avifil32.AVIStreamWrite(_streamCompressed, CurrentPosition, 1, _buf,
                _stride*_height, 0, IntPtr.Zero, IntPtr.Zero) != 0)
            {
                throw new ApplicationException("Failed adding frame");
            }
            CurrentPosition++;
        }
    }
}
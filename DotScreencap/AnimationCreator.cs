using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using DotScreencap.EventArgs;

namespace DotScreencap
{
    /// <summary>
    ///     Represents the animate class.
    ///     Is used for the creation of animated gifs.
    /// </summary>
    public sealed class AnimationCreator
    {
        private readonly ScreenCapture _screencap;
        private string _filename;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AnimationCreator" /> class.
        /// </summary>
        public AnimationCreator(ScreenCapture screencap) : this(screencap, "animation")
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AnimationCreator" /> class.
        /// </summary>
        public AnimationCreator(ScreenCapture screencap, string filename)
        {
            _screencap = screencap;
            Filename = filename + ".gif";
        }

        /// <summary>
        ///     Gets or sets the file name of the animation.
        /// </summary>
        public string Filename
        {
            get { return _filename; }

            set
            {
                if (value.Length < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _filename = value;
            }
        }

        /// <summary>
        ///     Is fired after recording was finished, before file is saved.
        /// </summary>
        public event EventHandler<AnimationCreatorOnAnimationRecordedEventArgs> OnAnimationRecorded;

        /// <summary>
        ///     Is fired after an OutOfMemoryException was thrown.
        /// </summary>
        public event EventHandler<AnimationCreatorOnOutOfMemoryExceptionThrownEventArgs> OnOutOfMemoryExceptionThrown;

        /// <summary>
        ///     Creates a *.gif from a list of bitmaps.
        /// </summary>
        public void SaveAnimationAsGif(int frames, int wait)
        {
            var encoder = new GifBitmapEncoder();

            for (var i = 0; i < frames; i++)
            {
                try
                {
                    _screencap.GetBitmapOfScreen();

                    var source = Imaging.CreateBitmapSourceFromHBitmap(
                        _screencap.ScreenBitmap.Bitmap.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());

                    encoder.Frames.Add(BitmapFrame.Create(source));
                    Thread.Sleep(wait);
                }

                catch (OutOfMemoryException e)
                {
                    RaiseOnOutOfMemoryExceptionThrown(e, i);
                    break;
                }

                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
            }

            RaiseOnAnimationRecorded();

            Thread.Sleep(1000);

            encoder.Save(new FileStream(_filename, FileMode.Create));
        }

        private void RaiseOnAnimationRecorded()
        {
            OnAnimationRecorded?.Invoke(this, new AnimationCreatorOnAnimationRecordedEventArgs(this));
        }

        private void RaiseOnOutOfMemoryExceptionThrown(OutOfMemoryException e, int thrownAfterXFrames)
        {
            OnOutOfMemoryExceptionThrown?.Invoke(this,
                new AnimationCreatorOnOutOfMemoryExceptionThrownEventArgs(e, thrownAfterXFrames));
        }
    }
}
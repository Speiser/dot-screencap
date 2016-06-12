namespace DotScreencap
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Represents the animate class.
    /// Is used for the creation of animated gifs.
    /// </summary>
    public sealed class AnimationCreator
    {
        private ScreenCapture screencap;
        private string filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationCreator"/> class.
        /// </summary>
        public AnimationCreator(ScreenCapture screencap) : this(screencap, "animation") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationCreator"/> class.
        /// </summary>
        public AnimationCreator(ScreenCapture screencap, string filename)
        {
            this.screencap = screencap;
            this.Filename = filename + ".gif";
        }

        /// <summary>
        /// Is fired after an OutOfMemoryException was thrown.
        /// </summary>
        public event EventHandler<AnimationCreatorOnOutOfMemoryExceptionThrownEventArgs> OnOutOfMemoryExceptionThrown;

        /// <summary>
        /// File name of the animation.
        /// </summary>
        public string Filename
        {
            get
            {
                return this.filename;
            }

            set
            {
                if (value.Length < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                this.filename = value;
            }
        }
        /// <summary>
        /// Creates a *.gif from a list of bitmaps.
        /// </summary>
        public void SaveAnimationAsGif(int frames, int wait)
        {
            var encoder = new GifBitmapEncoder();

            for (int i = 0; i < frames; i++)
            {
                try
                {
                    screencap.GetBitmapOfScreen();

                    var source = Imaging.CreateBitmapSourceFromHBitmap(
                                 screencap.ScreenBitmap.GetHbitmap(),
                                 IntPtr.Zero,
                                 Int32Rect.Empty,
                                 BitmapSizeOptions.FromEmptyOptions());

                    encoder.Frames.Add(BitmapFrame.Create(source));
                    Thread.Sleep(wait);
                }
                catch (OutOfMemoryException e)
                {
                    FireOnOutOfMemoryExceptionThrown(e, i);
                    break;
                }
                catch (Exception) { }
            }

            Thread.Sleep(1000);
            encoder.Save(new FileStream(this.filename, FileMode.Create));
        }

        private void FireOnOutOfMemoryExceptionThrown(OutOfMemoryException e, int thrownAfterXFrames)
        {
            this.OnOutOfMemoryExceptionThrown(this, new AnimationCreatorOnOutOfMemoryExceptionThrownEventArgs(e, thrownAfterXFrames));
        }
    }
}

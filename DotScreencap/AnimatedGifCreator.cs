namespace DotScreencap
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Represents the animate class.
    /// Is used for the creation of animated gifs.
    /// </summary>
    public sealed class AnimatedGifCreator
    {
        private int fps;

        /// <summary>
        /// Duration of the gif in seconds.
        /// </summary>
        private int duration;
        private List<Bitmap> images;
        private string filename;
        private ScreenCapture screencap;
        private Thread workerThread;


        /// <summary>
        /// Initializes a new instance of the <see cref="AnimatedGifCreator"/> class.
        /// </summary>
        public AnimatedGifCreator(List<Bitmap> images)
        {
            this.images = images;
            this.filename = "animation" + ".gif";
        }

        /// <summary>
        /// Creates a *.gif from a list of bitmaps.
        /// </summary>
        public void SaveAnimationAsGif()
        {
            var encoder = new GifBitmapEncoder();

            while (0 != this.images.Count)
            {
                var source = Imaging.CreateBitmapSourceFromHBitmap(
                             this.images[0].GetHbitmap(),
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());

                for (int i = 0; i < 2; i++)
                {
                    encoder.Frames.Add(BitmapFrame.Create(source));
                }

                this.images.RemoveAt(0);
            }

            encoder.Save(new FileStream(this.filename, FileMode.Create));
        }

        private void Start()
        {
            // Start Thread.
        }
    }
}

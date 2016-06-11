namespace DotScreencap
{
    // Rename to AnimationCreator
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
    public sealed class AnimationCreator
    {
        // private int fps;
        // private int duration;
        private ScreenCapture screencap;
        // private Thread workerThread;

        /// <summary>
        /// Duration of the gif in seconds.
        /// </summary>
        private List<Bitmap> images;

        private string filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationCreator"/> class.
        /// </summary>
        public AnimationCreator(ScreenCapture screencap, List<Bitmap> images)
        {
            this.screencap = screencap;
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

        /// <summary>
        /// Will replace this.SaveAnimationAsGif()().
        /// </summary>
        public void PERFORMANCETEST_SaveAnimationAsGif()
        {
            var encoder = new GifBitmapEncoder();

            // Create calculation for fps and duration.
            for (int i = 0; i < 50; i++)
            {
                screencap.GetBitmapOfScreen();

                var source = Imaging.CreateBitmapSourceFromHBitmap(
                             screencap.ScreenBitmap.GetHbitmap(),
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());

                encoder.Frames.Add(BitmapFrame.Create(source));
                Console.WriteLine(i);
                Thread.Sleep(200);
            }

            Thread.Sleep(1000);
            encoder.Save(new FileStream(this.filename, FileMode.Create));
        }

        private void Start()
        {
            // Start Thread.
        }
    }
}

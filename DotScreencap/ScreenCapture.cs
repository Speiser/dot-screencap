namespace DotScreencap
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Represents the ScreenCapture class.
    /// </summary>
    public sealed class ScreenCapture
    {
        private Bitmap screenBitmap;
        private BitmapImage screenBitmapImage;
        private JpgCreator jpgCreator;
        private Rectangle screenSize;
        private Thread gifWorker;               // Move to AnimatedGifCreator
        private List<Bitmap> imagesForGif;      // Remove

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenCapture"/> class.
        /// </summary>
        public ScreenCapture()
        {
            this.ScreenSize = Screen.PrimaryScreen.Bounds;
        }

        /// <summary>
        /// Will be fired after a screenshot is taken.
        /// </summary>
        public event EventHandler<ScreenCaptureOnScreenshotTakenEventArgs> OnScreenshotTaken;

        /// <summary>
        /// Will be fired after a screenshot is taken.
        /// </summary>
        public event EventHandler<ScreenCaptureOnGifCreatedEventArgs> OnGifCreated;

        /// <summary>
        /// Gets the ScreenBitmapImage.
        /// Can be used for WPF ImageBox.
        /// </summary>
        public BitmapImage ScreenBitmapImage
        {
            get
            {
                return this.screenBitmapImage;
            }

            private set
            {
                this.screenBitmapImage = value;
            }
        }

        /// <summary>
        /// Gets the ScreenSize.
        /// </summary>
        public Rectangle ScreenSize
        {
            get
            {
                return this.screenSize;
            }

            private set
            {
                this.screenSize = value;
            }
        }

        /// <summary>
        /// Saves a *.jpg to the execution folder.
        /// </summary>
        /// <param name="filename">Possibly specified filename.</param>
        public void TakeScreenshot(params string[] filename)
        {
            this.GetBitmapOfScreen();
            this.ConvertBitmapToBitmapImage();

            if (this.ScreenBitmapImage == null)
            {
                throw new NullReferenceException();
            }

            if (filename.Length < 1)
            {
                this.jpgCreator = new JpgCreator(this.ScreenBitmapImage);
            }
            else
            {
                this.jpgCreator = new JpgCreator(this.ScreenBitmapImage, filename[0]);
            }

            this.jpgCreator.SaveScreenshotAsJPG();
            this.FireOnScreenshotTaken();
        }

        /// <summary>
        /// Saves a animated *.gif to the execution folder.
        /// </summary>
        /// <param name="length">Length of recording time in seconds.</param>
        public void CreateGIF(int length)
        {
            this.imagesForGif = new List<Bitmap>();
            this.gifWorker = new Thread(new ThreadStart(this.GifWork));
            this.gifWorker.Start();
            Thread.Sleep(length * 1000);
            this.gifWorker.Abort();
            Animate.SaveAnimationAsGif(this.imagesForGif);
        }

        /// <summary>
        /// Work method to get bitmaps for the gif.
        /// </summary>
        private void GifWork()
        {
            while (true)
            {
                this.GetBitmapOfScreen();
                this.imagesForGif.Add(this.screenBitmap);
                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Creates a Bitmap of the users screen.
        /// </summary>
        private void GetBitmapOfScreen()
        {
            if (this.ScreenSize == null)
            {
                throw new NullReferenceException();
            }

            this.screenBitmap = new Bitmap(this.screenSize.Width, this.screenSize.Height);
            Graphics screen = Graphics.FromImage(this.screenBitmap);
            screen.CopyFromScreen(0, 0, 0, 0, new Size(this.screenSize.Width, this.screenSize.Height));
        }

        /// <summary>
        /// Converts a Bitmap to a BitmapImage using a memory stream.
        /// </summary>
        private void ConvertBitmapToBitmapImage()
        {
            if (this.screenBitmap == null)
            {
                throw new NullReferenceException();
            }

            MemoryStream ms = new MemoryStream();
            this.screenBitmap.Save(ms, ImageFormat.Bmp);
            this.ScreenBitmapImage = new BitmapImage();
            this.ScreenBitmapImage.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            this.ScreenBitmapImage.StreamSource = ms;
            this.ScreenBitmapImage.EndInit();
        }

        /// <summary>
        /// Is fired when screenshot was taken.
        /// </summary>
        private void FireOnScreenshotTaken()
        {
            this.OnScreenshotTaken(this, new ScreenCaptureOnScreenshotTakenEventArgs(this, this.jpgCreator));
        }
    }
}

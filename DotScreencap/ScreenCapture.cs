namespace DotScreencap
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Represents the ScreenCapture class.
    /// </summary>
    public class ScreenCapture
    {
        private Bitmap screenBitmap;
        private BitmapImage screenBitmapImage;
        private Rectangle screenSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenCapture"/> class.
        /// </summary>
        public ScreenCapture()
        {
            this.ScreenSize = Screen.PrimaryScreen.Bounds;
        }

        /// <summary>
        /// Gets the ScreenBitmapImage.
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
        /// <value>
        /// Size of the screen.
        /// </value>
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
        /// Creates a Bitmap of the users screen.
        /// </summary>
        public void GetBitmapOfScreen()
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
        public void ConvertBitmapToBitmapImage()
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

            Screenshot.SaveScreenshotAsJPG(this.ScreenBitmapImage, filename);
        }
    }
}

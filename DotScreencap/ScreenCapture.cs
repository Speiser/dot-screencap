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
    public sealed class ScreenCapture
    {
        private AnimationCreator animationCreator;
        private Bitmap screenBitmap;
        private BitmapImage screenBitmapImage;
        private PictureCreator pictureCreator;
        private Rectangle screenSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenCapture"/> class.
        /// If you want to use this with a different screen you have to use:
        ///     1. ScreenCapture.AllScreens
        ///     2. Find the screen you want to capture.
        ///     3. ScreenCapture.ChangeScreen(yourNewScreen)
        /// </summary>
        public ScreenCapture()
        {
            this.AllScreens = Screen.AllScreens;
            this.ScreenSize = Screen.PrimaryScreen.Bounds;
            this.animationCreator = new AnimationCreator(this);
        }

        /// <summary>
        /// Is fired after a screenshot was taken.
        /// </summary>
        public event EventHandler<ScreenCaptureOnScreenshotTakenEventArgs> OnScreenshotTaken;

        /// <summary>
        /// Is fired after an animation was created.
        /// </summary>
        public event EventHandler<ScreenCaptureOnAnimationCreatedEventArgs> OnAnimationCreated;

        /// <summary>
        /// Gets the animation creator.
        /// Used to subscribe to the OnOutOfMemoryExceptionThrown event.
        /// </summary>
        public AnimationCreator AnimationCreator
        {
            get
            {
                return this.animationCreator;
            }
        }

        /// <summary>
        /// Gets or sets the screen bitmap.
        /// </summary>
        public Bitmap ScreenBitmap
        {
            get
            {
                return this.screenBitmap;
            }
            set
            {
                this.screenBitmap = value;
            }
        }

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
        /// Gets a System.Drawing.Rectangle, representing the bounds of the display.
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
        /// An array of type System.Windows.Forms.Screen, containing all displays on the system.
        /// </summary>
        public Screen[] AllScreens
        {
            get;
            private set;
        }

        /// <summary>
        /// Is used to change your screen from e.g.: primary screen to second screen etc.
        /// Usage:
        ///     1. ScreenCapture.AllScreens
        ///     2. Find the screen you want to capture.
        ///     3. ScreenCapture.ChangeScreen(yourNewScreen)
        /// </summary>
        public void ChangeScreen(Screen screen)
        {
            this.ScreenSize = screen.Bounds;
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

            if (this.pictureCreator == null)
            {
                this.pictureCreator = (filename.Length < 1) ? new PictureCreator(this.ScreenBitmapImage) :
                                                              new PictureCreator(this.ScreenBitmapImage, filename[0]);
            }
            else
            {
                this.pictureCreator.Image = this.ScreenBitmapImage;
            }

            this.pictureCreator.SaveScreenshotAsJPG();
            this.FireOnScreenshotTaken();
        }

        /// <summary>
        /// Saves an animated *.gif to the execution folder.
        /// </summary>
        /// <param name="frames">Amount of frames that will be captured.</param>
        /// <param name="wait">Time in ms between each frame.</param>
        /// <param name="filename">Possibly specified filename.</param>
        public void CreateGIF(int frames, int wait, params string[] filename)
        {
            this.animationCreator.SaveAnimationAsGif(frames, wait);
            this.FireOnAnimationCreated();
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
            // screen.CopyFromScreen(Upper left corner X, Y, 0, 0, new Size(lower right corner X,Y));
            screen.CopyFromScreen(this.ScreenSize.X, 0, 0, 0, new Size(this.ScreenSize.Width, this.ScreenSize.Height));
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

        private void FireOnScreenshotTaken()
        {
            if (this.OnScreenshotTaken != null)
            {
                this.OnScreenshotTaken(this, new ScreenCaptureOnScreenshotTakenEventArgs(this, this.pictureCreator));
            }
        }

        private void FireOnAnimationCreated()
        {
            if (this.OnAnimationCreated != null)
            {
                this.OnAnimationCreated(this, new ScreenCaptureOnAnimationCreatedEventArgs(this, this.AnimationCreator));
            }
        }
    }
}

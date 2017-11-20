using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace DotScreencap
{
    /// <summary>
    /// Represents the <see cref="ScreenCapture"/> class.
    /// Visit https://github.com/Speiser/dot-screencap.
    /// </summary>
    public class ScreenCapture
    {
        private int _scalingFactor = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenCapture"/> class.
        /// </summary>
        public ScreenCapture()
        {
            var screenSize = Screen.PrimaryScreen.Bounds;
            this.ScreenRegion = new ScreenRegion(new Point(0, 0), 
                                                 new Point(screenSize.Width, screenSize.Height));
        }

        /// <summary>
        /// Gets an array of all displays on the system.
        /// </summary>
        public Screen[] AllScreens => Screen.AllScreens;

        /// <summary>
        /// Gets or sets the scaling factor.
        /// This value is used to lower the gif pixel size.
        /// Size = Height / ScalingFactor: Width / ScalingFactor.
        /// Default is 1. Raise the value if you want to record long gifs.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if value is less than 1.
        /// </exception>
        public int ScalingFactor
        {
            get => _scalingFactor;
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Value has to greater than or equal to 1."
                    );

                _scalingFactor = value;
            }
        }

        /// <summary>
        /// Gets or sets the current <see cref="IScreenRegion"/>.
        /// </summary>
        public IScreenRegion ScreenRegion { get; set; }

        /// <summary>
        /// Takes a screenshot and saves it to the execution folder.
        /// </summary>
        /// <param name="format">[Optional] Picture format.</param>
        /// <param name="filename">[Optional] File name.</param>
        /// <param name="quality">[Optional] Quality level of a <see cref="PictureFormat.Jpg"/>.</param>
        public void TakeScreenshot(PictureFormat format = PictureFormat.Jpg, string filename = "screenshot", int quality = 100)
        {
            var bitmap = this.GetBitmapOfScreen();
            var bitmapImage = this.GetBitmapImageFromBitmap(bitmap);

            PictureCreator.TakeScreenshot(bitmapImage, format, filename, quality);

            bitmap.Dispose();
            bitmapImage.StreamSource.Close();
            bitmapImage = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Records and saves an animation to the execution folder.
        /// </summary>
        /// <param name="frames">Amount of frames that will be captured.</param>
        /// <param name="wait">Time in milliseconds between each frame.</param>
        /// <param name="filename">[Optional] File name.</param>
        public void RecordAnimation(int frames, int wait, string filename = "recording.gif")
        {
            AnimationCreator.CreateAnimation(this, frames, wait, filename);
        }

        internal Bitmap GetBitmapOfScreen()
        {
            var bitmap = new Bitmap(this.ScreenRegion.Width, this.ScreenRegion.Height);
            using (var screen = Graphics.FromImage(bitmap))
            {
                screen.CopyFromScreen(
                    this.ScreenRegion.UpperLeftCorner.X,
                    this.ScreenRegion.UpperLeftCorner.Y, 0, 0,
                    new Size(this.ScreenRegion.Width, this.ScreenRegion.Height)
                );
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (ScalingFactor > 1)
            {
                bitmap = new Bitmap(bitmap, new Size(this.ScreenRegion.Width / this.ScalingFactor,
                                                     this.ScreenRegion.Height / this.ScalingFactor));
            }

            return bitmap;
        }

        internal BitmapImage GetBitmapImageFromBitmap(Bitmap bitmap)
        {
            var bitmapImage = new BitmapImage();

            // Don´t dispose the memory stream here or
            // BitmapFrame.Create(bitmap) in PictureCreator
            // will throw an ObjectDisposedException!
            // The memory stream is disposed in 
            // this.TakeScreenshot()...
            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);

            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}

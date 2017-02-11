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
        /// <summary>
        /// Represents the bounds of the display.
        /// </summary>
        private Rectangle _screenSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenCapture"/> class.
        /// </summary>
        public ScreenCapture()
        {
            _screenSize = Screen.PrimaryScreen.Bounds;
            this.ScreenRegion = new ScreenRegion(new Point(0, 0), 
                                                 new Point(_screenSize.Width, _screenSize.Height));
        }

        /// <summary>
        /// Gets an array of all displays on the system.
        /// </summary>
        public Screen[] AllScreens => Screen.AllScreens;

        /// <summary>
        /// Gets a <see cref="BitmapSource"/> that can be used with XAML.
        /// </summary>
        public BitmapImage BitmapImage => this.GetBitmapImageFromBitmap(this.GetBitmapOfScreen());

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
            PictureCreator.EncodeScreenshot(this.BitmapImage, format, filename, quality);
        }

        internal Bitmap GetBitmapOfScreen()
        {
            int width = this.ScreenRegion.LowerRightCorner.X - this.ScreenRegion.UpperLeftCorner.X;
            int height = this.ScreenRegion.LowerRightCorner.Y - this.ScreenRegion.UpperLeftCorner.Y;
            var bitmap = new Bitmap(width, height);
            var screen = Graphics.FromImage(bitmap);
            screen.CopyFromScreen(this.ScreenRegion.UpperLeftCorner.X, 
                                  this.ScreenRegion.UpperLeftCorner.Y, 0, 0,
                                  new Size(width, height));

            return bitmap;
        }

        internal BitmapImage GetBitmapImageFromBitmap(Bitmap bitmap)
        {
            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}

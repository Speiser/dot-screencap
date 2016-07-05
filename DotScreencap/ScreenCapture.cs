using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using DotScreencap.Enums;
using DotScreencap.EventArgs;

namespace DotScreencap
{
    /// <summary>
    ///     Represents the ScreenCapture class.
    /// </summary>
    public sealed class ScreenCapture
    {
        private PictureCreator _pictureCreator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScreenCapture" /> class.
        ///     If you want to use this with a different screen you have to use:
        ///     1. ScreenCapture.AllScreens
        ///     2. Find the screen you want to capture.
        ///     3. ScreenCapture.ChangeScreen(yourNewScreen)
        /// </summary>
        public ScreenCapture()
        {
            AllScreens = Screen.AllScreens;
            ScreenSize = Screen.PrimaryScreen.Bounds;
            ScreenRegion = new ScreenRegion(new Point(0, 0), new Point(ScreenSize.Width, ScreenSize.Height));
            AnimationCreator = new AnimationCreator(this);
        }

        /// <summary>
        ///     Gets the animation creator.
        ///     Used to subscribe to the OnOutOfMemoryExceptionThrown event.
        /// </summary>
        public AnimationCreator AnimationCreator { get; }

        /// <summary>
        ///     Gets or sets the screen bitmap.
        /// </summary>
        public DirectBitmap ScreenBitmap { get; set; }

        /// <summary>
        ///     Gets the ScreenBitmapImage.
        ///     Can be used for WPF ImageBox.
        /// </summary>
        public BitmapImage ScreenBitmapImage { get; private set; }

        /// <summary>
        ///     Gets a System.Drawing.Rectangle, representing the bounds of the display.
        /// </summary>
        public Rectangle ScreenSize { get; private set; }

        /// <summary>
        ///     Gets an array of type System.Windows.Forms.Screen, containing all displays on the system.
        /// </summary>
        public Screen[] AllScreens { get; private set; }

        /// <summary>
        ///     Gets or sets the points of the selected screen region.
        /// </summary>
        public ScreenRegion ScreenRegion { get; set; }

        /// <summary>
        ///     Is fired after a screenshot was taken.
        /// </summary>
        public event EventHandler<ScreenCaptureOnScreenshotTakenEventArgs> OnScreenshotTaken;

        /// <summary>
        ///     Is fired after an animation was created.
        /// </summary>
        public event EventHandler<ScreenCaptureOnAnimationCreatedEventArgs> OnAnimationCreated;

        /// <summary>
        ///     Is used to change your screen from e.g.: primary screen to second screen etc.
        ///     Usage:
        ///     1. ScreenCapture.AllScreens
        ///     2. Find the screen you want to capture.
        ///     3. ScreenCapture.ChangeScreen(yourNewScreen)
        /// </summary>
        public void ChangeScreen(Screen screen)
        {
            ScreenSize = screen.Bounds;
        }

        /// <summary>
        ///     Saves a *.jpg to the execution folder.
        /// </summary>
        /// <param name="format">Specified picture format.</param>
        /// <param name="filename">Possibly specified filename.</param>
        public void TakeScreenshot(PictureFormat format, params string[] filename)
        {
            GetBitmapOfScreen();
            ConvertBitmapToBitmapImage();

            if (ScreenBitmapImage == null)
            {
                throw new NullReferenceException();
            }

            if (_pictureCreator == null)
            {
                _pictureCreator = filename.Length < 1
                    ? new PictureCreator(ScreenBitmapImage, format)
                    : new PictureCreator(ScreenBitmapImage, filename[0], format);
            }
            else
            {
                _pictureCreator.Format = format;
                _pictureCreator.Image = ScreenBitmapImage;
            }

            _pictureCreator.SaveScreenshot();

            RaiseOnScreenshotTaken();
        }

        /// <summary>
        ///     Saves an animated *.gif to the execution folder.
        /// </summary>
        /// <param name="frames">Amount of frames that will be captured.</param>
        /// <param name="wait">Time in milliseconds between each frame.</param>
        /// <param name="filename">Possibly specified filename.</param>
        public void CreateGif(int frames, int wait, params string[] filename)
        {
            AnimationCreator.SaveAnimationAsGif(frames, wait);
            RaiseOnAnimationCreated();
        }

        /// <summary>
        ///     Creates a Bitmap of the users screen.
        /// </summary>
        public void GetBitmapOfScreen(bool raiseOnScreenShotEvent = false)
        {
            if (ScreenSize == null)
            {
                throw new NullReferenceException();
            }

            var width = ScreenRegion.LowerRightCorner.X - ScreenRegion.UpperLeftCorner.X;
            var height = ScreenRegion.LowerRightCorner.Y - ScreenRegion.UpperLeftCorner.Y;
            ScreenBitmap = new DirectBitmap(width, height);
            var screen = Graphics.FromImage(ScreenBitmap.Bitmap);
            screen.CopyFromScreen(ScreenRegion.UpperLeftCorner.X, ScreenRegion.UpperLeftCorner.Y, 0, 0,
                new Size(width, height));

            if (raiseOnScreenShotEvent)
            {
                RaiseOnScreenshotTaken();
            }
        }

        /// <summary>
        ///     Converts a Bitmap to a BitmapImage using a memory stream.
        /// </summary>
        private void ConvertBitmapToBitmapImage()
        {
            if (ScreenBitmap == null)
            {
                throw new NullReferenceException();
            }

            var ms = new MemoryStream();
            ScreenBitmap.Bitmap.Save(ms, ImageFormat.Bmp);
            ScreenBitmapImage = new BitmapImage();
            ScreenBitmapImage.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            ScreenBitmapImage.StreamSource = ms;
            ScreenBitmapImage.EndInit();
        }

        private void RaiseOnScreenshotTaken()
        {
            OnScreenshotTaken?.Invoke(this, new ScreenCaptureOnScreenshotTakenEventArgs(this, _pictureCreator));
        }

        private void RaiseOnAnimationCreated()
        {
            OnAnimationCreated?.Invoke(this, new ScreenCaptureOnAnimationCreatedEventArgs(this, AnimationCreator));
        }
    }
}
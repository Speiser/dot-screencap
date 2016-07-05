namespace DotScreencap.EventArgs
{
    /// <summary>
    ///     Represents the ScreenCaptureOnScreenshotTakenEventArgs class.
    /// </summary>
    public class ScreenCaptureOnScreenshotTakenEventArgs : System.EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ScreenCaptureOnScreenshotTakenEventArgs" /> class.
        /// </summary>
        public ScreenCaptureOnScreenshotTakenEventArgs(ScreenCapture screencap, PictureCreator pictureCreator)
        {
            ScreenCap = screencap;
            PictureCreator = pictureCreator;
        }

        /// <summary>
        ///     Gets the screen capturer.
        /// </summary>
        public ScreenCapture ScreenCap { get; private set; }

        /// <summary>
        ///     Gets the jpg creator.
        /// </summary>
        public PictureCreator PictureCreator { get; private set; }
    }
}
namespace DotScreencap
{
    using System;

    /// <summary>
    /// Represents the ScreenCaptureOnScreenshotTakenEventArgs class.
    /// </summary>
    public class ScreenCaptureOnScreenshotTakenEventArgs : EventArgs 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenCaptureOnScreenshotTakenEventArgs"/> class.
        /// </summary>
        public ScreenCaptureOnScreenshotTakenEventArgs(ScreenCapture screencap, PictureCreator pictureCreator)
        {
            this.ScreenCap = screencap;
            this.PictureCreator = pictureCreator;
        }

        /// <summary>
        /// Gets the screen capturer.
        /// </summary>
        public ScreenCapture ScreenCap
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the jpg creator.
        /// </summary>
        public PictureCreator PictureCreator
        {
            get;
            private set;
        }
    }
}
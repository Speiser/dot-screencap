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
        public ScreenCaptureOnScreenshotTakenEventArgs(ScreenCapture screencap, JpgCreator jpgcreator)
        {
            this.ScreenCap = screencap;
            this.JpgCreator = jpgcreator;
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
        public JpgCreator JpgCreator
        {
            get;
            private set;
        }
    }
}
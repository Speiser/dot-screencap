namespace DotScreencap.EventArgs
{
    /// <summary>
    ///     Represents the ScreenCaptureOnAnimationCreatedEventArgs class.
    /// </summary>
    public class ScreenCaptureOnAnimationCreatedEventArgs : System.EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ScreenCaptureOnAnimationCreatedEventArgs" /> class.
        /// </summary>
        public ScreenCaptureOnAnimationCreatedEventArgs(ScreenCapture screencap, AnimationCreator animationCreator)
        {
            ScreenCap = screencap;
            AnimationCreator = animationCreator;
        }

        /// <summary>
        ///     Gets the screen capturer.
        /// </summary>
        public ScreenCapture ScreenCap { get; private set; }

        /// <summary>
        ///     Gets the animation creator.
        /// </summary>
        public AnimationCreator AnimationCreator { get; private set; }
    }
}
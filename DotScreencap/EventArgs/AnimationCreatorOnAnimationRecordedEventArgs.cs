namespace DotScreencap
{
    using System;

    /// <summary>
    ///     Represents the ScreenCaptureOnAnimationCreatedEventArgs class.
    /// </summary>
    public class AnimationCreatorOnAnimationRecordedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AnimationCreatorOnAnimationRecordedEventArgs" /> class.
        /// </summary>
        public AnimationCreatorOnAnimationRecordedEventArgs(AnimationCreator animationCreator)
        {
            AnimationCreator = animationCreator;
        }

        /// <summary>
        ///     Gets the animation creator.
        /// </summary>
        public AnimationCreator AnimationCreator { get; private set; }
    }
}
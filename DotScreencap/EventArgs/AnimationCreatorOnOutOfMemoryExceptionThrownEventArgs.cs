namespace DotScreencap
{
    using System;

    /// <summary>
    /// Represents the AnimationCreatorOnOutOfMemoryExceptionThrownEventArgs class.
    /// </summary>
    public class AnimationCreatorOnOutOfMemoryExceptionThrownEventArgs : EventArgs 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationCreatorOnOutOfMemoryExceptionThrownEventArgs"/> class.
        /// </summary>
        public AnimationCreatorOnOutOfMemoryExceptionThrownEventArgs(OutOfMemoryException e, int thrownAfterXFrames)
        {
            this.Exception = e;
            this.ThrownAfterXFrames = thrownAfterXFrames;
        }

        /// <summary>
        /// Gets the OutOfMemoryException.
        /// </summary>
        public OutOfMemoryException Exception
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of the last frame which was added to the animation.
        /// </summary>
        public int ThrownAfterXFrames
        {
            get;
            private set;
        }
    }
}
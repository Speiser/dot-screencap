namespace DotScreencap
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Represents the animate class.
    /// Is used for the creation of animated gifs.
    /// </summary>
    public sealed class AnimatedGifCreator
    {
        private int fps;

        /// <summary>
        /// Duration of the gif in seconds.
        /// </summary>
        private int duration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimatedGifCreator"/> class.
        /// </summary>
        public AnimatedGifCreator() { }
    }
}

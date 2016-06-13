namespace DotScreencap
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Represents the screen region class.
    /// </summary>
    public class ScreenRegion
    {
        /// <summary>
        /// Used to select a screenregion for animations, screenshots and videos.
        /// Use: SetUpperLeftCorner(), SetLowerRightCorner().
        /// </summary>
        public ScreenRegion() { }

        /// <summary>
        /// Gets the upper left corner position.
        /// </summary>
        public Point UpperLeftCorner
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the lower right corner position.
        /// </summary>
        public Point LowerRightCorner
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the upper left corner to the current mouse position.
        /// </summary>
        public void SetUpperLeftCorner()
        {
            this.UpperLeftCorner = Control.MousePosition;
        }

        /// <summary>
        /// Sets the lower right corner to the current mouse position.
        /// </summary>
        public void SetLowerRightCorner()
        {
            this.LowerRightCorner = Control.MousePosition;
        }
    }
}
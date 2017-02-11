using System.Drawing;

namespace DotScreencap
{
    /// <summary>
    /// Represents the <see cref="ScreenRegion"/> class.
    /// </summary>
    public class ScreenRegion : IScreenRegion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenRegion"/> class.
        /// </summary>
        /// <param name="upperleft">The upper left corner <see cref="Point"/>.</param>
        /// <param name="lowerright">The lower right corner <see cref="Point"/>.</param>
        public ScreenRegion(Point upperleft, Point lowerright)
        {
            this.UpperLeftCorner = upperleft;
            this.LowerRightCorner = lowerright;
        }

        /// <summary>
        /// Gets or sets the upper left corner <see cref="Point"/>.
        /// </summary>
        public Point UpperLeftCorner { get; set; }

        /// <summary>
        /// Gets or sets the lower right corner <see cref="Point"/>.
        /// </summary>
        public Point LowerRightCorner { get; set; }
    }
}

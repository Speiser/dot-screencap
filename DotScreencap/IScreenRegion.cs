using System.Drawing;

namespace DotScreencap
{
    /// <summary>
    /// Represents the <see cref="IScreenRegion"/> interface.
    /// </summary>
    public interface IScreenRegion
    {
        /// <summary>
        /// Gets or sets the upper left corner <see cref="Point"/>.
        /// </summary>
        Point UpperLeftCorner { get; set; }

        /// <summary>
        /// Gets or sets the lower right corner <see cref="Point"/>.
        /// </summary>
        Point LowerRightCorner { get; set; }
    }
}

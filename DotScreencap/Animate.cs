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
    public static class Animate
    {
        /// <summary>
        /// Creates a *.gif from a list of bitmaps.
        /// </summary>
        public static void SaveAnimationAsGif(List<Bitmap> images)
        {
            var encoder = new GifBitmapEncoder();

            while (0 != images.Count)
            {
                try
                {
                    var source = Imaging.CreateBitmapSourceFromHBitmap(
                                 images[0].GetHbitmap(),
                                 IntPtr.Zero,
                                 Int32Rect.Empty,
                                 BitmapSizeOptions.FromEmptyOptions());

                    for (int i = 0; i < 2; i++)
                    {
                        encoder.Frames.Add(BitmapFrame.Create(source));
                    }
                    
                    images.RemoveAt(0);
                }
                catch (OutOfMemoryException)
                {
                }
            }

            encoder.Save(new FileStream("test.gif", FileMode.Create));
        }
    }
}

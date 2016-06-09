namespace DotScreencap
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Media.Imaging;
    using System.Windows.Interop;
    using System.IO;
    using System.Windows;

    public static class Animate
    {
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

                    for (int i = 0; i < 5; i++)
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

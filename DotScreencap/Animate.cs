namespace DotScreencap
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Media.Imaging;
    using System.Windows.Interop;
    using System.IO;

    public static class Animate
    {
        public static void SaveAnimationAsGif(List<Bitmap> images)
        {
            GifBitmapEncoder gEnc = new GifBitmapEncoder();
            foreach (Bitmap bmpImage in images)
            {
                try
                {
                    var src = Imaging.CreateBitmapSourceFromHBitmap(
                                bmpImage.GetHbitmap(),
                                IntPtr.Zero,
                                System.Windows.Int32Rect.Empty,
                                BitmapSizeOptions.FromEmptyOptions());
                    gEnc.Frames.Add(BitmapFrame.Create(src));
                }
                catch (Exception)
                {
                }
            }
            gEnc.Save(new FileStream("test.gif", FileMode.Create));
        }
    }
}

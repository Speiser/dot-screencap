using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace DotScreencap
{
    internal static class AnimationCreator
    {
        public static void CreateAnimation(ScreenCapture sc, int frames, int wait, string filename)
        {
            var encoder = new GifBitmapEncoder();

            for (var i = 0; i < frames; i++)
            {
                using (var bmp = sc.GetBitmapOfScreen())
                {
                    var size = BitmapSizeOptions.FromEmptyOptions();
                    var source = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, size);
                    var frame = BitmapFrame.Create(source);
                    encoder.Frames.Add(frame);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                Thread.Sleep(wait);
            }

            Thread.Sleep(1000);
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                encoder.Save(fs);
                encoder.Frames.Clear();
                encoder = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}

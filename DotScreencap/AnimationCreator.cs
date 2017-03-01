using System;
using System.Diagnostics;
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
                try
                {
                    var source = Imaging.CreateBitmapSourceFromHBitmap(
                                 sc.GetBitmapOfScreen().GetHbitmap(),
                                 IntPtr.Zero,
                                 Int32Rect.Empty,
                                 BitmapSizeOptions.FromEmptyOptions());
                    encoder.Frames.Add(BitmapFrame.Create(source));
                    Thread.Sleep(wait);
                }
                catch (OutOfMemoryException e)
                {
                    Debug.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            Thread.Sleep(1000);
            encoder.Save(new FileStream(filename, FileMode.Create));
        }
    }
}

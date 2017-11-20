using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace DotScreencap
{
    /// <summary>
    /// Represents the <see cref="PictureCreator"/> class.
    /// </summary>
    internal static class PictureCreator
    {
        internal static void TakeScreenshot(BitmapImage bitmap, PictureFormat format, string filename, int quality)
        {
            BitmapEncoder encoder;

            switch (format)
            {
                case PictureFormat.Jpg:
                    filename += ".jpg";
                    encoder = new JpegBitmapEncoder { QualityLevel = quality };
                    break;
                case PictureFormat.Bmp:
                    filename += ".bmp";
                    encoder = new BmpBitmapEncoder();
                    break;
                default:
                    throw new ArgumentException($"Unknown format {nameof(format)}");
            }

            using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(fs);
                encoder.Frames.Clear();
            }
        }
    }
}

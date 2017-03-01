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
            EncodeScreenshot(bitmap, format, filename, quality);
        }

        /// <summary>
        /// Creates the encoder which is used to save the screenshot and calls <see cref="SaveScreenshot(BitmapImage, string, BitmapEncoder)"/>.
        /// </summary>
        /// <param name="bitmap">Bitmap image.</param>
        /// <param name="format">Picture format.</param>
        /// <param name="filename">File name.</param>
        /// <param name="quality">Quality level of a <see cref="PictureFormat.Jpg"/>.</param>
        private static void EncodeScreenshot(BitmapImage bitmap, PictureFormat format, string filename, int quality)
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

            SaveScreenshot(bitmap, filename, encoder);
        }

        /// <summary>
        /// Saves the screenshot with a filestream.
        /// </summary>
        /// <param name="bitmap">Bitmap image.</param>
        /// <param name="filename">File name.</param>
        /// <param name="encoder">Bitmap encoder.</param>
        private static void SaveScreenshot(BitmapImage bitmap, string filename, BitmapEncoder encoder)
        {
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                encoder.Save(fs);
            }
        }
    }
}

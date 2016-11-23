namespace DotScreencap
{
    using System;
    using System.IO;
    using System.Windows.Media.Imaging;

    /// <summary>
    ///     Represents the picture creator class.
    ///     Is used to create local picture files.
    /// </summary>
    public class PictureCreator
    {
        private int _count = 1;
        private string _filename;
        private BitmapImage _image;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PictureCreator" /> class.
        /// </summary>
        public PictureCreator(BitmapImage image, PictureFormat format) : this(image, "screenshot", format)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PictureCreator" /> class.
        /// </summary>
        public PictureCreator(BitmapImage image, string filename, PictureFormat format)
        {
            Image = image;
            Filename = filename;
            Format = format;
        }

        /// <summary>
        ///     The format to save the image as.
        /// </summary>
        public PictureFormat Format { get; set; }

        /// <summary>
        ///     Gets or sets the bitmap image.
        /// </summary>
        public BitmapImage Image
        {
            get { return _image; }

            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }

                _image = value;
            }
        }

        /// <summary>
        ///     Gets the filename of the screenshot.
        /// </summary>
        public string Filename
        {
            get { return _filename; }

            private set
            {
                if (value.Length < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _filename = value;
            }
        }

        /// <summary>
        ///     Saves a picture to the execution folder.
        /// </summary>
        public void SaveScreenshot()
        {
            switch (Format)
            {
                case PictureFormat.Jpg:
                {
                    SaveScreenshotAsJpg();
                    break;
                }

                case PictureFormat.Bmp:
                {
                    SaveScreenshotAsBmp();
                    break;
                }
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Saves a *.jpg to the execution folder.
        /// </summary>
        public void SaveScreenshotAsJpg(int qualityLevel = 100)
        {
            var encoder = new JpegBitmapEncoder {QualityLevel = qualityLevel};
            var screenshotName = Filename == "screenshot" ? Filename + _count + ".jpg" : Filename + ".jpg";

            _count++;
            var frame = BitmapFrame.Create(Image);
            encoder.Frames.Add(frame);

            using (var filestream = new FileStream(screenshotName, FileMode.OpenOrCreate))
            {
                encoder.Save(filestream);
            }
        }

        /// <summary>
        ///     Saves a *.bmp to the execution folder.
        /// </summary>
        public void SaveScreenshotAsBmp()
        {
            var encoder = new BmpBitmapEncoder();
            var screenshotName = Filename == "screenshot" ? Filename + _count + ".bmp" : Filename + ".bmp";

            _count++;
            var frame = BitmapFrame.Create(Image);
            encoder.Frames.Add(frame);

            using (var filestream = new FileStream(screenshotName, FileMode.OpenOrCreate))
            {
                encoder.Save(filestream);
            }
        }
    }
}
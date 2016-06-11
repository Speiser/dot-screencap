namespace DotScreencap
{
    using System;
    using System.IO;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Represents the picture creator class.
    /// Is used to create local picture files.
    /// </summary>
    public sealed class PictureCreator
    {
        private BitmapImage image;
        private PictureFormat format = PictureFormat.jpg;
        private string filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureCreator"/> class.
        /// </summary>
        public PictureCreator(BitmapImage image) : this(image, "screenshot") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureCreator"/> class.
        /// </summary>
        public PictureCreator(BitmapImage image, string filename)
        {
            this.Image = image;
            this.Filename = filename;
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        public BitmapImage Image
        {
            get
            {
                return this.image;
            }

            private set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }

                this.image = value;
            }
        }

        /// <summary>
        /// Gets the filename of the screenshot.
        /// </summary>
        public string Filename
        {
            get
            {
                return this.filename;
            }

            private set
            {
                if (value.Length < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                this.filename = value;
            }
        }

        /// <summary>
        /// Saves a picture to the execution folder.
        /// </summary>
        public void SaveScreenshot()
        {
            switch (this.format)
            {
                case PictureFormat.jpg:
                    {
                        this.SaveScreenshotAsJPG();
                        break;
                    }
            }
        }

        /// <summary>
        /// Saves a *.jpg to the execution folder.
        /// </summary>
        public void SaveScreenshotAsJPG()
        {
            string screenshotName;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            screenshotName = this.Filename + ".jpg";
            encoder.Frames.Add(BitmapFrame.Create(this.Image));

            encoder.Save(new FileStream(screenshotName, FileMode.Create));
        }
    }
}

namespace DotScreencap
{
    using System;
    using System.IO;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Represents the JpgCreator class.
    /// Is used to create screenshots.
    /// </summary>
    public sealed class JpgCreator
    {
        private BitmapImage image;
        private string filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="JpgCreator"/> class.
        /// </summary>
        public JpgCreator(BitmapImage image) : this(image, "screenshot") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="JpgCreator"/> class.
        /// </summary>
        public JpgCreator(BitmapImage image, string filename)
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

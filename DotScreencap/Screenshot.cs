namespace DotScreencap
{
    using System;
    using System.IO;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Represents the screenshot class.
    /// </summary>
    public class Screenshot
    {
        /// <summary>
        /// Saves a *.jpg to the execution folder.
        /// </summary>
        /// <param name="screen">BitmapImage.</param>
        /// <param name="filename">Possibly specified filename.</param>
        public static void SaveScreenshotAsJPG(BitmapImage screen, params string[] filename)
        {
            string name;
            string screenshotName;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            if (filename.Length == 0)
            {
                Guid guid = Guid.NewGuid();
                name = guid.ToString();
            }
            else
            {
                name = filename[0];
            }

            screenshotName = name + ".jpg";
            encoder.Frames.Add(BitmapFrame.Create(screen));

            using (var filestream = new FileStream(screenshotName, FileMode.Create))
            {
                encoder.Save(filestream);
            }
        }
    }
}

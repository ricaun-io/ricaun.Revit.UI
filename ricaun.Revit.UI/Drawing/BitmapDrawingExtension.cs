using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace ricaun.Revit.UI.Drawing
{
    /// <summary>
    /// BitmapDrawingExtension
    /// </summary>
    public static class BitmapDrawingExtension
    {
        /// <summary>
        /// Convert <paramref name="bitmap"/> to <seealso cref="BitmapSource"/>
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(this System.Drawing.Bitmap bitmap)
        {
            var data = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                data.Width, data.Height, 96.0, 96.0,
                System.Windows.Media.PixelFormats.Bgra32, null,
                data.Scan0, data.Stride * data.Height, data.Stride);

            bitmap.UnlockBits(data);

            return bitmapSource;
        }

        /// <summary>
        /// Convert <paramref name="icon"/> to <seealso cref="BitmapSource"/>
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(this System.Drawing.Icon icon)
        {
            var stream = new MemoryStream();
            icon.Save(stream);
            var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.Default);
            return decoder.Frames[0];
        }

        /// <summary>
        /// Convert <paramref name="image"/> to <seealso cref="BitmapSource"/>
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(this System.Drawing.Image image)
        {
            var bitmap = new System.Drawing.Bitmap(image);
            return bitmap.GetBitmapSource();
        }

        internal static BitmapSource Base64ToBitmapSource(string base64)
        {
            var convert = Convert.FromBase64String(base64);
            var image = System.Drawing.Bitmap.FromStream(new MemoryStream(convert));
            return image.GetBitmapSource();
        }
    }
}

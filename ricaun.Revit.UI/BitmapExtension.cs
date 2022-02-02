using System;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// BitmapExtension
    /// </summary>
    public static class BitmapExtension
    {
        /// <summary>
        /// GetBitmapSource
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(this System.Drawing.Bitmap bitmap)
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return bitmapSource;
        }

        /// <summary>
        /// GetBitmapSource
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(this System.Drawing.Icon icon)
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.ToBitmap().GetHicon(),
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return bitmapSource;
        }

        /// <summary>
        /// GetBitmapSource
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(this System.Drawing.Image image)
        {
            var bitmap = new System.Drawing.Bitmap(image);
            return bitmap.GetBitmapSource();
        }

        /// <summary>
        /// Transform string base64 or Uri to BitmapSource
        /// </summary>
        /// <param name="base64orUri"></param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(this string base64orUri)
        {
            if (base64orUri.StartsWith("http"))
                return new BitmapImage(new Uri(base64orUri));

            var image = System.Drawing.Bitmap.FromStream(new MemoryStream(Convert.FromBase64String(base64orUri)));
            return image.GetBitmapSource();
        }

        /// <summary>
        /// Scale <paramref name="bitmapSource"/>
        /// </summary>
        /// <param name="bitmapSource"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static BitmapSource Scale(this BitmapSource bitmapSource, double scale)
        {
            return new TransformedBitmap(bitmapSource, new ScaleTransform(scale, scale));
        }

        /// <summary>
        /// Scale <paramref name="imageSource"/>
        /// </summary>
        /// <param name="imageSource"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static BitmapSource Scale(this ImageSource imageSource, double scale)
        {
            return new TransformedBitmap(imageSource as BitmapSource, new ScaleTransform(scale, scale));
        }

    }
}

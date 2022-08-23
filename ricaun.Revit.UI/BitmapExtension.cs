using System;
using System.IO;
using System.Linq;
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

        /// <summary>
        /// Transform string base64 or Uri to BitmapSource
        /// </summary>
        /// <param name="base64orUri"></param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(this string base64orUri)
        {
            try
            {
                var uri = new Uri(base64orUri, UriKind.RelativeOrAbsolute);
                var decoder = BitmapDecoder.Create(uri, BitmapCreateOptions.None, BitmapCacheOption.Default);
                return decoder.Frames[0];
            }
            catch { }

            try
            {
                var uri = new Uri("pack://application:,,," + base64orUri, UriKind.RelativeOrAbsolute);
                var decoder = BitmapDecoder.Create(uri, BitmapCreateOptions.None, BitmapCacheOption.Default);
                return decoder.Frames[0];
            }
            catch { }

            try
            {
                var convert = Convert.FromBase64String(base64orUri);
                var image = System.Drawing.Bitmap.FromStream(new MemoryStream(convert));
                return image.GetBitmapSource();
            }
            catch { }

            return null;
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
        public static ImageSource Scale(this ImageSource imageSource, double scale)
        {
            if (imageSource is BitmapSource bitmapSource)
                return bitmapSource.Scale(scale);
            return imageSource;
        }

        /// <summary>
        /// GetBitmapFrame with Width Equal or Scale
        /// </summary>
        /// <param name="imageSource"></param>
        /// <param name="width"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TImageSource GetBitmapFrame<TImageSource>(this TImageSource imageSource, int width = 16, Action<TImageSource> action = null) where TImageSource : ImageSource
        {
            if (imageSource is BitmapFrame bitmapFrame)
            {
                if (bitmapFrame.IsDownloading)
                {
                    bitmapFrame.DownloadCompleted += (s, e) =>
                    {
                        var frames = bitmapFrame.Decoder.Frames;
                        var frame = frames.FirstOrDefault(e => e.Width == width);

                        if (frame != null)
                            imageSource = frame as TImageSource;

                        if (imageSource.Width > width)
                            imageSource = imageSource.Scale(width / imageSource.Width) as TImageSource;

                        action?.Invoke(imageSource);
                    };
                }

                var frames = bitmapFrame.Decoder.Frames;
                var frame = frames.FirstOrDefault(e => e.Width == width);

                if (frame != null)
                    imageSource = frame as TImageSource;
            }

            if (imageSource.Width > width)
                imageSource = imageSource.Scale(width / imageSource.Width) as TImageSource;

            return imageSource;
        }
    }
}

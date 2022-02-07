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
            var stream = new MemoryStream();
            icon.Save(stream);
            var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.Default);
            return decoder.Frames[0];
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
            if (base64orUri.StartsWith("pack") || base64orUri.StartsWith("http"))
            {
                var decoder = BitmapDecoder.Create(new Uri(base64orUri), BitmapCreateOptions.None, BitmapCacheOption.Default);
                return decoder.Frames[0];
            }

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
        /// <returns></returns>
        public static TImageSource GetBitmapFrame<TImageSource>(this TImageSource imageSource, int width = 16, Action<BitmapFrame> action = null) where TImageSource : ImageSource
        {
            if (imageSource is BitmapFrame bitmapFrame)
            {
                if (bitmapFrame.IsDownloading)
                {
                    bitmapFrame.DownloadCompleted += (s, e) =>
                    {
                        var frames = bitmapFrame.Decoder.Frames;
                        var frame = frames.FirstOrDefault(e => e.Width == width);
                        action?.Invoke(frame);
                    };
                }

                var frames = bitmapFrame.Decoder.Frames;
                var frame = frames.FirstOrDefault(e => e.Width == width);
                if (frame != null) return frame as TImageSource;
            }
            return imageSource;
        }
    }
}

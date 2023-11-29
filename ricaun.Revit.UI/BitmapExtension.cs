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
        internal static BitmapFrame UriToBitmapFrame(string uriString)
        {
            var uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
            var decoder = BitmapDecoder.Create(uri, BitmapCreateOptions.None, BitmapCacheOption.Default);
            return decoder.Frames[0];
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
                return UriToBitmapFrame(base64orUri);
            }
            catch { }

            try
            {
                var componentUri = "pack://application:,,,/" + base64orUri.TrimStart('/');
                return UriToBitmapFrame(componentUri);
            }
            catch { }

            try
            {
                var executingAssembly = Utils.StackTraceUtils.GetCallingAssembly();
                var assemblyName = executingAssembly.GetName().Name;
                var componentUri = $"pack://application:,,,/{assemblyName};component/" + base64orUri.TrimStart('/');
                return UriToBitmapFrame(componentUri);
            }
            catch { }

            try
            {
                return Drawing.BitmapDrawingExtension.Base64ToBitmapSource(base64orUri);
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

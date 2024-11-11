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
            return decoder.Frames.OrderBy(e => e.Width).LastOrDefault();
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
        /// 
        /// </summary>
        /// <param name="bitmapDecoder"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        internal static BitmapFrame GetBitmapFrameByDpiAndWidth(this BitmapDecoder bitmapDecoder, int width = 0)
        {
            double systemDpi = 96;

#if NET47_OR_GREATER || NET
            var imageScaleInfo = VisualTreeHelper.GetDpi(new System.Windows.Controls.Image());
            systemDpi = imageScaleInfo.PixelsPerInchX;
#endif

            var frames = bitmapDecoder.Frames;
            var frame = frames
                .OrderBy(e => e.DpiX >= systemDpi ? -systemDpi / e.DpiX : systemDpi / e.DpiX)
                .ThenBy(e => e.Width)
                .FirstOrDefault(e => Math.Round(e.Width) >= width);

            return frame;
        }

        /// <summary>
        /// GetBitmapFrame with Width Equal or Scale
        /// </summary>
        /// <param name="imageSource"></param>
        /// <param name="width"></param>
        /// <param name="downloadCompleted"></param>
        /// <returns></returns>
        /// <remarks>When <paramref name="width"/> is zero, return the smallest width frame.</remarks>
        public static TImageSource GetBitmapFrame<TImageSource>(this TImageSource imageSource, int width = 0, Action<TImageSource> downloadCompleted = null) where TImageSource : ImageSource
        {
            TImageSource ScaleDownIfWidthIsGreater(TImageSource imageSource, int width)
            {
                if (width <= 0)
                    return imageSource;

                var imageRoundWidth = Math.Round(imageSource.Width);
                if (imageRoundWidth > width)
                    imageSource = imageSource.Scale(width / imageRoundWidth) as TImageSource;

                return imageSource;
            }

            if (imageSource is BitmapFrame bitmapFrame)
            {
                if (bitmapFrame.IsDownloading)
                {
                    bitmapFrame.DownloadCompleted += (s, e) =>
                    {
                        if (bitmapFrame.Decoder.GetBitmapFrameByDpiAndWidth(width) is TImageSource frame)
                            imageSource = frame;

                        imageSource = ScaleDownIfWidthIsGreater(imageSource, width);

                        downloadCompleted?.Invoke(imageSource);
                    };
                }

                if (bitmapFrame.Decoder.GetBitmapFrameByDpiAndWidth(width) is TImageSource frame)
                    imageSource = frame;
            }

            imageSource = ScaleDownIfWidthIsGreater(imageSource, width);

            return imageSource;
        }
    }
}

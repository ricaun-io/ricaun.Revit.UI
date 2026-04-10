using ricaun.Revit.UI.Utils;
using System;
using System.IO;
using System.Linq;
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
            return decoder.GetBitmapFrameByWidthAndDpi(int.MaxValue);
        }

        private static bool CheckURLValid(this string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
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
                if (File.Exists(base64orUri))
                    return UriToBitmapFrame(base64orUri);

                if (base64orUri.CheckURLValid())
                    return UriToBitmapFrame(base64orUri);

                if (PackUri.TryParse(base64orUri, out var packUri))
                {
                    if (packUri.IsResourceExists())
                    {
                        return UriToBitmapFrame(packUri.PackPath);
                    }
                }
            }
            catch { }

            foreach (var executingAssembly in StackTraceUtils.GetResourceAssemblies())
            {
                try
                {
                    var assemblyName = executingAssembly.GetName().Name;
                    var packUri = new PackUri(assemblyName, base64orUri);
                    if (packUri.IsResourceExists())
                    {
                        return UriToBitmapFrame(packUri.PackPath);
                    }
                }
                catch { }
            }

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

#if NET47_OR_GREATER || NET
        /// <summary>
        /// Get the system DPI based on the <see cref="System.Windows.Media.VisualTreeHelper.GetDpi"/> using a new <see cref="System.Windows.Controls.Image"/>.
        /// </summary>
#else
        /// <summary>
        /// Get the system DPI based on the <see cref="System.Drawing.Graphics.DpiX"/> using a new <see cref="System.Drawing.Graphics"/> from <see cref="IntPtr.Zero"/>.
        /// </summary>
#endif
        internal static double GetSystemDpi()
        {
            double systemDpi = 96;
            try
            {
#if NET47_OR_GREATER || NET
                var imageScaleInfo = VisualTreeHelper.GetDpi(new System.Windows.Controls.Image());
                systemDpi = imageScaleInfo.PixelsPerInchX;
#else
                using (var g = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
                {
                    systemDpi = g.DpiX;
                }
#endif
            }
            catch { }
            return systemDpi;
        }

        /// <summary>
        /// System Dpi
        /// </summary>
        public readonly static double SystemDpi = GetSystemDpi();

        /// <summary>
        /// Get the bitmap frame from the <paramref name="bitmapDecoder"/> based on the DPI and width.
        /// </summary>
        /// <param name="bitmapDecoder">The bitmap decoder.</param>
        /// <param name="width">The desired width of the bitmap frame. When set to zero, the smallest width frame is returned.</param>
        /// <param name="dpi">The optimal dpi for the frame. When set to zero, <see cref="SystemDpi"/> is used.</param>
        /// <returns>The bitmap frame with the specified width or the smallest width frame.</returns>
        public static BitmapFrame GetBitmapFrameByWidthAndDpi(this BitmapDecoder bitmapDecoder, int width, int dpi = 0)
        {
            double systemDpi = dpi > 0 ? dpi : SystemDpi;

            double OrderDpiX(BitmapFrame frame)
            {
                var dpiX = Math.Round(frame.DpiX);
                return dpiX >= systemDpi ? -systemDpi / dpiX : systemDpi / dpiX;
            }

            var frames = bitmapDecoder.Frames;
            var framesOrder = frames
                .OrderBy(OrderDpiX)
                .ThenBy(e => Math.Round(e.Width));

            var widthMax = (int)Math.Round(framesOrder.LastOrDefault().Width);
            if (width > widthMax)
                width = widthMax;

            var frame = framesOrder.FirstOrDefault(e => Math.Round(e.Width) >= width);
            return frame;
        }

        /// <summary>
        /// GetBitmapFrame with Width Equal or Scale
        /// </summary>
        /// <param name="imageSource">The image source.</param>
        /// <param name="width">The desired width of the bitmap frame. When set to zero, the smallest width frame is returned.</param>
        /// <param name="downloadCompleted">An optional action to be executed when the download of the bitmap frame is completed.</param>
        /// <returns>The bitmap frame with the specified width or the scaled bitmap frame.</returns>
        /// <remarks>When <paramref name="width"/> is zero, the smallest width frame is returned.</remarks>
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
                        if (bitmapFrame.Decoder.GetBitmapFrameByWidthAndDpi(width) is TImageSource frame)
                            imageSource = frame;

                        imageSource = ScaleDownIfWidthIsGreater(imageSource, width);

                        downloadCompleted?.Invoke(imageSource);
                    };
                }

                if (bitmapFrame.Decoder.GetBitmapFrameByWidthAndDpi(width) is TImageSource frame)
                    imageSource = frame;
            }

            imageSource = ScaleDownIfWidthIsGreater(imageSource, width);

            return imageSource;
        }
    }
}

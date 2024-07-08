using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ricaun.Revit.UI.Utils
{
    /// <summary>
    /// RibbonThemeImageUtils
    /// </summary>
    public static class RibbonThemeImageUtils
    {
        private const string NAME_DARK = "dark";
        private const string NAME_LIGHT = "light";

        /// <summary>
        /// GetThemeImageSource
        /// </summary>
        /// <typeparam name="TImageSource"></typeparam>
        /// <param name="imageSource"></param>
        /// <param name="isLight"></param>
        /// <remarks>
        /// Replace 'light' to 'dark' or 'dark' to 'light' in the image source name.
        /// </remarks>
        public static TImageSource GetThemeImageSource<TImageSource>(this TImageSource imageSource, bool isLight = true) where TImageSource : ImageSource
        {
            if (imageSource.GetSourceName().TryThemeImage(isLight, out string imageTheme))
            {
                if (imageTheme.GetBitmapSource() is TImageSource themeImageSource)
                    return themeImageSource;
            }

            return imageSource;
        }

        internal static bool TryThemeImage(this string image, bool isLight, out string imageTheme)
        {
            imageTheme = string.Empty;

            if (string.IsNullOrEmpty(image))
                return false;

            var findThemeName = !isLight ? NAME_LIGHT : NAME_DARK;
            var replaceThemeName = isLight ? NAME_LIGHT : NAME_DARK;

            if (image.IndexOf(findThemeName, System.StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                imageTheme = image.Replace(findThemeName, replaceThemeName);
                return true;
            }

            return false;
        }
        internal static string GetSourceName(this ImageSource imageSource)
        {
            if (imageSource is TransformedBitmap transformedBitmap)
            {
                return transformedBitmap.Source.ToString().ToLowerInvariant();
            }
            if (imageSource is BitmapFrame bitmapFrame)
            {
                return bitmapFrame.Decoder.ToString().ToLowerInvariant();
            }
            return null;
        }
    }
}

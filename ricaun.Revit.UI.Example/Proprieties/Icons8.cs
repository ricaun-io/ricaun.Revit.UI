using System;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using ricaun.Revit.UI;

namespace ricaun.Revit.UI.Example.Proprieties
{
    /// <summary>
    /// https://icons8.com/
    /// https://img.icons8.com/small/32/000000/circled.png
    /// https://img.icons8.com/{0}/{1}/{2}/{3}.png
    /// </summary>
    public class Icons8
    {
        #region Private
        private static string Color => "000000";
        private static string Type => "color";
        private static string Size => "32";
        private static string BaseUri => @"https://img.icons8.com/{0}/{1}/{2}/{3}.png";
        #endregion

        #region Icons
        public static BitmapSource Icon([CallerMemberName] string name = null) => string.Format(BaseUri, Type, Size, Color, name.ToLower()).GetBitmapSource();
        public static BitmapSource Ok => Icon();
        public static BitmapSource Document => Icon();
        public static BitmapSource File => Icon();
        public static BitmapSource Support => Icon();
        public static BitmapSource Settings => Icon();
        public static BitmapSource About => Icon();
        public static BitmapSource Restart => Icon();
        public static BitmapSource Filter => Icon();
        public static BitmapSource Search => Icon();
        public static BitmapSource Trash => Icon();
        public static BitmapSource Home => Icon();
        public static BitmapSource Menu => Icon();
        public static BitmapSource Info => Icon();
        public static BitmapSource Circled => Icon();
        public static BitmapSource Checked => Icon();
        public static BitmapSource Cancel => Icon();
        #endregion
    }
}

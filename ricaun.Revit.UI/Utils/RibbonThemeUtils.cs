using System;

namespace ricaun.Revit.UI.Utils
{
    /// <summary>
    /// ThemeChangedEventArgs
    /// </summary>
    public sealed class ThemeChangedEventArgs : EventArgs
    {
        internal ThemeChangedEventArgs(bool isLight)
        {
            IsLight = isLight;
        }
        /// <summary>
        /// Theme is Light
        /// </summary>
        public bool IsLight { get; private set; }
        /// <summary>
        /// Theme is Dark
        /// </summary>
        public bool IsDark => !IsLight;
    }
    /// <summary>
    /// ThemeChangedEventHandler for RibbonThemeUtils
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    public delegate void ThemeChangedEventHandler(object sender, ThemeChangedEventArgs e);
    /// <summary>
    /// RibbonThemeUtils using UIFramework.ApplicationTheme
    /// </summary>
    /// <remarks>
    /// Available for Revit 2019+
    /// </remarks>
    public class RibbonThemeUtils
    {
        /// <summary>
        /// ThemeChanged
        /// </summary>
        public static event ThemeChangedEventHandler ThemeChanged;
        /// <summary>
        /// Theme is Light
        /// </summary>
        public static bool IsLight { get; private set; } = true;
        /// <summary>
        /// Theme is Dark
        /// </summary>
        public bool IsDark => !IsLight;

#if NET47_OR_GREATER || NET
        static RibbonThemeUtils()
        {
            ExecuteCurrentTheme();
            UIFramework.ApplicationTheme.CurrentTheme.PropertyChanged += CurrentTheme_PropertyChanged;
        }

        internal static void Dispose()
        {
            UIFramework.ApplicationTheme.CurrentTheme.PropertyChanged -= CurrentTheme_PropertyChanged;
            ThemeChanged = null;
        }

        internal static void ThemeChangedTest()
        {
            UIFramework.RevitRibbonControl.RibbonControl.Dispatcher.Invoke(() =>
            {
                var color = UIFramework.ApplicationTheme.CurrentTheme.ActiveTabBackgroundColor;
                UIFramework.ApplicationTheme.CurrentTheme.ActiveTabBackgroundColor = System.Windows.Media.Colors.White;
                UIFramework.ApplicationTheme.CurrentTheme.ActiveTabBackgroundColor = System.Windows.Media.Colors.Black;
                UIFramework.ApplicationTheme.CurrentTheme.ActiveTabBackgroundColor = color;
            });
        }

        private static void ExecuteCurrentTheme()
        {
            if (IsThemeChanged(out UIFramework.ApplicationTheme theme, out bool isLight))
            {
                ExecuteThemeChanged(theme, isLight);
            }
        }

        const string ApplicationCurrentThemeChangedPropertyName = "ActiveTabBackgroundColor";
        private static void CurrentTheme_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ApplicationCurrentThemeChangedPropertyName)
            {
                ExecuteCurrentTheme();
            }
        }
        private static void ExecuteThemeChanged(UIFramework.ApplicationTheme theme, bool isLight)
        {
            try
            {
                ThemeChanged?.Invoke(theme, new ThemeChangedEventArgs(IsLight));
            }
            catch { }
        }
        private static bool IsThemeChanged(out UIFramework.ApplicationTheme theme, out bool isLight)
        {
            theme = UIFramework.ApplicationTheme.CurrentTheme;
            isLight = IsThemeLight(theme);

            if (isLight == IsLight)
                return false;

            IsLight = isLight;
            return true;
        }
        private static bool IsThemeLight(UIFramework.ApplicationTheme applicationTheme)
        {
            if (applicationTheme is null)
                return true;

            var color = applicationTheme.ActiveTabBackgroundColor;
            var brightness = (byte)((color.R + color.G + color.B) / 3);
            return brightness > 128;
        }
#endif
    }
}

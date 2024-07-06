using ricaun.Revit.UI.Utils;
using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests.Themes
{
    public class RibbonThemeUtilsTests
    {
        [Test]
        public void RibbonThemeUtils_ThemeChangedEventHandler()
        {
            bool themeChanged = false;
            ThemeChangedEventHandler RibbonThemeUtils_ThemeChanged = (object sender, ThemeChangedEventArgs e) =>
            {
                themeChanged = true;
                Console.WriteLine($"ThemeChangedEvent {e.IsLight}");
            };

            try
            {
                RibbonThemeUtils.ThemeChanged += RibbonThemeUtils_ThemeChanged;
                RibbonThemeUtils.ThemeChangedTest();
                Assert.IsTrue(themeChanged);
            }
            finally
            {
                RibbonThemeUtils.ThemeChanged -= RibbonThemeUtils_ThemeChanged;
            }
        }

        [Test]
        public void ThemeChangedEventArgs_Constructor()
        {
            var themeChangedEventArgs = new ThemeChangedEventArgs(true);
            Assert.IsTrue(themeChangedEventArgs.IsLight);
            Assert.IsFalse(themeChangedEventArgs.IsDark);
        }
    }
}

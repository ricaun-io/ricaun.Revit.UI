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

        [Test]
        public void RibbonThemeUtils_LightDark()
        {
            var isLight = false;
            var isDark = false;

            RibbonThemeUtils.ThemeChangedTest(
                () => { isLight = RibbonThemeUtils.IsLight; },
                () => { isDark = RibbonThemeUtils.IsDark; }
            );

            Assert.IsTrue(isLight);
            Assert.IsTrue(isDark);
        }

        [Test]
        public void RibbonThemeUtils_TryThemeImage_IsFalse()
        {
            var lights = new string[]
            {
                "",
                "test.ico",
                "light.ico"
            };
            foreach (var image in lights)
            {
                Assert.IsFalse(image.TryThemeImage(true, out _));
            }

            var darks = new string[]
{
                "",
                "test.ico",
                "dark.ico"
            };
            foreach (var image in darks)
            {
                Assert.IsFalse(image.TryThemeImage(false, out _));
            }
        }

        [Test]
        public void RibbonThemeUtils_TryThemeImage_IsTrue()
        {
            var lights = new string[]
            {
                "dark.ico"
            };
            foreach (var image in lights)
            {
                Assert.IsTrue(image.TryThemeImage(true, out _));
            }

            var darks = new string[]
{
                "light.ico"
            };
            foreach (var image in darks)
            {
                Assert.IsTrue(image.TryThemeImage(false, out _));
            }
        }

        [TestCase("ricaun")]
        [TestCase("ricaun.Dark")]
        [TestCase("ricaun.Light")]
        public void RibbonThemeUtils_TryThemeImage_Component(string assemblyName)
        {
            var imageLight = $"pack://application:,,,/{assemblyName};component/Images/Image_Light.ico";
            var imageDark = $"pack://application:,,,/{assemblyName};component/Images/Image_Dark.ico";

            imageLight = imageLight.ToLowerInvariant();
            imageDark = imageDark.ToLowerInvariant();

            var isLight = imageDark.TryThemeImage(true, out string imageDarkToLight);
            var isDark = imageLight.TryThemeImage(false, out string imageLightToDark);

            Assert.IsTrue(isLight);
            Assert.IsTrue(isDark);

            Assert.AreEqual(imageLight, imageDarkToLight);
            Assert.AreEqual(imageDark, imageLightToDark);
        }

        [TestCase("https://ricaun.com")]
        [TestCase("https://github.com")]
        public void RibbonThemeUtils_TryThemeImage_Url(string url)
        {
            var imageLight = $"{url}/Images/Image_Light.ico";
            var imageDark = $"{url}/Images/Image_Dark.ico";

            imageLight = imageLight.ToLowerInvariant();
            imageDark = imageDark.ToLowerInvariant();

            var isLight = imageDark.TryThemeImage(true, out string imageDarkToLight);
            var isDark = imageLight.TryThemeImage(false, out string imageLightToDark);

            Assert.IsTrue(isLight);
            Assert.IsTrue(isDark);

            Assert.AreEqual(imageLight, imageDarkToLight);
            Assert.AreEqual(imageDark, imageLightToDark);
        }

    }
}

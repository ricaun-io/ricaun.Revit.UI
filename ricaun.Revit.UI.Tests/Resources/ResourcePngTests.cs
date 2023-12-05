using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests.Resources
{
    public class ResourcePngTests
    {
        const string ResourceNamePng = "Resources/Images/Revit32.png";

        [Test]
        public void GetBitmapSource_NotNull_Png()
        {
            Console.WriteLine(ResourceNamePng.GetBitmapSource());
            Assert.IsNotNull(ResourceNamePng.GetBitmapSource());
            Assert.IsNotNull(("/" + ResourceNamePng).GetBitmapSource());
        }

        [TestCase(15, 15)]
        [TestCase(16, 16)]
        [TestCase(31, 31)]
        [TestCase(32, 32)]
        [TestCase(48, 32)]
        [TestCase(64, 32)]
        public void GetBitmapFrame_Width_ScaleDown(int width, int expected)
        {
            var source = ResourceNamePng.GetBitmapSource().GetBitmapFrame(width);
            Assert.AreEqual(expected, (int)Math.Round(source.Width));
        }
    }
}
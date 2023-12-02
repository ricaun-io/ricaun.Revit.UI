using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests.Resources
{
    public class ResourceTests
    {
        const string ResourceNameIco = "Resources/Images/Revit.ico";

        [Test]
        public void GetBitmapSource_NotNull()
        {
            Console.WriteLine(ResourceNameIco.GetBitmapSource());
            Assert.IsNotNull(ResourceNameIco.GetBitmapSource());
            Assert.IsNotNull(("/" + ResourceNameIco).GetBitmapSource());
        }

        [Test]
        public void GetBitmapSource_NotNull_Component()
        {
            var component = "ricaun.Revit.UI.Tests;component/";
            Assert.IsNotNull((component + ResourceNameIco).GetBitmapSource());
            Assert.IsNotNull(("/" + component + ResourceNameIco).GetBitmapSource());
        }

        [Test]
        public void GetBitmapSource_Null_NotExist()
        {
            var component = "NotExist/" + ResourceNameIco;
            var source = component.GetBitmapSource();
            Assert.IsNull(source);
        }

        [TestCase(32)]
        public void GetBitmapSource_Width(int width)
        {
            var source = ResourceNameIco.GetBitmapSource();
            Assert.AreEqual(width, source.Width);
        }

        [TestCase(0, 16)]
        [TestCase(10, 10)]
        [TestCase(16, 16)]
        [TestCase(32, 32)]
        [TestCase(64, 32)]
        [TestCase(128, 32)]
        public void GetBitmapFrame_Width(int width, int expected)
        {
            var source = ResourceNameIco.GetBitmapSource().GetBitmapFrame(width);
            Assert.AreEqual(expected, source.Width);
        }
    }
}
using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests.Resources
{
    public class ResourceTests
    {
        const string ResourceNameIco = "Resources/Images/Revit.ico";
        const string ResourceNamePng = "Resources/Images/Revit32.png";

        [Test]
        public void GetBitmapSource_NotNull()
        {
            Console.WriteLine(ResourceNameIco.GetBitmapSource());
            Assert.IsNotNull(ResourceNameIco.GetBitmapSource());
            Assert.IsNotNull(("/" + ResourceNameIco).GetBitmapSource());
        }

        [Test]
        public void GetBitmapSource_NotNull_Png()
        {
            Console.WriteLine(ResourceNamePng.GetBitmapSource());
            Assert.IsNotNull(ResourceNamePng.GetBitmapSource());
            Assert.IsNotNull(("/" + ResourceNamePng).GetBitmapSource());
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
    }
}
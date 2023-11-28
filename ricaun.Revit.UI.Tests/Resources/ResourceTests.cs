using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests.Resources
{
    public class ResourceTests
    {
        const string ResourceName = "Resources/Images/Revit.ico";

        [Test]
        public void GetBitmapSource_NotNull()
        {
            Console.WriteLine(ResourceName.GetBitmapSource());
            Assert.IsNotNull(ResourceName.GetBitmapSource());
            Assert.IsNotNull(("/" + ResourceName).GetBitmapSource());
        }

        [Test]
        public void GetBitmapSource_NotNull_Component()
        {
            var component = "ricaun.Revit.UI.Tests;component/";
            Assert.IsNotNull((component + ResourceName).GetBitmapSource());
            Assert.IsNotNull(("/" + component + ResourceName).GetBitmapSource());
        }

        [Test]
        public void GetBitmapSource_Null_NotExist()
        {
            var component = "NotExist/" + ResourceName;
            var source = component.GetBitmapSource();
            Assert.IsNull(source);
        }
    }
}
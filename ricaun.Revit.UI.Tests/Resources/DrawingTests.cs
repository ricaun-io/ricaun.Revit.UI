using NUnit.Framework;
using ricaun.Revit.UI.Drawing;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ricaun.Revit.UI.Tests.Resources
{
    public class DrawingTests
    {
        [Test]
        public void GetBitmapSource_Image()
        {
            Image image = Images.Resources.Revit32;
            Assert.IsNotNull(image.GetBitmapSource());
        }

        [Test]
        public void GetBitmapSource_Bitmap()
        {
            Bitmap image = Images.Resources.Revit32;
            Assert.IsNotNull(image.GetBitmapSource());
        }

        [Test]
        public void GetBitmapSource_Icon()
        {
            Icon image = Images.Resources.Revit;
            Assert.IsNotNull(image.GetBitmapSource());
        }

    }
}
using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests.Resources
{
    public class ResourceTiffTests
    {
        /// <summary>
        /// Tiff file with multiple scales, 1.0x, 1.5x, 2.0x, 3.0x, 4.0x for 16x16 and 32x32, 10 frames.
        /// </summary>
        /// <remarks>
        /// Tiff file based in the https://github.com/ricaun-io/Autodesk.Icon.Example?tab=readme-ov-file#autodesk-high-resolution-icons
        /// </remarks>
        const string ResourceNameTiff = "Resources/Images/Cube.tiff";

        System.Windows.Media.Imaging.BitmapFrame BitmapFrame;
        [OneTimeSetUp]
        public void Setup()
        {
            BitmapFrame = ResourceNameTiff.GetBitmapSource() as System.Windows.Media.Imaging.BitmapFrame;
        }

        [Test]
        public void GetBitmapSource_NotNull()
        {
            Console.WriteLine(ResourceNameTiff.GetBitmapSource());
            Assert.IsNotNull(ResourceNameTiff.GetBitmapSource());
            Assert.IsNotNull(("/" + ResourceNameTiff).GetBitmapSource());
        }

        [TestCase(32)]
        public void GetBitmapSource_Default_Width(int width)
        {
            var source = ResourceNameTiff.GetBitmapSource();
            Assert.AreEqual(width, Math.Round(source.Width));
            var systemDpi = BitmapExtension.SystemDpi;
            if (systemDpi != Math.Round(source.DpiX))
                Assert.Ignore($"SystemDpi:{systemDpi} != {Math.Round(source.DpiX)}");
        }

        [TestCase(10)]
        public void BitmapFrame_CountFrames(int count)
        {
            Assert.IsNotNull(BitmapFrame);
            var decoder = BitmapFrame.Decoder;
            Assert.AreEqual(count, decoder.Frames.Count);
        }

        [TestCase(16, 96)] // 1.0
        [TestCase(16, 144)] // 1.5
        [TestCase(16, 192)] // 2.0
        [TestCase(16, 288)] // 3.0
        [TestCase(16, 384)] // 4.0
        [TestCase(32, 96)] // 1.0
        [TestCase(32, 144)] // 1.5
        [TestCase(32, 192)] // 2.0
        [TestCase(32, 288)] // 3.0
        [TestCase(32, 384)] // 4.0
        public void BitmapFrame_ByWidthAndDpi(int width, int dpi)
        {
            Assert.IsNotNull(BitmapFrame);
            var decoder = BitmapFrame.Decoder;
            var frame = decoder.GetBitmapFrameByWidthAndDpi(width, dpi) as System.Windows.Media.Imaging.BitmapFrame;
            Assert.IsNotNull(frame);
            Assert.AreEqual(width, Math.Round(frame.Width));
            Assert.AreEqual(dpi, Math.Round(frame.DpiX));
        }

        [TestCase(16, 168, 192)] // 1.75
        [TestCase(32, 168, 192)] // 1.75
        [TestCase(16, 240, 288)] // 2.5
        [TestCase(32, 240, 288)] // 2.5
        [TestCase(16, 336, 384)] // 3.5
        [TestCase(32, 336, 384)] // 3.5
        [TestCase(16, 480, 384)] // 5.0
        [TestCase(32, 480, 384)] // 5.0
        public void BitmapFrame_ByWidthAndDpi_DpiExpected(int width, int dpi, int dpiExpected)
        {
            Assert.IsNotNull(BitmapFrame);
            var decoder = BitmapFrame.Decoder;
            var frame = decoder.GetBitmapFrameByWidthAndDpi(width, dpi) as System.Windows.Media.Imaging.BitmapFrame;
            Assert.IsNotNull(frame);
            Assert.AreEqual(width, Math.Round(frame.Width));
            Assert.AreEqual(dpiExpected, Math.Round(frame.DpiX));
        }

        [TestCase(64, 96, 32)] // 1.0
        [TestCase(64, 144, 32)] // 1.5
        [TestCase(64, 192, 32)] // 2.0
        [TestCase(64, 288, 32)] // 3.0
        [TestCase(64, 384, 32)] // 4.0
        public void BitmapFrame_ByWidthAndDpi_WidthExpected(int width, int dpi, int widthExpected)
        {
            Assert.IsNotNull(BitmapFrame);
            var decoder = BitmapFrame.Decoder;
            var frame = decoder.GetBitmapFrameByWidthAndDpi(width, dpi) as System.Windows.Media.Imaging.BitmapFrame;
            Assert.IsNotNull(frame);
            Assert.AreEqual(widthExpected, Math.Round(frame.Width));
            Assert.AreEqual(dpi, Math.Round(frame.DpiX));
        }
    }
}
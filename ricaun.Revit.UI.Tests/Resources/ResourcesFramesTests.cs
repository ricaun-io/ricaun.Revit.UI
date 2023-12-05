using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests.Resources
{
    public class ResourcesFramesTests
    {
        const string ResourceNameIcoFrames = "Resources/Images/Revit21Frames.ico";

        [Test]
        public void Frames_GetBitmapSource_NotNull()
        {
            Console.WriteLine(ResourceNameIcoFrames.GetBitmapSource());
            Assert.IsNotNull(ResourceNameIcoFrames.GetBitmapSource());
            Assert.IsNotNull(("/" + ResourceNameIcoFrames).GetBitmapSource());
        }

        [TestCase(256)]
        public void Frames_GetBitmapSource_Width(int width)
        {
            var source = ResourceNameIcoFrames.GetBitmapSource();
            Assert.AreEqual(width, source.Width);
        }

        [TestCase(0, 16)]
        [TestCase(10, 10)]
        [TestCase(16, 16)]
        [TestCase(32, 32)]
        [TestCase(64, 64)]
        [TestCase(128, 128)]
        [TestCase(256, 256)]
        [TestCase(512, 256)]
        public void Frames_GetBitmapFrame_Width(int width, int expected)
        {
            var source = ResourceNameIcoFrames.GetBitmapSource().GetBitmapFrame(width);
            Assert.AreEqual(expected, source.Width);
        }
    }
}
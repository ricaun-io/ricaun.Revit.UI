using NUnit.Framework;

namespace ricaun.Revit.UI.Tests
{
    public class AppLoaderTests
    {
        [Test]
        public void HasAppLoaderAttribute()
        {
            var name = typeof(AppLoaderAttribute).Name;
            Assert.AreEqual("AppLoaderAttribute", name);
        }
    }
}
using Autodesk.Revit.UI;
using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests
{
    public class RevitTests
    {
        [Test]
        public void RevitTests_Username(UIApplication uiapp)
        {
            Assert.IsNotNull(uiapp);
            Console.WriteLine(uiapp.Application.Username);
        }

        [Test]
        public void RevitTests_VersionName(UIApplication uiapp)
        {
            Assert.IsNotNull(uiapp);
            Console.WriteLine(uiapp.Application.VersionName);
        }
    }
}
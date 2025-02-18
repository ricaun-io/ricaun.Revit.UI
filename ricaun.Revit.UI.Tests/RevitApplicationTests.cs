using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ricaun.Revit.UI.Tests
{
    public class RevitApplicationTests
    {
        [Test]
        public void RevitApplicationTests_UIApplication()
        {
            var uiapp = RevitApplication.UIApplication;
            Assert.IsNotNull(uiapp);
            Console.WriteLine(uiapp.Application.VersionName);
        }

        [Test]
        public void RevitApplicationTests_UIControlledApplication()
        {
            var application = RevitApplication.UIControlledApplication;
            Assert.IsNotNull(application);
            Console.WriteLine(application.ControlledApplication.VersionName);
        }

        [Test]
        public void RevitApplicationTests_IsInAddInContext()
        {
            var isInAddInContext = RevitApplication.IsInAddInContext;
            Assert.IsTrue(isInAddInContext);
        }

        [Test]
        public void RevitApplicationTests_IsInAddInContext_Task_IsFalse()
        {
            var isInAddInContext = Task.Run(() => RevitApplication.IsInAddInContext).GetAwaiter().GetResult();
            Assert.IsFalse(isInAddInContext);
        }
    }
}
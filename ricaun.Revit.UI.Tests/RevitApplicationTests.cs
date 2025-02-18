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
        public void RevitApplicationTests_IsAddInContext()
        {
            var isAddInContext = RevitApplication.IsAddInContext;
            Assert.IsTrue(isAddInContext);
        }

        [Test]
        public void RevitApplicationTests_IsAddInContext_Task_IsFalse()
        {
            var isAddInContext = Task.Run(() => RevitApplication.IsAddInContext).GetAwaiter().GetResult();
            Assert.IsFalse(isAddInContext);
        }
    }
}
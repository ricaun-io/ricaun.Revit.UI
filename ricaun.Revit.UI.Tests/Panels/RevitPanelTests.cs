using Autodesk.Revit.UI;
using NUnit.Framework;
using ricaun.Revit.UI;
using System.Linq;

namespace ricaun.Revit.UI.Tests.Panels
{
    public class RevitPanelTests
    {
        private const string PanelName = "Example";

        [SetUp]
        public void SetUp(UIControlledApplication application)
        {
            var panel = application.GetRibbonPanels().FirstOrDefault();
            System.Console.WriteLine($"RibbonTab: {panel?.GetRibbonTab().Id}");
        }

        private bool ContainPanel(UIControlledApplication application, RibbonPanel panel)
        {
            return application.GetRibbonPanels().Contains(panel);
        }

        [Test]
        public void CreatePanel(UIControlledApplication application)
        {
            var panel = application.CreatePanel(PanelName);
            foreach (var item in RibbonTabExtension.GetRibbonTabsDictionary(panel.GetRibbonTab()))
            {
                System.Console.WriteLine(item);
            }
            Assert.IsTrue(ContainPanel(application, panel), "Contain Panel");
            panel?.Remove();
            Assert.IsFalse(ContainPanel(application, panel), "Not Contain Panel");
        }

        [Test]
        public void CreatePanel_10Times(UIControlledApplication application)
        {
            for (int i = 0; i < 10; i++)
            {
                var panel = application.CreatePanel(PanelName);
                Assert.IsTrue(ContainPanel(application, panel), "Contain Panel");
                panel?.Remove();
                Assert.IsFalse(ContainPanel(application, panel), "Not Contain Panel");
            }
        }
    }
}
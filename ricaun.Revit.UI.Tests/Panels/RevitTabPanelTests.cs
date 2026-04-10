using Autodesk.Revit.UI;
using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Panels
{
    public class RevitTabPanelTests
    {
        private const string TabName = "RevitTabPanelTests";
        private const string PanelName = "Example";
        private static RibbonPanel ribbonPanel;
        private UIControlledApplication application;

        [SetUp]
        public void SetUp(UIControlledApplication application)
        {
            this.application = application;
            ribbonPanel = application.CreatePanel(TabName, PanelName);
            ribbonPanel.Remove();
            System.Console.WriteLine($"RibbonTab: {ribbonPanel.GetRibbonTab().Id}");
        }

        private bool TabPanelExists(RibbonPanel panel)
        {
            var tabName = panel.GetRibbonTab().Id;
            // GetRibbonPanels thrown when the tab is not exist, so try catch is used to determine whether the tab exist or not.
            try
            {
                return application.GetRibbonPanels(tabName) is not null;
            }
            catch
            {
                return false;
            }
        }

        private bool ContainPanel(RibbonPanel panel)
        {
            var tabName = panel.GetRibbonTab().Id;
            if (!TabPanelExists(panel))
                return false;

            return application.GetRibbonPanels(tabName).Contains(panel);
        }

        [Test]
        public void AfterRemovePanel_RemoveEmptyTab()
        {
            System.Console.WriteLine($"RibbonTab: {ribbonPanel.GetRibbonTab().Id}");
            Assert.IsEmpty(ribbonPanel.GetRibbonTab().Panels);
            Assert.IsFalse(TabPanelExists(ribbonPanel), "Not Exist Tab");
        }

        [Test]
        public void CreatePanel()
        {
            var panel = application.CreatePanel(TabName, PanelName);
            foreach (var item in RibbonTabExtension.GetRibbonTabsDictionary(panel.GetRibbonTab()))
            {
                System.Console.WriteLine(item);
            }
            Assert.IsTrue(ContainPanel(panel), "Contain Panel");
            panel?.Remove();
            Assert.IsFalse(ContainPanel(panel), "Not Contain Panel");
        }

        [Test]
        public void CreatePanel_10Times()
        {
            for (int i = 0; i < 10; i++)
            {
                var panel = application.CreatePanel(TabName, PanelName);
                Assert.IsTrue(ContainPanel(panel), "Contain Panel");
                panel?.Remove();
                Assert.IsFalse(ContainPanel(panel), "Not Contain Panel");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (!string.IsNullOrEmpty(TabName))
                ribbonPanel.GetRibbonTab().Remove();
        }
    }
}
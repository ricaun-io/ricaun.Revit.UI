using Autodesk.Revit.UI;
using NUnit.Framework;
using ricaun.Revit.UI;
using System.Linq;

namespace ricaun.Revit.UI.Tests.Panels
{
    public class RevitPanelTests
    {
        private const string PanelName = "Example";

        UIControlledApplication application;

        [SetUp]
        public void SetUp(UIControlledApplication application)
        {
            this.application = application;
            var panel = application.GetRibbonPanels().FirstOrDefault();
            System.Console.WriteLine($"RibbonTab: {panel?.GetRibbonTab().Id}");
        }

        private bool ContainPanel(RibbonPanel panel)
        {
            return application.GetRibbonPanels().Contains(panel);
        }

        [Test]
        public void CreatePanel()
        {
            var panel = application.CreatePanel(PanelName);
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
                var panel = application.CreatePanel(PanelName);
                Assert.IsTrue(ContainPanel(panel), "Contain Panel");
                panel?.Remove();
                Assert.IsFalse(ContainPanel(panel), "Not Contain Panel");
            }
        }

        [Test]
        public void RemovePanel_DisableFloating()
        {
            var panel = application.CreatePanel(PanelName);
            panel.GetRibbonPanel().IsFloating = true;
            Assert.IsTrue(panel.GetRibbonPanel().IsFloating, "Panel Should be Floating");
            panel?.Remove();
            Assert.IsFalse(panel.GetRibbonPanel().IsFloating, "Panel Should not be Floating");
        }

        [Test]
        public void RemovePanel_ShouldBe_NotEnable()
        {
            var panel = application.CreatePanel(PanelName);
            Assert.IsTrue(panel.Enabled, "Panel Should be Enabled");
            panel.Remove();
            Assert.IsFalse(panel.Enabled, "Panel Should be not Enabled");
        }

        [Test]
        public void RemovePanel_ShouldBe_NotVisible()
        {
            var panel = application.CreatePanel(PanelName);
            Assert.IsTrue(panel.Visible, "Panel Should be Visible");
            panel.Remove();
            Assert.IsFalse(panel.Visible, "Panel Should be not Visible");
        }

        [Test]
        public void RemovePanel_IgnoreIfNull()
        {
            RibbonPanel panel = null;
            Assert.IsNull(panel);
            panel.Remove();
        }
    }
}
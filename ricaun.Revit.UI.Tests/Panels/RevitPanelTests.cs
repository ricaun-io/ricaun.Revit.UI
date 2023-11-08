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

        [Test]
        public void RemovePanel_DisableFloating(UIControlledApplication application)
        {
            var panel = application.CreatePanel(PanelName);
            panel.GetRibbonPanel().IsFloating = true;
            Assert.IsTrue(panel.GetRibbonPanel().IsFloating, "Panel Should be Floating");
            panel?.Remove();
            Assert.IsFalse(panel.GetRibbonPanel().IsFloating, "Panel Should not be Floating");
        }

        [Test]
        public void RemovePanel_IgnoreIfNull()
        {
            RibbonPanel panel = null;
            Assert.IsNull(panel);
            panel.Remove();
        }
    }

    public class RevitPanelQuickAccessTests
    {
        private const string PanelName = "Example";

        private bool HasQuickAccessToolBar(Autodesk.Windows.RibbonItem ribbonItem)
        {
            return Autodesk.Windows.ComponentManager.QuickAccessToolBar.Items
                .FirstOrDefault(e => e.Id == ribbonItem.Id) is not null;
        }

        [Test]
        public void CreatePanel_PushButton_QuickAccess(UIControlledApplication application)
        {
            var panel = application.CreatePanel(PanelName);
            var pushButton = panel.CreatePushButton<BaseCommand>();
            var ribbonItem = pushButton.GetRibbonItem();
            Assert.IsFalse(HasQuickAccessToolBar(ribbonItem), "Should not has QuickAccess after Create");

            try
            {
                pushButton.AddQuickAccessToolBar();
                Assert.IsTrue(HasQuickAccessToolBar(ribbonItem), "Should has QuickAccess after AddQuickAccessToolBar");

                pushButton.RemoveQuickAccessToolBar();
                Assert.IsFalse(HasQuickAccessToolBar(ribbonItem), "Should has not QuickAccess after RemoveQuickAccessToolBar");
            }
            finally
            {
                panel.Remove(true);
            }
        }

        [Test]
        public void RemovePanel_PushButton_QuickAccess(UIControlledApplication application)
        {
            var panel = application.CreatePanel(PanelName);
            var pushButton = panel.CreatePushButton<BaseCommand>();
            var ribbonItem = pushButton.GetRibbonItem();
            Assert.IsFalse(HasQuickAccessToolBar(ribbonItem), "Should not has QuickAccess after Create");
            try
            {
                pushButton.AddQuickAccessToolBar();
                Assert.IsTrue(HasQuickAccessToolBar(ribbonItem), "Should has QuickAccess after AddQuickAccessToolBar");
            }
            finally
            {
                panel.Remove(true);
                Assert.IsFalse(HasQuickAccessToolBar(ribbonItem), "Should not has QuickAccess after Remove(true)");
            }
        }


        [Test]
        public void RemovePanel_QuickAccess_IgnoreIfNull()
        {
            RibbonPanel panel = null;
            Assert.IsNull(panel);
            panel.Remove(true);
        }
    }
}
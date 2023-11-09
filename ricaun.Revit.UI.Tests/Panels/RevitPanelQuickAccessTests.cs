using Autodesk.Revit.UI;
using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Panels
{
    public class RevitPanelQuickAccessTests
    {
        private const string PanelName = "Example";

        [Test]
        public void Internal_HasQuickAccessToolBar_IgnoreNull()
        {
            Autodesk.Windows.RibbonItem ribbonItem = null;
            Assert.IsFalse(ribbonItem.HasQuickAccessToolBar());
        }

        [Test]
        public void Internal_GetQuickAccessToolBar_IgnoreNull()
        {
            Autodesk.Windows.RibbonItem ribbonItem = null;
            Assert.IsNull(ribbonItem.GetQuickAccessToolBar());
        }

        [Test]
        public void PushButton_AddQuickAccessToolBar(UIControlledApplication application)
        {
            var panel = application.CreatePanel(PanelName);
            var pushButton = panel.CreatePushButton<BaseCommand>();
            var ribbonItem = pushButton.GetRibbonItem();
            Assert.IsFalse(ribbonItem.HasQuickAccessToolBar(), "Should not has QuickAccess after Create");
            try
            {
                pushButton.AddQuickAccessToolBar();
                Assert.IsTrue(ribbonItem.HasQuickAccessToolBar(), "Should has QuickAccess after AddQuickAccessToolBar");
            }
            finally
            {
                panel.Remove(true);
                Assert.IsFalse(ribbonItem.HasQuickAccessToolBar(), "Should not has QuickAccess after Remove(true)");
            }
        }

        [Test]
        public void PushButton_AddRemoveQuickAccessToolBar(UIControlledApplication application)
        {
            var panel = application.CreatePanel(PanelName);
            var pushButton = panel.CreatePushButton<BaseCommand>();
            var ribbonItem = pushButton.GetRibbonItem();
            Assert.IsFalse(ribbonItem.HasQuickAccessToolBar(), "Should not has QuickAccess after Create");

            try
            {
                pushButton.AddQuickAccessToolBar();
                Assert.IsTrue(ribbonItem.HasQuickAccessToolBar(), "Should has QuickAccess after AddQuickAccessToolBar");

                pushButton.RemoveQuickAccessToolBar();
                Assert.IsFalse(ribbonItem.HasQuickAccessToolBar(), "Should has not QuickAccess after RemoveQuickAccessToolBar");
            }
            finally
            {
                panel.Remove(true);
            }
        }

        [Test]
        public void RibbonItem_AddRemoveQuickAccessToolBar(UIControlledApplication application)
        {
            var panel = application.CreatePanel(PanelName);
            var pushButton = panel.CreatePushButton<BaseCommand>();
            var ribbonItem = pushButton.GetRibbonItem();
            Assert.IsFalse(ribbonItem.HasQuickAccessToolBar(), "Should not has QuickAccess after Create");

            try
            {
                ribbonItem.AddQuickAccessToolBar();
                Assert.IsTrue(ribbonItem.HasQuickAccessToolBar(), "Should has QuickAccess after AddQuickAccessToolBar");

                ribbonItem.RemoveQuickAccessToolBar();
                Assert.IsFalse(ribbonItem.HasQuickAccessToolBar(), "Should has not QuickAccess after RemoveQuickAccessToolBar");
            }
            finally
            {
                panel.Remove(true);
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
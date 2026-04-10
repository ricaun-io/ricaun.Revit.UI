using Autodesk.Revit.UI;
using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests.Items
{
    public class RibbonPanelAllowInToolBarTests : BaseCreatePanelTests
    {
        [Test]
        public void SetAllowInToolBar()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();

            Assert.IsTrue(pushButton.GetRibbonItem().AllowInToolBar);
            ribbonPanel.SetAllowInToolBar();
            Assert.IsFalse(pushButton.GetRibbonItem().AllowInToolBar);
        }

        [Test]
        public void SetAllowInToolBar_MultipleRibbonItems()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
            var pushButton2 = ribbonPanel.CreatePushButton<BaseCommand>();
            var pulldownButton = ribbonPanel.CreatePulldownButton();

            var ribbonItems = new RibbonItem[] { pushButton, pushButton2, pulldownButton };
            foreach (var ribbonItem in ribbonItems)
                Assert.IsTrue(ribbonItem.GetRibbonItem().AllowInToolBar);

            ribbonPanel.SetAllowInToolBar(false);

            foreach (var ribbonItem in ribbonItems)
                Assert.IsFalse(ribbonItem.GetRibbonItem().AllowInToolBar);

            ribbonPanel.SetAllowInToolBar(true);

            foreach (var ribbonItem in ribbonItems)
                Assert.IsTrue(ribbonItem.GetRibbonItem().AllowInToolBar);
        }
    }
}
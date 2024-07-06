using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Extensions
{
    public class RevitRibbonItemTests : BaseCreatePanelTests
    {
        [Test]
        public void GetRibbonItem_NotNull()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
            var ribbonItem = pushButton.GetRibbonItem();
            Assert.IsNotNull(ribbonItem);
        }

        [Test]
        public void GetRibbonItem_Alternative_NotNull()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
            var ribbonItemAlternative = pushButton.GetRibbonItem_Alternative();
            Assert.IsNotNull(ribbonItemAlternative);
        }

        [Test]
        public void GetRibbonItem_RibbonControl_NotNull()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
            var ribbonItemRibbonControl = pushButton.GetRibbonItem_RibbonControl();
            Assert.IsNotNull(ribbonItemRibbonControl);
        }

        [Test]
        public void GetRibbonItem_RemovePanel()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
            ribbonPanel.Remove();
            var ribbonItem = pushButton.GetRibbonItem();
            Assert.IsNotNull(ribbonItem);
        }

        [Test]
        public void GetRibbonItem_Alternative_RemovePanel()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
            ribbonPanel.Remove();
            var ribbonItemAlternative = pushButton.GetRibbonItem_Alternative();
            Assert.IsNotNull(ribbonItemAlternative);
        }

        [Test]
        public void GetRibbonItem_RibbonControl_RemovePanel()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
            ribbonPanel.Remove();
            var ribbonItemRibbonControl = pushButton.GetRibbonItem_RibbonControl();
            // if the panel is removed, the ribbonItem will be null
            Assert.IsNull(ribbonItemRibbonControl);
        }


        [Test]
        public void GetRibbonItem_EqualTo_Alternative_And_RibbonControl()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
            var ribbonItem = pushButton.GetRibbonItem();
            var ribbonItemAlternative = pushButton.GetRibbonItem_Alternative();
            var ribbonItemRibbonControl = pushButton.GetRibbonItem_RibbonControl();
            Assert.AreEqual(ribbonItem, ribbonItemAlternative);
            Assert.AreEqual(ribbonItemRibbonControl, ribbonItemAlternative);
        }
    }

}
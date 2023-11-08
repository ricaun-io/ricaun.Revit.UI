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
        public void GetRibbonItem_EqualTo_Alternative()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
            var ribbonItem = pushButton.GetRibbonItem();
            var ribbonItemAlternative = pushButton.GetRibbonItem_Alternative();
            Assert.AreEqual(ribbonItem, ribbonItemAlternative);
        }

    }

}
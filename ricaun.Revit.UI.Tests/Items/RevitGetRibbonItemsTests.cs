using Autodesk.Windows;
using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Items
{
    public class RevitGetRibbonItemsTests : BaseCreatePanelTests
    {
        [Test]
        public void GetRibbonItem()
        {
            Assert.IsInstanceOf<RibbonCombo>(ribbonPanel.CreateComboBox().GetRibbonItem());
            Assert.IsInstanceOf<RibbonSplitButton>(ribbonPanel.CreatePulldownButton().GetRibbonItem());
            Assert.IsInstanceOf<RibbonButton>(ribbonPanel.CreatePushButton<BaseCommand>().GetRibbonItem());
            Assert.IsInstanceOf<RibbonRadioButtonGroup>(ribbonPanel.CreateRadioButtonGroup().GetRibbonItem());
            Assert.IsInstanceOf<RibbonSplitButton>(ribbonPanel.CreateSplitButton().GetRibbonItem());
            Assert.IsInstanceOf<RibbonTextBox>(ribbonPanel.CreateTextBox().GetRibbonItem());
        }

        [Test]
        public void GetRibbonItem_IsNull()
        {
            Assert.IsNull(ribbonPanel.CreateComboBox().GetRibbonItem<RibbonButton>());
            Assert.IsNull(ribbonPanel.CreateComboBox().GetRibbonItem<RibbonTextBox>());
        }

        [Test]
        public void GetRibbonItem_IsNotNull()
        {
            Assert.IsNotNull(ribbonPanel.CreateComboBox().GetRibbonItem<RibbonCombo>());
            Assert.IsNotNull(ribbonPanel.CreateComboBox().GetRibbonItem<RibbonItem>());
        }

        [Test]
        public void GetRibbonItem_Action()
        {
            RibbonItem ribbonItem = null;
            ribbonPanel.CreateComboBox().GetRibbonItem<RibbonItem>((ribbon) => { ribbonItem = ribbon; });
            Assert.IsNotNull(ribbonItem);
        }

        [Test]
        public void GetRibbonItem_Action_IsNull()
        {
            RibbonItem ribbonItem = null;
            ribbonPanel.CreateComboBox().GetRibbonItem<RibbonButton>((ribbon) => { ribbonItem = ribbon; });
            Assert.IsNull(ribbonItem);
        }
    }
}
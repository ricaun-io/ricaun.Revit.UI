using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Items
{
    public class RevitCreateItemsTests : BaseCreatePanelTests
    {
        [Test]
        public void CreateComboBox()
        {
            ribbonPanel.CreateComboBox();
        }
        [Test]
        public void CreatePulldownButton()
        {
            ribbonPanel.CreatePulldownButton();
        }
        [Test]
        public void CreatePushButton()
        {
            ribbonPanel.CreatePushButton<BaseCommand>();
        }

        [Test]
        public void CreateRadioButtonGroup()
        {
            ribbonPanel.CreateRadioButtonGroup();
        }

        [Test]
        public void CreateSplitButton()
        {
            ribbonPanel.CreateSplitButton();
        }
        [Test]
        public void CreateTextBox()
        {
            ribbonPanel.CreateTextBox();
        }
    }
}
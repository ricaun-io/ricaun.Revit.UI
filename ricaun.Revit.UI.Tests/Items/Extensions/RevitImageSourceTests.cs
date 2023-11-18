using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Items.Extensions
{
    public class RevitImageSourceTests : BaseCreatePanelTests
    {
        System.Windows.Media.ImageSource imageSource = null;
        const string ComponentImage = "/UIFrameworkRes;component/ribbon/images/revit.ico";

        #region RibbonItem
        [Test]
        public void PushButton_SetImage_Should_BeRemoved()
        {
            var ribbonItem = ribbonPanel.CreatePushButton<BaseCommand>();
            ribbonItem.SetImage(ComponentImage);
            Assert.IsNotNull(ribbonItem.Image);
            ribbonItem.SetImage(imageSource);
            Assert.IsNull(ribbonItem.Image);
        }

        [Test]
        public void ComboBox_SetImage_Should_BeRemoved()
        {
            var ribbonItem = ribbonPanel.CreateComboBox();
            ribbonItem.SetImage(ComponentImage);
            Assert.IsNotNull(ribbonItem.Image);
            ribbonItem.SetImage(imageSource);
            Assert.IsNull(ribbonItem.Image);
        }

        [Test]
        public void TextBox_SetImage_Should_BeRemoved()
        {
            var ribbonItem = ribbonPanel.CreateTextBox();
            ribbonItem.SetImage(ComponentImage);
            Assert.IsNotNull(ribbonItem.Image);
            ribbonItem.SetImage(imageSource);
            Assert.IsNull(ribbonItem.Image);
        }
        #endregion

        #region RibbonItemData
        [Test]
        public void PushButtonData_SetImage_Should_BeRemoved()
        {
            var ribbonItem = ribbonPanel.NewPushButtonData<BaseCommand>();
            ribbonItem.SetImage(ComponentImage);
            Assert.IsNotNull(ribbonItem.Image);
            ribbonItem.SetImage(imageSource);
            Assert.IsNull(ribbonItem.Image);
        }

        [Test]
        public void ComboBoxData_SetImage_Should_BeRemoved()
        {
            var ribbonItem = ribbonPanel.NewComboBoxData();
            ribbonItem.SetImage(ComponentImage);
            Assert.IsNotNull(ribbonItem.Image);
            ribbonItem.SetImage(imageSource);
            Assert.IsNull(ribbonItem.Image);
        }

        [Test]
        public void ComboBoxMemberData_SetImage_Should_BeRemoved()
        {
            var ribbonItem = ribbonPanel.NewComboBoxMemberData();
            ribbonItem.SetImage(ComponentImage);
            Assert.IsNotNull(ribbonItem.Image);
            ribbonItem.SetImage(imageSource);
            Assert.IsNull(ribbonItem.Image);
        }

        [Test]
        public void TextBoxData_SetImage_Should_BeRemoved()
        {
            var ribbonItem = ribbonPanel.NewTextBoxData();
            ribbonItem.SetImage(ComponentImage);
            Assert.IsNotNull(ribbonItem.Image);
            ribbonItem.SetImage(imageSource);
            Assert.IsNull(ribbonItem.Image);
        }
        #endregion
    }
}
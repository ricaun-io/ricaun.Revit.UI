using Autodesk.Revit.UI;
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

    public class RevitCreateItemsImageTests : BaseCreatePanelTests
    {
        PushButton pushButton;

        [SetUp]
        public void CreatePushButton()
        {
            pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
        }

        const string ComponentImage = "/UIFrameworkRes;component/ribbon/images/revit.ico";
        const string Base64Image = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAHYcAAB2HAY/l8WUAAABOSURBVEhLtccxDQAwDMCw8odRdIWxP7cn+fHM3l8913M913M913M913M913M913M913M913M913M913M913M913M913M913M913O9tfcAG98oW3bd33wAAAAASUVORK5CYII=";
        const string UrlImage = "https://www.autodesk.com/favicon.ico";

        [TestCase(ComponentImage)]
        [TestCase(Base64Image)]
        [TestCase(UrlImage)]
        public void SetImage_Should_Be(string image)
        {
            Assert.IsNull(pushButton.Image);
            pushButton.SetImage(image);
            System.Console.WriteLine(pushButton.Image);
            Assert.IsNotNull(pushButton.Image);
        }

        [TestCase()]
        [TestCase("")]
        [TestCase(" ")]
        public void SetImage_Should_BeRemoved(string image)
        {
            pushButton.SetImage(ComponentImage);
            Assert.IsNotNull(pushButton.Image);
            pushButton.SetImage(image);
            Assert.IsNull(pushButton.Image);
        }

        [TestCase(ComponentImage)]
        [TestCase(Base64Image)]
        [TestCase(UrlImage)]
        public void SetLargeImage_Should_Be(string largeImage)
        {
            Assert.IsNull(pushButton.LargeImage);
            pushButton.SetLargeImage(largeImage);
            System.Console.WriteLine(pushButton.LargeImage);
            Assert.IsNotNull(pushButton.Image);
            Assert.IsNotNull(pushButton.LargeImage);
        }

        [TestCase()]
        [TestCase("")]
        [TestCase(" ")]
        public void SetLargeImage_Should_BeRemoved(string largeImage)
        {
            pushButton.SetLargeImage(ComponentImage);
            Assert.IsNotNull(pushButton.Image);
            Assert.IsNotNull(pushButton.LargeImage);
            pushButton.SetLargeImage(largeImage);
            Assert.IsNull(pushButton.Image);
            Assert.IsNull(pushButton.LargeImage);
        }

        [Test]
        public void SetLargeImage_Should_SetImage()
        {
            var image = "";
            var largeImage = ComponentImage;
            pushButton.SetLargeImage(largeImage);
            Assert.IsNotNull(pushButton.Image);
            Assert.IsNotNull(pushButton.LargeImage);

            pushButton.SetImage(image);
            Assert.IsNull(pushButton.Image);
        }

        [Test]
        public void SetLargeImage_Should_NotSetImage()
        {
            var image = Base64Image;
            var largeImage = ComponentImage;
            pushButton.SetImage(image);

            var lastImageSource = pushButton.Image;
            pushButton.SetLargeImage(largeImage);

            Assert.AreEqual(lastImageSource, pushButton.Image);
        }

        [TestCase(ComponentImage)]
        [TestCase(Base64Image)]
        [TestCase(UrlImage)]
        public void SetToolTipImage_Should_Be(string toolTipImage)
        {
            Assert.IsNull(pushButton.ToolTipImage);
            pushButton.SetToolTipImage(toolTipImage);
            System.Console.WriteLine(pushButton.ToolTipImage);
            Assert.IsNotNull(pushButton.ToolTipImage);
        }
    }
}
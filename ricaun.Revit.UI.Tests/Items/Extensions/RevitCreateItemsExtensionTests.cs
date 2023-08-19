using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Items.Extensions
{
    public class RevitCreateItemsExtensionTests : BaseCreatePanelTests
    {
        PushButton pushButton;

        [SetUp]
        public void CreatePushButton()
        {
            pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
        }

        [TestCase("Test")]
        [TestCase("Button")]
        [TestCase("Name")]
        public void SetText_Should_Be(string text)
        {
            pushButton.SetText(text);
            Assert.AreEqual(text, pushButton.ItemText);
        }

        [Test]
        public void SetText_Should_HiddenText()
        {
            pushButton.SetText();
            Assert.IsFalse(pushButton.GetRibbonItem().ShowText);
        }

        [Test]
        public void SetShowText_Should_HiddenText()
        {
            pushButton.SetShowText();
            Assert.IsFalse(pushButton.GetRibbonItem().ShowText);

            pushButton.SetShowText(true);
            Assert.IsTrue(pushButton.GetRibbonItem().ShowText);
        }

        [Test]
        public void SetShowImage_Should_HiddenImage()
        {
            pushButton.SetShowImage();
            Assert.IsFalse(pushButton.GetRibbonItem().ShowImage);

            pushButton.SetShowImage(true);
            Assert.IsTrue(pushButton.GetRibbonItem().ShowImage);
        }

        [TestCase(" ")]
        [TestCase("Tool")]
        [TestCase("ToolTip")]
        public void SetToolTip_Should_Be(string toolTip)
        {
            pushButton.SetToolTip(toolTip);
            Assert.AreEqual(toolTip, pushButton.ToolTip);
        }

        [TestCase(" ")]
        [TestCase("Description")]
        [TestCase("LongDescription")]
        public void SetLongDescription_Should_Be(string description)
        {
            pushButton.SetLongDescription(description);
            Assert.AreEqual(description, pushButton.LongDescription);
        }

        [TestCase("https://www.autodesk.com/")]
        [TestCase("help/file.txt")]
        public void SetContextualHelp_Should_Be(string helpPath)
        {
            pushButton.SetContextualHelp(helpPath);
            Assert.IsNotNull(pushButton.GetContextualHelp());
        }
    }
}
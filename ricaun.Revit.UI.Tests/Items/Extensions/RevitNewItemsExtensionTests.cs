using Autodesk.Revit.UI;
using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Items.Extensions
{
    public class RevitNewItemsExtensionTests : BaseCreatePanelTests
    {
        PushButtonData pushButton;

        [SetUp]
        public void CreatePushButton()
        {
            pushButton = ribbonPanel.NewPushButtonData<BaseCommand>();
        }

        [TestCase("Test")]
        [TestCase("Button")]
        [TestCase("Name")]
        public void SetText_Should_Be(string text)
        {
            pushButton.SetText(text);
            Assert.AreEqual(text, pushButton.Text);
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
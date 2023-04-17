using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Items.Commands
{
    public class RevitCreateItemsCommandSetTests : BaseCreatePanelTests
    {
        [TestCase("Text")]
        [TestCase("Command")]
        public void CreatePushButton_Text(string text)
        {
            var ribbonItem = ribbonPanel.CreatePushButton<BaseCommand>(text);
            Assert.AreEqual(text, ribbonItem.ItemText);
        }

        [TestCase("Text")]
        [TestCase("Command")]
        public void CreatePushButton_SetText(string text)
        {
            var ribbonItem = ribbonPanel.CreatePushButton<BaseCommand>()
                .SetText(text);
            Assert.AreEqual(text, ribbonItem.ItemText);
        }

        [TestCase("Tip")]
        [TestCase("ToolTip")]
        public void CreatePushButton_SetToolTip(string toolTip)
        {
            var ribbonItem = ribbonPanel.CreatePushButton<BaseCommand>()
                .SetToolTip(toolTip);
            Assert.AreEqual(toolTip, ribbonItem.ToolTip);
        }

        [TestCase("Description")]
        [TestCase("LongDescription")]
        public void CreatePushButton_SetLongDescription(string longDescription)
        {
            var ribbonItem = ribbonPanel.CreatePushButton<BaseCommand>()
                .SetLongDescription(longDescription);
            Assert.AreEqual(longDescription, ribbonItem.LongDescription);
        }
    }
}
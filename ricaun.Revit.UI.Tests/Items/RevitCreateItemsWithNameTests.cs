using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Items
{
    public class RevitCreateItemsWithNameTests : BaseCreatePanelTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreateComboBox(string name)
        {
            ribbonPanel.CreateComboBox(name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreatePulldownButton(string name)
        {
            ribbonPanel.CreatePulldownButton(name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreatePushButton(string name)
        {
            ribbonPanel.CreatePushButton<BaseCommand>(name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreateRadioButtonGroup(string name)
        {
            ribbonPanel.CreateRadioButtonGroup(name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreateSplitButton(string name)
        {
            ribbonPanel.CreateSplitButton(name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreateTextBox(string name)
        {
            ribbonPanel.CreateTextBox(name);
        }
    }
}
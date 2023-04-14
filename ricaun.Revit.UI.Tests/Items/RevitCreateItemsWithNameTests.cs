using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Items
{
    public class RevitCreateItemsWithNameTests : BaseCreatePanelTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreateComboBox(string name)
        {
            ribbonPanel.CreateComboBox(name);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreatePulldownButton(string name)
        {
            ribbonPanel.CreatePulldownButton(name);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreatePushButton(string name)
        {
            ribbonPanel.CreatePushButton<BaseCommand>(name);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreateRadioButtonGroup(string name)
        {
            ribbonPanel.CreateRadioButtonGroup(name);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreateSplitButton(string name)
        {
            ribbonPanel.CreateSplitButton(name);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Name")]
        public void CreateTextBox(string name)
        {
            ribbonPanel.CreateTextBox(name);
        }
    }



}
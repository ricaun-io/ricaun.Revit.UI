using NUnit.Framework;
using System;

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
        [TestCase(3)]
        public void CreatePushButton_Repeat(int numberOfCommands)
        {
            for (int i = 0; i < numberOfCommands; i++)
            {
                var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
                Console.WriteLine(pushButton.Name);
            }
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
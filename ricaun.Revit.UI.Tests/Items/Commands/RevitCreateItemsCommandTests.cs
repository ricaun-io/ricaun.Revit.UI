using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests.Items.Commands
{
    public class RevitCreateItemsCommandTests : BaseCreatePanelTests
    {
        [Test]
        public void CreatePushButton()
        {
            var ribbonItem = ribbonPanel.CreatePushButton<BaseCommand>();
            Console.WriteLine(ribbonItem.Name);
            Assert.AreEqual(nameof(BaseCommand), ribbonItem.Name);
            Assert.AreEqual(nameof(BaseCommand), ribbonItem.ItemText);
        }

        [Test]
        public void NewPushButtonData()
        {
            var ribbonItem = ribbonPanel.NewPushButtonData<BaseCommand>();
            Console.WriteLine(ribbonItem.Name);
            Assert.AreEqual(nameof(BaseCommand), ribbonItem.Name);
            Assert.AreEqual(nameof(BaseCommand), ribbonItem.Text);
        }

        [Test]
        public void NewPushButtonData_Type()
        {
            var ribbonItem = ribbonPanel.NewPushButtonData(typeof(BaseCommand));
            Console.WriteLine(ribbonItem.Name);
            Assert.AreEqual(nameof(BaseCommand), ribbonItem.Name);
            Assert.AreEqual(nameof(BaseCommand), ribbonItem.Text);
        }

        [Test]
        public void NewToggleButtonData()
        {
            var ribbonItem = ribbonPanel.NewToggleButtonData<BaseCommand>();
            Console.WriteLine(ribbonItem.Name);
            Assert.AreEqual(nameof(BaseCommand), ribbonItem.Name);
            Assert.AreEqual(nameof(BaseCommand), ribbonItem.Text);
        }
    }
}
using NUnit.Framework;
using System;
using System.Linq;

namespace ricaun.Revit.UI.Tests.Items
{
    public class RevitStackedItemsTests : BaseCreatePanelTests
    {
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(9)]
        [TestCase(20)]
        public void CreateRow(int numberOfCommands)
        {
            var pushButtons = Enumerable.Range(0, numberOfCommands)
                .Select(e => ribbonPanel.CreatePushButton<BaseCommand>())
                .ToArray();

            var ribbonRowPanel = ribbonPanel.RowStackedItems(pushButtons);

            var expectadRowPanels = Math.Ceiling(numberOfCommands / 3.0);

            Assert.AreEqual(expectadRowPanels, ribbonRowPanel.Length);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(9)]
        [TestCase(20)]
        public void CreateFlow(int numberOfCommands)
        {
            var pushButtons = Enumerable.Range(0, numberOfCommands)
                .Select(e => ribbonPanel.CreatePushButton<BaseCommand>())
                .ToArray();

            var ribbonFlowPanel = ribbonPanel.FlowStackedItems(pushButtons);

            Assert.AreEqual(numberOfCommands, ribbonFlowPanel.Items.Count);
        }
    }
}
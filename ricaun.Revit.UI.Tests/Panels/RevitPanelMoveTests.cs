using NUnit.Framework;
using System.Linq;

namespace ricaun.Revit.UI.Tests.Panels
{
    public class RevitPanelMoveTests : BaseCreatePanelTests
    {
        private const string TabIdAddIns = "Add-Ins";
        private const string TabIdModify = "Modify";

        [Test]
        public void CreatePanel_ShouldBe_AddIns()
        {
            Assert.AreEqual(TabIdAddIns, ribbonPanel.GetRibbonTab().Id);
        }

        [Test]
        public void MovePanelTo_Modify()
        {
            ribbonPanel.MoveToRibbonTab();
            Assert.AreEqual(TabIdModify, ribbonPanel.GetRibbonTab().Id);
        }

        [Test]
        public void MovePanelWithId_Modify()
        {
            ribbonPanel.MoveToRibbonTab(TabIdModify);
            Assert.AreEqual(TabIdModify, ribbonPanel.GetRibbonTab().Id);
        }

        [Test]
        public void MovePanelWithId_AddIns()
        {
            ribbonPanel.MoveToRibbonTab(TabIdModify);
            ribbonPanel.MoveToRibbonTab(TabIdAddIns);
            Assert.AreEqual(TabIdAddIns, ribbonPanel.GetRibbonTab().Id);
        }

        [Test]
        public void MovePanelWithTab_First()
        {
            var ribbonTab = RibbonTabExtension.GetRibbonTabs().FirstOrDefault();
            ribbonPanel.MoveToRibbonTab(ribbonTab);
            Assert.AreEqual(ribbonTab, ribbonPanel.GetRibbonTab());
        }

        [Test]
        public void MovePanelWithTab_ForEach()
        {
            foreach (var ribbonTab in RibbonTabExtension.GetRibbonTabs())
            {
                ribbonPanel.MoveToRibbonTab(ribbonTab);
                Assert.AreEqual(ribbonTab, ribbonPanel.GetRibbonTab());
            }
        }
    }
}
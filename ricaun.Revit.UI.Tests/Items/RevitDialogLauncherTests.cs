using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests.Items
{
    public class RevitDialogLauncherTests : BaseCreatePanelTests
    {
        [Test]
        public void Create_DialogLauncher()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
            Assert.IsNull(ribbonPanel.GetRibbonPanel().GetDialogLauncher());

            ribbonPanel.SetDialogLauncher(pushButton);
            Assert.IsNotNull(ribbonPanel.GetRibbonPanel().GetDialogLauncher());
        }

        [Test]
        public void Create_DialogLauncher_Null()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();
            ribbonPanel.SetDialogLauncher(pushButton);

            ribbonPanel.SetDialogLauncher(null);
            Assert.IsNull(ribbonPanel.GetRibbonPanel().GetDialogLauncher());
        }

        [Test]
        public void Create_DialogLauncher_Command()
        {
            var pushButton = ribbonPanel.CreatePushButton<BaseCommand>();

            var ribbonCommandItem = pushButton.GetRibbonItem<Autodesk.Windows.RibbonCommandItem>();

            ribbonPanel.GetRibbonPanel()
                .SetDialogLauncher(ribbonCommandItem);

            Assert.AreEqual(ribbonCommandItem, ribbonPanel.GetRibbonPanel().GetDialogLauncher());
        }
    }
}
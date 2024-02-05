using Autodesk.Revit.UI;
using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Items.Items
{
    public class RevitPulldownButtonTests : BaseCreatePanelTests
    {
        protected PulldownButton pulldownButton;
        protected Autodesk.Windows.RibbonSplitButton RibbonSplitButton => pulldownButton.GetRibbonItem<Autodesk.Windows.RibbonSplitButton>();
        [SetUp]
        public void CreatePulldownButton()
        {
            pulldownButton = ribbonPanel.CreatePulldownButton();
        }

        [Test]
        public void SetListImageSize_ShouldBe_Standard()
        {
            Autodesk.Windows.RibbonImageSize imageSize = Autodesk.Windows.RibbonImageSize.Standard;
            pulldownButton.SetListImageSize();
            Assert.AreEqual(imageSize, RibbonSplitButton.ListImageSize);
        }

        [Test]
        public void SetListImageSize_ShouldBe()
        {
            Autodesk.Windows.RibbonImageSize imageSize;

            imageSize = Autodesk.Windows.RibbonImageSize.Standard;
            pulldownButton.SetListImageSize(imageSize);
            Assert.AreEqual(imageSize, RibbonSplitButton.ListImageSize);

            imageSize = Autodesk.Windows.RibbonImageSize.Large;
            pulldownButton.SetListImageSize(imageSize);
            Assert.AreEqual(imageSize, RibbonSplitButton.ListImageSize);
        }

    }
}
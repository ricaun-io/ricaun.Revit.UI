using Autodesk.Revit.UI;
using NUnit.Framework;
using System.Linq;

namespace ricaun.Revit.UI.Tests
{
    public class BaseCreatePanelTests
    {
        protected RibbonPanel ribbonPanel;
        private const string PanelName = "Example";

        [SetUp]
        public void SetUp(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel(PanelName);
        }

        [TearDown]
        public void TearDown(UIControlledApplication application)
        {
            ribbonPanel?.Remove(true);
        }
    }
}
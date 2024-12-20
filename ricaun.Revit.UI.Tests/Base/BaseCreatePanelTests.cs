﻿using Autodesk.Revit.UI;
using NUnit.Framework;
using System.Linq;

namespace ricaun.Revit.UI.Tests
{
    public class BaseCreatePanelTests
    {
        protected RibbonPanel ribbonPanel;
        protected virtual string PanelName => "Example";

        [SetUp]
        public void SetUp(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel(PanelName);
        }

        [TearDown]
        public void TearDown()
        {
            ribbonPanel?.Remove(true);
        }
    }
}
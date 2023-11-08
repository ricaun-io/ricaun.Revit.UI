using Autodesk.Revit.UI;
using NUnit.Framework;

namespace ricaun.Revit.UI.Tests.Extensions
{
    public class RevitRibbonHelpTests
    {
        [TestCase("http")]
        [TestCase("http://ricaun.com")]
        [TestCase("https://ricaun.com")]
        public void ContextualHelp_ShouldBe_Url(string helpPath)
        {
            var contextualHelp = RibbonHelpExtension.GetContextualHelp(helpPath);
            Assert.AreEqual(ContextualHelpType.Url, contextualHelp.HelpType);
            Assert.AreEqual(helpPath, contextualHelp.HelpPath);
        }

        [TestCase("file")]
        [TestCase("help.pdf")]
        [TestCase("ricaun.com")]
        public void ContextualHelp_ShouldBe_ChmFile(string helpPath)
        {
            var contextualHelp = RibbonHelpExtension.GetContextualHelp(helpPath);
            Assert.AreEqual(ContextualHelpType.ChmFile, contextualHelp.HelpType);
            Assert.AreEqual(helpPath, contextualHelp.HelpPath);
        }
    }

}
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using NUnit.Framework;
using System;

namespace ricaun.Revit.UI.Tests.Items.Items
{
    public class RevitTextBoxTests : BaseCreatePanelTests
    {
        protected TextBox textBox;
        [SetUp]
        public void CreateTextBox()
        {
            textBox = ribbonPanel.CreateTextBox();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetShowImageAsButton_Should_Be(bool show)
        {
            textBox.SetShowImageAsButton(show);
            Assert.AreEqual(show, textBox.ShowImageAsButton);
        }


        [TestCase(true)]
        [TestCase(false)]
        public void SetSelectTextOnFocus_Should_Be(bool focus)
        {
            textBox.SetSelectTextOnFocus(focus);
            Assert.AreEqual(focus, textBox.SelectTextOnFocus);
        }

        [TestCase("text")]
        [TestCase("value")]
        [TestCase("textValue")]
        public void SetValue_Should_Be(string value)
        {
            textBox.SetValue(value);
            Assert.AreEqual(value, textBox.Value);
        }

        [TestCase("text")]
        [TestCase("prompt")]
        [TestCase("promptText")]
        public void SetPromptText_Should_Be(string promptText)
        {
            textBox.SetPromptText(promptText);
            Assert.AreEqual(promptText, textBox.PromptText);
        }

        [TestCase(80)]
        [TestCase(120)]
        [TestCase(150)]
        public void SetWidth_Should_Be(double width)
        {
            textBox.SetWidth(width);
            Assert.AreEqual(width, textBox.GetRibbonItem().Width);
        }

        [Test]
        public void Event_EnterPressed()
        {
            EventHandler<TextBoxEnterPressedEventArgs> eventHandler = (s, e) => { };
            textBox.AddEnterPressed(eventHandler);
            textBox.RemoveEnterPressed(eventHandler);
        }
    }
}
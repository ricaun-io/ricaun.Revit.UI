using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using NUnit.Framework;
using System;
using System.Linq;

namespace ricaun.Revit.UI.Tests.Items.Items
{
    public class RevitComboBoxTests : BaseCreatePanelTests
    {
        protected ComboBox comboBox;
        [SetUp]
        public void CreateComboBox()
        {
            comboBox = ribbonPanel.CreateComboBox();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void AddComboBoxMembers_Should_Be(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var name = i.ToString();
                var data = ribbonPanel.NewComboBoxMemberData(name);
                comboBox.AddComboBoxMembers(data);
            }
            Assert.AreEqual(count, comboBox.GetItems().Count);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void SetCurrent_Should_Be(int current)
        {
            var count = current + 10;
            for (int i = 0; i < count; i++)
            {
                var name = i.ToString();
                var data = ribbonPanel.NewComboBoxMemberData(name);
                comboBox.AddComboBoxMembers(data);
            }
            var first = comboBox.GetItems().Skip(current).FirstOrDefault();
            comboBox.SetCurrent(first);
            Assert.AreEqual(current.ToString(), comboBox.Current.Name);
        }

        [TestCase(80)]
        [TestCase(120)]
        [TestCase(150)]
        public void SetWidth_Should_Be(double width)
        {
            comboBox.SetWidth(width);
            Assert.AreEqual(width, comboBox.GetRibbonItem().Width);
        }

        [Test]
        public void Event_CurrentChanged()
        {
            EventHandler<ComboBoxCurrentChangedEventArgs> eventHandler = (s, e) => { };
            comboBox.AddCurrentChanged(eventHandler);
            comboBox.RemoveCurrentChanged(eventHandler);
        }

        [Test]
        public void Event_DropDownOpened()
        {
            EventHandler<ComboBoxDropDownOpenedEventArgs> eventHandler = (s, e) => { };
            comboBox.AddDropDownOpened(eventHandler);
            comboBox.RemoveDropDownOpened(eventHandler);
        }

        [Test]
        public void Event_DropDownClosed()
        {
            EventHandler<ComboBoxDropDownClosedEventArgs> eventHandler = (s, e) => { };
            comboBox.AddDropDownClosed(eventHandler);
            comboBox.RemoveDropDownClosed(eventHandler);
        }
    }
}
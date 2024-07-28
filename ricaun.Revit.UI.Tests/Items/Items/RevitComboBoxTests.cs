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

        [Test]
        public void SetImage_ShouldBe_Image()
        {
            Assert.IsNull(comboBox.Image);
            comboBox.SetImage(BaseImage.Revit);
            Assert.IsNotNull(comboBox.Image);
        }

        [Test]
        public void SetLargeImage_ShouldBe_Image()
        {
            Assert.IsNull(comboBox.Image);
            comboBox.SetLargeImage(BaseImage.Revit);
            Assert.IsNotNull(comboBox.Image);
        }

        [Test]
        public void ComboBoxMember_SetImage_ShouldBe_Image()
        {
            var data = ribbonPanel.NewComboBoxMemberData();
            Assert.IsNull(data.Image);
            data.SetImage(BaseImage.Revit);
            Assert.IsNotNull(data.Image);
        }

        [Test]
        public void ComboBoxMember_SetLargeImage_ShouldBe_Image()
        {
            var data = ribbonPanel.NewComboBoxMemberData();
            Assert.IsNull(data.Image);
            data.SetLargeImage(BaseImage.Revit);
            Assert.IsNotNull(data.Image);
        }

        [TestCase("Text")]
        [TestCase("Name")]
        public void ComboBoxMember_Create_Name_ShouldBe_ItemText(string name)
        {
            var comboBoxMember = comboBox.CreateComboBoxMember(name);
            Assert.AreEqual(name, comboBoxMember.ItemText);
        }

        [TestCase("Text")]
        [TestCase("Name")]
        public void ComboBoxMember_Create_SetTest_ShouldBe_ItemText(string text)
        {
            var comboBoxMember = comboBox.CreateComboBoxMember();
            comboBoxMember.SetText(text);
            Assert.AreEqual(text, comboBoxMember.ItemText);
        }

        [TestCase("Group")]
        [TestCase("Name")]
        public void ComboBoxMember_Create_GroupName_ShouldBe_GroupName(string groupName)
        {
            var comboBoxMember = comboBox.CreateComboBoxMember(groupName: groupName);
            Assert.AreEqual(groupName, comboBoxMember.GroupName);
        }

        [Test]
        public void ComboBoxMember_Create_SetLargeImage_ShouldBe_Image()
        {
            var comboBoxMember = comboBox.CreateComboBoxMember();
            Assert.IsNull(comboBoxMember.Image);
            comboBoxMember.SetLargeImage(BaseImage.Revit);
            Assert.IsNotNull(comboBoxMember.Image);
        }

        [Test]
        public void ComboBoxMember_Create_SetImage_ShouldBe_Image()
        {
            var comboBoxMember = comboBox.CreateComboBoxMember();
            Assert.IsNull(comboBoxMember.Image);
            comboBoxMember.SetImage(BaseImage.Revit);
            Assert.IsNotNull(comboBoxMember.Image);
        }

        [Test]
        public void ComboBoxMember_Create_ShouldBe_Current()
        {
            var comboBoxMember = comboBox.CreateComboBoxMember();
            Assert.AreEqual(comboBoxMember, comboBox.Current);
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
using System;
using System.Windows.Input;

namespace ricaun.Revit.UI.Example.Models
{
    public class TestModel : NotifyPropertyBase
    {
        public string Text { get; set; } = "Test";
        public string Title { get; set; } = "Title";
        public object Icon { get; set; } = Proprieties.Pack.Revit;
        public ICommand CommandTest { get; set; }
        public ICommand CommandTest2 { get; set; }
    }
}
using System;

namespace ricaun.Revit.UI.Example.Models
{
    public static class TestViewModel
    {
        public static TestModel TestModel = new TestModel();
    }
    public class TestModel : NotifyPropertyBase
    {
        public string Text { get; set; }
    }
}
using System;
using System.Windows;

namespace ricaun.Revit.UI.Example.Views
{
    public partial class TestView : Window
    {
        public TestView()
        {
            InitializeComponent();
            InitializeWindow();
            this.DataContext = Models.TestViewModel.TestModel;
        }

        #region InitializeWindow
        private void InitializeWindow()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new System.Windows.Interop.WindowInteropHelper(this) { Owner = Autodesk.Windows.ComponentManager.ApplicationWindow };
            this.PreviewKeyDown += (s, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Escape) Close();
                if (e.Key == System.Windows.Input.Key.Up) Models.TestViewModel.TestModel.Text += "2";
            };
        }
        #endregion
    }
}
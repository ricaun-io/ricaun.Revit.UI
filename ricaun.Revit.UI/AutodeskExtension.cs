using Autodesk.Windows;
using System.Windows;
using System.Windows.Interop;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// AutodeskExtension
    /// </summary>
    public static class AutodeskExtension
    {
        /// <summary>
        /// Set Autodesk.Windows as the Owner of the Window
        /// </summary>
        /// <param name="window"></param>
        public static void SetAutodeskOwner(this Window window)
        {
            new WindowInteropHelper(window) { Owner = ComponentManager.ApplicationWindow };
        }

        /// <summary>
        /// Get Autodesk.Windows as the Owner Window
        /// </summary>
        /// <returns></returns>
        public static Window GetAutodeskOwner()
        {
            var owner = ComponentManager.ApplicationWindow;
            var source = System.Windows.Interop.HwndSource.FromHwnd(owner);
            return source.RootVisual as Window;
        }
    }
}

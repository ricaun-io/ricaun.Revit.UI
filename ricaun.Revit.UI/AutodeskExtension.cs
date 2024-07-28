using Autodesk.Windows;
using System;
using System.Runtime.InteropServices;
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
        /// Sets the foreground ActiveWindow.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Set Autodesk.Windows as the Owner of the Window and Active Autodesk.Windows when Closed
        /// </summary>
        /// <param name="window"></param>
        public static void SetAutodeskOwner(this Window window)
        {
            new WindowInteropHelper(window) { Owner = ComponentManager.ApplicationWindow };
            window.Closed += (s, e) => { SetForegroundWindow(ComponentManager.ApplicationWindow); };
        }

        /// <summary>
        /// Get Autodesk.Windows as the Owner Window
        /// </summary>
        /// <returns></returns>
#if NET46
        [Obsolete("This funciton does not work with Revit 2018 and 2017, Revit Application is not a Window.")]
#endif
        public static Window GetAutodeskOwner()
        {
            var owner = ComponentManager.ApplicationWindow;
            var source = System.Windows.Interop.HwndSource.FromHwnd(owner);
            return source?.RootVisual as Window;
        }

        /// <summary>
        /// Is Application is Active
        /// </summary>
        public static bool IsApplicationActive => ComponentManager.IsApplicationActive;
    }
}

using Autodesk.Revit.UI;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonDialogLauncherExtension
    /// </summary>
    public static class RibbonDialogLauncherExtension
    {
        /// <summary>
        /// Moves the <paramref name="pushButton"/> to the DialogLauncher of the <paramref name="ribbonPanel"/>
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="pushButton"></param>
        /// <returns></returns>
        public static PushButton SetDialogLauncher(this RibbonPanel ribbonPanel, PushButton pushButton)
        {
            ribbonPanel.GetRibbonPanel().SetDialogLauncher(pushButton?.GetRibbonItem<Autodesk.Windows.RibbonCommandItem>());
            if (pushButton is PushButton) ribbonPanel.Remove(pushButton);
            return pushButton;
        }

        /// <summary>
        /// GetDialogLauncher
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonCommandItem GetDialogLauncher(this Autodesk.Windows.RibbonPanel ribbonPanel)
        {
            return ribbonPanel?.Source?.DialogLauncher;
        }

        /// <summary>
        /// SetDialogLauncher
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="ribbonCommandItem"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel SetDialogLauncher(this Autodesk.Windows.RibbonPanel ribbonPanel, Autodesk.Windows.RibbonCommandItem ribbonCommandItem)
        {
            ribbonPanel.Source.DialogLauncher = ribbonCommandItem;
            return ribbonPanel;
        }
    }
}

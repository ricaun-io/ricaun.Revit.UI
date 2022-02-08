using Autodesk.Revit.UI;
using System.Reflection;
using UIFramework;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonUtilExtension
    /// </summary>
    public static class RibbonUtilExtension
    {
        /// <summary>
        /// GetId from <paramref name="ribbonItem"/>
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        private static string GetId(this RibbonItem ribbonItem)
        {
            var type = typeof(RibbonItem);

            var parentId = type.GetField("m_parentId",
                BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(ribbonItem)
                ?? string.Empty;

            var generateIdMethod = type.GetMethod("generateId",
                BindingFlags.Static | BindingFlags.NonPublic);

            return (string)generateIdMethod?.Invoke(ribbonItem, new[] { parentId, ribbonItem.Name });
        }

        /// <summary>
        /// GetRibbonItem
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonItem GetRibbonItem(this RibbonItem ribbonItem)
        {
            var revitRibbonItem = RevitRibbonControl
                .RibbonControl.findRibbonItemById(ribbonItem.GetId());
            return revitRibbonItem;
        }

        /// <summary>
        /// GetRibbonPanel
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel GetRibbonPanel(this RibbonPanel ribbonPanel)
        {
            var type = typeof(RibbonPanel);
            var _ribbonPanel = type.GetField("m_RibbonPanel",
                BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(ribbonPanel) as Autodesk.Windows.RibbonPanel;
            return _ribbonPanel;
        }
    }
}

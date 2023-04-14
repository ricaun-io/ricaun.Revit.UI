using Autodesk.Revit.UI;
using System;
using System.Reflection;
using UIFramework;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonUtilExtension
    /// </summary>
    public static class RibbonUtilExtension
    {
        #region GetRibbonItem
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
        /// Get <see cref="Autodesk.Windows.RibbonItem"/> from <paramref name="ribbonItem"/>
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
        /// Get <typeparamref name="T"/> from <paramref name="ribbonItem"/> using <see cref="GetRibbonItem"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        public static T GetRibbonItem<T>(this RibbonItem ribbonItem) where T : Autodesk.Windows.RibbonItem
        {
            return ribbonItem.GetRibbonItem() as T;
        }

        /// <summary>
        /// Get <typeparamref name="T"/> from <paramref name="ribbonItem"/> using <see cref="GetRibbonItem"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T GetRibbonItem<T>(this RibbonItem ribbonItem, Action<T> action) where T : Autodesk.Windows.RibbonItem
        {
            var ribbon = ribbonItem.GetRibbonItem<T>();
            if (ribbon is not null) action?.Invoke(ribbon);
            return ribbon;
        }

        /// <summary>
        /// Get <see cref="Autodesk.Windows.RibbonItem"/> from <paramref name="ribbonItem"/>
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        internal static Autodesk.Windows.RibbonItem GetRibbonItem_Alternative(this RibbonItem ribbonItem)
        {
            var ribbonItemType = typeof(RibbonItem);

            var buttonField = ribbonItemType.GetField("m_RibbonItem",
                BindingFlags.Instance | BindingFlags.NonPublic);

            return buttonField?.GetValue(ribbonItem) as Autodesk.Windows.RibbonItem;
        }
        #endregion

        #region GetRibbonPanel
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
        #endregion
    }
}

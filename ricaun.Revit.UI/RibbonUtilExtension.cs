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

        #region CreateCopy
        /// <summary>
        /// Create a copy of the <paramref name="ribbonItemSource"/> with the same type <typeparamref name="TRibbonItem"/>
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItemSource"></param>
        /// <returns></returns>
        public static TRibbonItem CreateCopy<TRibbonItem>(this TRibbonItem ribbonItemSource) where TRibbonItem : Autodesk.Windows.RibbonItem
        {
            var copyRibbonItem = Activator.CreateInstance(ribbonItemSource.GetType()) as TRibbonItem;
            copyRibbonItem.CopyFrom(ribbonItemSource);
            return copyRibbonItem;
        }

        /// <summary>
        /// Create a copy of the <paramref name="ribbonItemSource"/> and convert to type <typeparamref name="TRibbonItem"/>
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItemSource"></param>
        /// <returns></returns>
        public static TRibbonItem CreateCopy<TRibbonItem>(this Autodesk.Windows.RibbonItem ribbonItemSource) where TRibbonItem : Autodesk.Windows.RibbonItem
        {
            var copyRibbonItem = CreateCopy(ribbonItemSource);
            return copyRibbonItem as TRibbonItem;
        }

        /// <summary>
        /// Create a copy of the <paramref name="ribbonItemSource"/> with the same type <typeparamref name="TRibbonItem"/>
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItemSource"></param>
        /// <param name="actionCopy"></param>
        /// <returns></returns>
        public static TRibbonItem CreateCopy<TRibbonItem>(this TRibbonItem ribbonItemSource, Action<TRibbonItem> actionCopy) where TRibbonItem : Autodesk.Windows.RibbonItem
        {
            var copyRibbonItem = CreateCopy(ribbonItemSource);
            actionCopy?.Invoke(copyRibbonItem);
            return copyRibbonItem;
        }

        /// <summary>
        /// Create a copy of the <paramref name="ribbonItemSource"/> and convert to type <typeparamref name="TRibbonItem"/>
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItemSource"></param>
        /// <param name="actionCopy"></param>
        /// <returns></returns>
        public static TRibbonItem CreateCopy<TRibbonItem>(this Autodesk.Windows.RibbonItem ribbonItemSource, Action<TRibbonItem> actionCopy) where TRibbonItem : Autodesk.Windows.RibbonItem
        {
            var copyRibbonItem = CreateCopy<TRibbonItem>(ribbonItemSource);
            actionCopy?.Invoke(copyRibbonItem);
            return copyRibbonItem;
        }
        #endregion
    }
}

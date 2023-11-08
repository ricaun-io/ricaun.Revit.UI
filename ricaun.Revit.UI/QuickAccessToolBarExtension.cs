using Autodesk.Windows;
using System.Linq;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// QuickAccessToolBarExtension
    /// </summary>
    public static class QuickAccessToolBarExtension
    {
        /// <summary>
        /// Remove RibbonItem from QuickAccessToolBar
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        public static TRibbonItem RemoveQuickAccessToolBar<TRibbonItem>(this TRibbonItem ribbonItem) where TRibbonItem : RibbonItem
        {
            if (ribbonItem == null)
                return ribbonItem;

            var ri = ribbonItem.GetQuickAccessToolBar();

            if (ri != null)
                ComponentManager.QuickAccessToolBar.Items.Remove(ri);

            return ribbonItem;
        }

        /// <summary>
        /// Add RibbonItem to QuickAccessToolBar
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        public static TRibbonItem AddQuickAccessToolBar<TRibbonItem>(this TRibbonItem ribbonItem) where TRibbonItem : RibbonItem
        {
            if (ribbonItem == null)
                return ribbonItem;

            var ri = ribbonItem.GetQuickAccessToolBar();

            if (ri == null)
                ComponentManager.QuickAccessToolBar.Items.Add(ribbonItem);

            return ribbonItem;
        }

        /// <summary>
        /// Get RibbonItem from QuickAccessToolBar with same Id
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        internal static RibbonItem GetQuickAccessToolBar(this RibbonItem ribbonItem)
        {
            var ri = ComponentManager.QuickAccessToolBar.Items
                .FirstOrDefault(e => e.Id == ribbonItem?.Id);

            return ri;
        }

        /// <summary>
        /// Get RibbonItem from QuickAccessToolBar with same Id
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        internal static bool HasQuickAccessToolBar(this RibbonItem ribbonItem)
        {
            return ribbonItem.GetQuickAccessToolBar() is not null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Windows;

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

            var ri = ComponentManager.QuickAccessToolBar.Items
                .FirstOrDefault(e => e.Id == ribbonItem.Id);

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

            var ri = ComponentManager.QuickAccessToolBar.Items
                .FirstOrDefault(e => e.Id == ribbonItem.Id);

            if (ri == null)
                ComponentManager.QuickAccessToolBar.Items.Add(ribbonItem);

            return ribbonItem;
        }
    }
}

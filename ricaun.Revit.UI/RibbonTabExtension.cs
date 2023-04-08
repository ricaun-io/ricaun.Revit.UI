using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonTabExtension
    /// </summary>
    public static class RibbonTabExtension
    {
        #region Select
        /// <summary>
        /// GetRibbonTab
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonTab GetRibbonTab(this RibbonPanel ribbonPanel)
        {
            return ribbonPanel.GetRibbonPanel().Tab;
        }

        /// <summary>
        /// GetRibbonTab
        /// </summary>
        /// <param name="ribbonTabId"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonTab GetRibbonTab(string ribbonTabId)
        {
            var ribbon = Autodesk.Windows.ComponentManager.Ribbon;
            return ribbon.FindTab(ribbonTabId);
        }

        /// <summary>
        /// Get GetRibbonTabs
        /// </summary>
        /// <returns></returns>
        public static IList<Autodesk.Windows.RibbonTab> GetRibbonTabs()
        {
            var ribbon = Autodesk.Windows.ComponentManager.Ribbon;
            return ribbon.Tabs;
        }
        #endregion

        #region Remove
        /// <summary>
        /// Remove Tab / Remove Revit Dictionary Name
        /// </summary>
        /// <param name="ribbonTab"></param>
        /// <returns></returns>
        public static bool Remove(this Autodesk.Windows.RibbonTab ribbonTab)
        {
            var ribbon = Autodesk.Windows.ComponentManager.Ribbon;
            GetRibbonTabsDictionary()?.Remove(ribbonTab.Name);
            return ribbon.Tabs.Remove(ribbonTab);
        }
        #endregion

        #region Order

        /// <summary>
        /// Set Panels Order <paramref name="keySelector"/>
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="ribbonTab"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonTab SetPanelsOrderBy<TKey>(this Autodesk.Windows.RibbonTab ribbonTab, Func<Autodesk.Windows.RibbonPanel, TKey> keySelector)
        {
            if (ribbonTab.Panels.Count <= 1)
                return ribbonTab;

            ribbonTab.Panels.OrderBy(keySelector);
            return ribbonTab;
        }

        /// <summary>
        /// Set Panels Order By Title
        /// </summary>
        /// <param name="ribbonTab"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonTab SetPanelsOrderByTitle(this Autodesk.Windows.RibbonTab ribbonTab)
        {
            return ribbonTab.SetPanelsOrderBy(ComparationOrderByTitle);
        }

        /// <summary>
        /// Set Order of Panels by Title
        /// </summary>
        /// <param name="ribbonTab"></param>
        [Obsolete("This method gonna be removed, use SetPanelsOrderByTitle")]
        public static void SetOrderPanels(this Autodesk.Windows.RibbonTab ribbonTab)
        {
            ribbonTab.SetPanelsOrderByTitle();
        }

        private static string ComparationOrderByTitle(Autodesk.Windows.RibbonPanel ribbonPanel)
        {
            return ribbonPanel.Source.Title;
        }
        #endregion

        #region Util
        /// <summary>
        /// GetRibbonTabsDictionary
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, Dictionary<string, RibbonPanel>> GetRibbonTabsDictionary()
        {
            var type = typeof(UIApplication);

            var ribbonItemDictionary = type.GetProperty("RibbonItemDictionary",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                ?.GetValue(null);

            return ribbonItemDictionary as Dictionary<string, Dictionary<string, RibbonPanel>>;
        }
        #endregion
    }
}

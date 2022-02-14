using Autodesk.Revit.UI;
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
        /// <param name="tabId"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonTab GetRibbonTab(string tabId)
        {
            var ribbon = Autodesk.Windows.ComponentManager.Ribbon;
            foreach (Autodesk.Windows.RibbonTab tab in ribbon.Tabs)
            {
                if (tab.Id == tabId)
                {
                    return tab;
                }
            }
            return null;
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
        /// Remove Tab
        /// </summary>
        /// <param name="ribbonTab"></param>
        public static void Remove(this Autodesk.Windows.RibbonTab ribbonTab)
        {
            var ribbon = Autodesk.Windows.ComponentManager.Ribbon;
            ribbon.Tabs.Remove(ribbonTab);
        }
        #endregion

        #region Order
        /// <summary>
        /// MoveRibbonPanel to Position
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="newIndex"></param>
        public static void MoveRibbonPanel(this Autodesk.Windows.RibbonPanel ribbonPanel, int newIndex = 0)
        {
            var ribbonTab = ribbonPanel.Tab;
            var panels = ribbonTab.Panels;
            var length = panels.Count;
            if (newIndex < 0) newIndex = length - 1 + newIndex;
            if (newIndex >= length) newIndex = length - 1;
            for (int i = 0; i < length; i++)
            {
                if (i == newIndex) continue;
                if (panels[i] == ribbonPanel)
                {
                    ribbonTab.Panels.Move(i, newIndex);
                    return;
                }
            }
        }

        /// <summary>
        /// Set Order of Panels by Title
        /// </summary>
        /// <param name="ribbonTab"></param>
        public static void SetOrderPanels(this Autodesk.Windows.RibbonTab ribbonTab)
        {
            var order = ribbonTab.Panels.OrderBy(e => e.Source.Title).ToList();
            var length = order.Count;
            if (length <= 1) return;
            for (int i = 0; i < length; i++)
            {
                var o = order[i];
                for (int j = i; j < length; j++)
                {
                    if (j == i) continue;
                    if (o == ribbonTab.Panels[j])
                    {
                        ribbonTab.Panels.Move(j, i);
                    }
                }
            }
        }

        #endregion
    }
}

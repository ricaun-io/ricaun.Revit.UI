using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UIFramework;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonItemExtension
    /// </summary>
    public static class RibbonItemExtension
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

        /// <summary>
        /// GetRibbonPanel
        /// </summary>
        /// <param name="tabId"></param>
        /// <param name="panelEndWithId"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel GetTabRibbonPanel(string tabId, string panelEndWithId)
        {
            var ribbon = Autodesk.Windows.ComponentManager.Ribbon;
            foreach (Autodesk.Windows.RibbonTab tab in ribbon.Tabs)
            {
                if (tab.Id == tabId)
                {
                    foreach (Autodesk.Windows.RibbonPanel panel in tab.Panels)
                    {
                        if (panel.Source.Id.EndsWith(panelEndWithId))
                        {
                            return panel;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// GetRibbonPanels
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public static IList<Autodesk.Windows.RibbonPanel> GetRibbonPanels(string tabId)
        {
            var ribbon = Autodesk.Windows.ComponentManager.Ribbon;
            var list = new List<Autodesk.Windows.RibbonPanel>();
            foreach (Autodesk.Windows.RibbonTab tab in ribbon.Tabs)
            {
                if (tab.Id == tabId)
                {
                    return tab.Panels;
                }
            }
            return list;
        }

        /// <summary>
        /// GetRibbonItems
        /// </summary>
        /// <param name="tabId"></param>
        /// <param name="panelEndWithId"></param>
        /// <returns></returns>
        public static IList<Autodesk.Windows.RibbonItem> GetRibbonItems(string tabId, string panelEndWithId)
        {
            var ribbon = Autodesk.Windows.ComponentManager.Ribbon;
            var list = new List<Autodesk.Windows.RibbonItem>();
            foreach (Autodesk.Windows.RibbonTab tab in ribbon.Tabs)
            {
                if (tab.Id == tabId)
                {
                    foreach (Autodesk.Windows.RibbonPanel panel in tab.Panels)
                    {
                        if (panel.Source.Id.EndsWith(panelEndWithId))
                        {
                            return panel.Source.Items.ToList();
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// MoveRibbonPanel
        /// </summary>
        /// <param name="tabId"></param>
        /// <param name="panelEndWithId"></param>
        /// <param name="newIndex"></param>
        /// <returns></returns>
        public static bool MoveRibbonPanel(string tabId, string panelEndWithId, int newIndex)
        {
            if (GetRibbonPanels(tabId) is Autodesk.Windows.RibbonTab ribbonTab)
            {
                if (newIndex < 0) newIndex = 0;
                if (newIndex >= ribbonTab.Panels.Count) newIndex = ribbonTab.Panels.Count;
                foreach (var panel in ribbonTab.Panels)
                {
                    if (panel.Source.Id.EndsWith(panelEndWithId))
                    {
                        var index = ribbonTab.Panels.IndexOf(panel);
                        ribbonTab.Panels.Move(index, newIndex);
                        return true;
                    }
                }
            }
            return false;
        }

        #region Autodesk.Windows.RibbonTab

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
    }
}

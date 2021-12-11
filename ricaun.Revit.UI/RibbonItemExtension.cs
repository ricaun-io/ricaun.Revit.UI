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
        public static string GetId(this RibbonItem ribbonItem)
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
        /// GetRibbonPanel
        /// </summary>
        /// <param name="tabId"></param>
        /// <param name="panelEndWithId"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel GetRibbonPanel(string tabId, string panelEndWithId)
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
    }
}

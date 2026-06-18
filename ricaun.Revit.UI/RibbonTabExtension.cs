using Autodesk.Revit.UI;
using System;
using System.Collections;
using System.Collections.Generic;

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
            GetRibbonTabsDictionary()?.Remove(ribbonTab.Id);
            return ribbon.Tabs.Remove(ribbonTab);
        }

        /// <summary>
        /// Remove Tab When Empty / Remove Revit Dictionary Name
        /// </summary>
        /// <param name="ribbonTab"></param>
        internal static void RemoveWhenEmptyAndNotActiveTab(this Autodesk.Windows.RibbonTab ribbonTab)
        {
            if (ribbonTab is null) return;

            if (RibbonControl?.ActiveTab == ribbonTab)
                return;

            if (ribbonTab.Panels.Count == 0)
            {
                ribbonTab.Remove();
            }
        }

        /// <summary>
        /// Remove RibbonPanel / Remove Revit Dictionary Name
        /// </summary>
        /// <param name="ribbonTab"></param>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        public static bool Remove(this Autodesk.Windows.RibbonTab ribbonTab, Autodesk.Windows.RibbonPanel ribbonPanel)
        {
            var removed = ribbonTab.Panels.Remove(ribbonPanel);
            RibbonTabsDictionaryRemove(ribbonTab.Id, ribbonPanel.Source.Name);
            ribbonTab.RemoveWhenEmptyAndNotActiveTab();
            return removed;
        }

        /// <summary>
        /// MoveToRibbonTab
        /// </summary>
        /// <param name="ribbonTab"></param>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        internal static bool MoveToRibbonTab(this Autodesk.Windows.RibbonTab ribbonTab, Autodesk.Windows.RibbonPanel ribbonPanel)
        {
            var removed = ribbonPanel.Tab.Remove(ribbonPanel);
            ribbonTab.Panels.Add(ribbonPanel);
            return removed;
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

        private static string ComparationOrderByTitle(Autodesk.Windows.RibbonPanel ribbonPanel)
        {
            return ribbonPanel.Source.Title;
        }
        #endregion

        #region Util
        /// <summary>
        /// GetRibbonTabsDictionary
        /// </summary>
        /// <param name="ribbonTab"></param>
        /// <returns></returns>
        internal static ICollection GetRibbonTabsDictionary(Autodesk.Windows.RibbonTab ribbonTab)
        {
            return GetRibbonTabsDictionary(ribbonTab.Id);
        }
        /// <summary>
        /// GetRibbonTabsDictionary
        /// </summary>
        /// <param name="ribbonTabId"></param>
        /// <returns></returns>
        internal static ICollection GetRibbonTabsDictionary(string ribbonTabId)
        {
            var ribbonTabsDictionary = GetRibbonTabsDictionary();
            if (ribbonTabsDictionary is null)
                return null;

            if (ribbonTabsDictionary.Contains(ribbonTabId))
                return ribbonTabsDictionary[ribbonTabId] as ICollection;

            return null;
        }
        /// <summary>
        /// GetRibbonTabsDictionary
        /// </summary>
        /// <returns></returns>
        internal static IDictionary GetRibbonTabsDictionary()
        {
            var type = typeof(UIApplication);

            var ribbonItemDictionary = type.GetProperty("RibbonItemDictionary",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                ?.GetValue(null);

            if (ribbonItemDictionary is Dictionary<string, List<RibbonPanel>> revit2028)
                return revit2028;

            return ribbonItemDictionary as Dictionary<string, Dictionary<string, RibbonPanel>>;
        }
        internal static bool RibbonTabsDictionaryRemove(string ribbonTabId, string ribbonPanelName)
        {
            var ribbonTabsDictionary = GetRibbonTabsDictionary(ribbonTabId);
            if (ribbonTabsDictionary is null)
                return false;

            if (ribbonTabsDictionary is IList list) // Revit 2028+
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i] as Autodesk.Revit.UI.RibbonPanel;
                    if (!string.IsNullOrEmpty(item.Name) && string.Equals(item.Name, ribbonPanelName, StringComparison.OrdinalIgnoreCase))
                    {
                        list.RemoveAt(i);
                        return true;
                    }
                }
            }
            else if (ribbonTabsDictionary is IDictionary dictionary)
            {
                dictionary.Remove(ribbonPanelName);
                return true;
            }

            return false;
        }
        #endregion
    }
}

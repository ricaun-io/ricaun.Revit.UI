using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonPanelExtension
    /// </summary>
    public static class RibbonPanelExtension
    {
        #region Panel
        /// <summary>
        /// Create RibbonPanel
        /// </summary>
        /// <param name="application"></param>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static RibbonPanel CreatePanel(this UIControlledApplication application, string panelName)
        {
            RibbonPanel ribbonPanel;
            try
            {
                ribbonPanel = application.CreateRibbonPanel(panelName);
            }
            catch
            {
                ribbonPanel = application.CreateRibbonPanel(RibbonSafeExtension.SafeRibbonPanelName(panelName));
                ribbonPanel.Title = panelName;
            }
            return ribbonPanel;
        }

        /// <summary>
        /// Create or Select RibbonPanel with Name EndsWith <paramref name="panelName"/>
        /// </summary>
        /// <param name="application"></param>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static RibbonPanel CreateOrSelectPanel(this UIControlledApplication application, string panelName)
        {
            if (application.GetRibbonPanels().FirstOrDefault(p => p.IsSelect(panelName)) is RibbonPanel ribbonPanel)
                return ribbonPanel;

            return application.CreatePanel(panelName);
        }

        /// <summary>
        /// Create RibbonPanel
        /// </summary>
        /// <param name="application"></param>
        /// <param name="tabName"></param>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static RibbonPanel CreatePanel(this UIControlledApplication application, string tabName, string panelName)
        {
            if (string.IsNullOrEmpty(tabName))
                return application.CreatePanel(panelName);

            RibbonPanel ribbonPanel = null;
            try { application.CreateRibbonTab(tabName); } catch { }
            try
            {
                ribbonPanel = application.CreateRibbonPanel(tabName, panelName);
            }
            catch
            {
                if (ribbonPanel == null)
                {
                    ribbonPanel = application.CreateRibbonPanel(tabName, RibbonSafeExtension.SafeRibbonPanelName(panelName));
                    ribbonPanel.Title = panelName;
                }
            }
            return ribbonPanel;
        }

        /// <summary>
        /// Create or Select RibbonPanel with Name EndWith <paramref name="panelName"/> on the <paramref name="tabName"/>
        /// </summary>
        /// <param name="application"></param>
        /// <param name="tabName"></param>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static RibbonPanel CreateOrSelectPanel(this UIControlledApplication application, string tabName, string panelName)
        {
            if (string.IsNullOrEmpty(tabName))
                return application.CreateOrSelectPanel(panelName);

            if (application.GetRibbonPanels(tabName).FirstOrDefault(p => p.IsSelect(panelName)) is RibbonPanel ribbonPanel)
                return ribbonPanel;

            return application.CreatePanel(tabName, panelName);
        }

        /// <summary>
        /// Remove RibbonPanel from Tab
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="removeQuickAccessToolBar"></param>
        /// <returns></returns>
        public static RibbonPanel Remove(this RibbonPanel ribbonPanel, bool removeQuickAccessToolBar)
        {
            if (removeQuickAccessToolBar)
                foreach (var ribbonItem in ribbonPanel.GetRibbonItems())
                    ribbonItem.GetRibbonItem().RemoveQuickAccessToolBar();

            return ribbonPanel.Remove();
        }

        /// <summary>
        /// Remove RibbonPanel from Tab
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        public static RibbonPanel Remove(this RibbonPanel ribbonPanel)
        {
            if (ribbonPanel is null)
                return ribbonPanel;

            ribbonPanel.Visible = false;
            ribbonPanel.Enabled = false;

            // Disable Floating
            ribbonPanel.GetRibbonPanel().IsFloating = false;

            ribbonPanel.GetRibbonTab().Remove(ribbonPanel.GetRibbonPanel());
            return ribbonPanel;
        }

        /// <summary>
        /// Move RibbonPanel to RibbonTab with <paramref name="ribbonTabId"/>
        /// <code>'Modify' | 'Add-Ins'</code>
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="ribbonTabId"></param>
        /// <returns></returns>
        public static RibbonPanel MoveToRibbonTab(this RibbonPanel ribbonPanel, string ribbonTabId)
        {
            var ribbonTab = Autodesk.Windows.ComponentManager.Ribbon.FindTab(ribbonTabId);
            return ribbonPanel.MoveToRibbonTab(ribbonTab);
        }

        /// <summary>
        /// Move RibbonPanel to <paramref name="ribbonTab"/>
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="ribbonTab"></param>
        /// <returns></returns>
        public static RibbonPanel MoveToRibbonTab(this RibbonPanel ribbonPanel, Autodesk.Windows.RibbonTab ribbonTab)
        {
            if (ribbonTab is not null)
            {
                var panel = ribbonPanel.GetRibbonPanel();
                panel.Tab.Panels.Remove(panel);
                ribbonTab.Panels.Add(panel);
            }
            return ribbonPanel;
        }
        #endregion

        #region RibbonItems

        /// <summary>
        /// Get Each RibbonItem
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        public static IList<RibbonItem> GetRibbonItems(this RibbonPanel ribbonPanel)
        {
            var ribbonItems = new List<RibbonItem>();

            if (ribbonPanel is null)
                return ribbonItems;

            foreach (var ribbonItem in ribbonPanel.GetItems())
            {
                ribbonItems.Add(ribbonItem);
                if (ribbonItem is PulldownButton pulldownButton)
                    ribbonItems.AddRange(pulldownButton.GetItems());
                if (ribbonItem is SplitButton splitButton)
                    ribbonItems.AddRange(splitButton.GetItems());
                if (ribbonItem is ToggleButton) { }
                if (ribbonItem is RadioButtonGroup radioButtonGroup)
                {
                    ribbonItems.AddRange(radioButtonGroup.GetItems());
                }
                if (ribbonItem is ComboBoxMember) { }
                if (ribbonItem is ComboBox comboBox)
                {
                    ribbonItems.AddRange(comboBox.GetItems());
                }
                if (ribbonItem is TextBox) { }
            }
            return ribbonItems;
        }


        /// <summary>
        /// Remove <paramref name="ribbonItem"/> from <paramref name="ribbonPanel"/>
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        public static RibbonPanel Remove(this RibbonPanel ribbonPanel, RibbonItem ribbonItem)
        {
            var aRibbonItem = ribbonItem.GetRibbonItem();
            var aRibbonPanel = ribbonPanel.GetRibbonPanel();

            aRibbonPanel.Source.Items.Remove(aRibbonItem);

            foreach (var item in aRibbonPanel.Source.Items)
            {
                if (item is Autodesk.Windows.RibbonListButton ribbonListButton)
                    ribbonListButton.Items?.Remove(aRibbonItem);
                if (item is Autodesk.Windows.RibbonRowPanel ribbonRowPanel)
                    ribbonRowPanel.Items?.Remove(aRibbonItem);
            }

            return ribbonPanel;
        }
        #endregion

        #region MoveRibbonPanel
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
            if (length <= 1) return;

            var index = panels.IndexOf(ribbonPanel);
            if (index == -1) return;

            newIndex = Math.Max(0, Math.Min(length - 1, newIndex));
            panels.Move(panels.IndexOf(ribbonPanel), newIndex);
        }
        #endregion

        #region Utils Private
        private static bool IsTabContains(this RibbonPanel ribbonPanel)
        {
            return ribbonPanel.GetRibbonTab().Panels.Contains(ribbonPanel.GetRibbonPanel());
        }

        private static bool IsSelect(this RibbonPanel ribbonPanel, string panelName)
        {
            return ribbonPanel.IsTabContains() && ribbonPanel.Name.EndsWith(panelName);
        }
        #endregion
    }
}

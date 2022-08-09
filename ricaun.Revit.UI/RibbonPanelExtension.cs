﻿using Autodesk.Revit.UI;
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
        public static RibbonPanel Remove(this RibbonPanel ribbonPanel, bool removeQuickAccessToolBar = false)
        {
            if (removeQuickAccessToolBar)
                foreach (var ribbonItem in ribbonPanel.GetRibbonItems())
                    ribbonItem.GetRibbonItem().RemoveQuickAccessToolBar();

            ribbonPanel.Visible = false;
            ribbonPanel.Enabled = false;

            var panel = ribbonPanel.GetRibbonPanel();
            panel.Tab.Panels.Remove(panel);
            return ribbonPanel;
        }

        /// <summary>
        /// Remove RibbonPanel from Tab
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        [Obsolete("Close is deprecated, please use Remove instead.")]
        public static RibbonPanel Close(this RibbonPanel ribbonPanel)
        {
            return ribbonPanel.Remove();
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

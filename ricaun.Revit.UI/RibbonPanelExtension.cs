using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

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
            RibbonPanel ribbonManager;
            try
            {
                ribbonManager = application.CreateRibbonPanel(panelName);
            }
            catch
            {
                ribbonManager = application.CreateRibbonPanel(RibbonSafeExtension.SafeRibbonPanelName(panelName));
                ribbonManager.Title = panelName;
            }
            return ribbonManager;
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
            RibbonPanel ribbonManager = null;
            try { application.CreateRibbonTab(tabName); } catch { }
            try
            {
                ribbonManager = application.CreateRibbonPanel(tabName, panelName);
            }
            catch
            {
                if (ribbonManager == null)
                {
                    ribbonManager = application.CreateRibbonPanel(tabName, RibbonSafeExtension.SafeRibbonPanelName(panelName));
                    ribbonManager.Title = panelName;
                }
            }
            return ribbonManager;
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
    }
}

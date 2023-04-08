using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace ricaun.Revit.UI.Utils
{
    /// <summary>
    /// RibbonModifyUtils
    /// </summary>
    public static class RibbonModifyUtils
    {
        private const string MODIFY_PANEL_ID = "Modify";

        /// <summary>
        /// Create Panel with <paramref name="panelName"/> to Modify Tab. And add the <paramref name="ribbonItems"/> to the Panel.
        /// </summary>
        /// <param name="panelName"></param>
        /// <param name="ribbonItems"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel CreatePanel(string panelName, params RibbonItem[] ribbonItems)
        {
            var panel = CreatePanel(panelName, ribbonItems.Select(e => e.GetRibbonItem()).ToArray());
            return panel;
        }

        /// <summary>
        /// Create Panel with <paramref name="panelName"/> to Modify Tab. And add the <paramref name="ribbonItems"/> to the Panel.
        /// </summary>
        /// <param name="panelName"></param>
        /// <param name="ribbonItems"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel CreatePanel(string panelName, params Autodesk.Windows.RibbonItem[] ribbonItems)
        {
            var panel = CreatePanel(panelName);
            foreach (var item in ribbonItems)
            {
                panel.Source.Items.Add(item);
            }
            return panel;
        }

        /// <summary>
        /// Create Panel with <paramref name="panelName"/> to Modify Tab.
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel CreatePanel(string panelName)
        {
            var modifyTab = Autodesk.Windows.ComponentManager.Ribbon.FindTab(MODIFY_PANEL_ID);
            var modifyPanel = modifyTab.CreatePanel(panelName);
            return modifyPanel;
        }

        /// <summary>
        /// Remove Panel with <paramref name="panelName"/> from Modify Tab.
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel RemovePanel(string panelName)
        {
            var modifyTab = Autodesk.Windows.ComponentManager.Ribbon.FindTab(MODIFY_PANEL_ID);
            var modifyPanel = modifyTab.RemovePanel(panelName);
            return modifyPanel;
        }
    }
}

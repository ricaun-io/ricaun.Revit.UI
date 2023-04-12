using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace ricaun.Revit.UI.Utils
{
    /// <summary>
    /// RibbonModifyUtils
    /// </summary>
    internal static class RibbonModifyUtils
    {
        private const string MODIFY_PANEL_ID = "Modify";

        /// <summary>
        /// Create Panel with <paramref name="panelName"/> to Modify Tab. And add the <paramref name="ribbonItems"/> to the Panel.
        /// </summary>
        /// <param name="panelName"></param>
        /// <param name="ribbonItems"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel CreateRibbonPanel(string panelName, params RibbonItem[] ribbonItems)
        {
            var panel = CreateRibbonPanel(panelName, ribbonItems.Select(e => e.GetRibbonItem()).ToArray());
            return panel;
        }

        /// <summary>
        /// Create Panel with <paramref name="panelName"/> to Modify Tab. And add the <paramref name="ribbonItems"/> to the Panel.
        /// </summary>
        /// <param name="panelName"></param>
        /// <param name="ribbonItems"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel CreateRibbonPanel(string panelName, params Autodesk.Windows.RibbonItem[] ribbonItems)
        {
            var panel = CreateRibbonPanel(panelName);
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
        public static Autodesk.Windows.RibbonPanel CreateRibbonPanel(string panelName)
        {
            var modifyTab = Autodesk.Windows.ComponentManager.Ribbon.FindTab(MODIFY_PANEL_ID);
            var modifyPanel = modifyTab.CreateRibbonPanel(panelName);
            return modifyPanel;
        }

        /// <summary>
        /// Remove Panel with <paramref name="panelName"/> from Modify Tab.
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel RemoveRibbonPanel(string panelName)
        {
            var modifyTab = Autodesk.Windows.ComponentManager.Ribbon.FindTab(MODIFY_PANEL_ID);
            var modifyPanel = modifyTab.RemoveRibbonPanel(panelName);
            return modifyPanel;
        }
    }
}

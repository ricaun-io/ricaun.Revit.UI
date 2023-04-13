using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ricaun.Revit.UI.Utils
{
    /// <summary>
    /// RibbonTabUtils
    /// </summary>
    internal static class RibbonTabUtils
    {
        #region Panel
        /// <summary>
        /// Get Id for the <paramref name="panelName"/> that equals to $"ID_{panelName}". 
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static string GetPanelId(string panelName) => $"ID_{panelName}";

        /// <summary>
        /// Create <see cref="Autodesk.Windows.RibbonPanel"/> and add a with <paramref name="panelName"/> to <paramref name="tab"/>.
        /// <code>RibbonPanel.Id uses <see cref="GetPanelId"/></code>
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel CreateRibbonPanel(this Autodesk.Windows.RibbonTab tab, string panelName)
        {
            var panel = new Autodesk.Windows.RibbonPanel
            {
                Source = new Autodesk.Windows.RibbonPanelSource
                {
                    Id = GetPanelId(panelName),
                    Title = panelName
                }
            };

            tab.Panels.Add(panel);
            return panel;
        }

        /// <summary>
        /// Remove <see cref="Autodesk.Windows.RibbonPanel"/> with <paramref name="panelName"/> from <paramref name="tab"/>
        /// <code>RibbonPanel.Id uses <see cref="GetPanelId"/></code>
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonPanel RemoveRibbonPanel(this Autodesk.Windows.RibbonTab tab, string panelName)
        {
            var panel = tab.FindPanel(GetPanelId(panelName));
            if (panel is not null)
            {
                tab.Panels.Remove(panel);
            }
            return panel;
        }
        #endregion
    }
}

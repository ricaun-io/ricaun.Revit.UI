using Autodesk.Revit.UI;
using System.Collections.Generic;

namespace ricaun.Revit.UI.Utils
{
    internal static class RibbonThemePanelUtils
    {
        private static List<RibbonPanel> RibbonThemePanels = new List<RibbonPanel>();
        static RibbonThemePanelUtils()
        {
            RibbonThemeUtils.ThemeChanged += RibbonThemeUtils_ThemeChanged;
        }
        internal static void Dispose()
        {
            RibbonThemeUtils.ThemeChanged -= RibbonThemeUtils_ThemeChanged;
            RibbonThemePanels.Clear();
        }
        internal static RibbonPanel ThemeChangeDisable(this RibbonPanel ribbonPanel) => ThemeChangeEnable(ribbonPanel, false);
        internal static RibbonPanel ThemeChangeEnable(this RibbonPanel ribbonPanel, bool enable = true)
        {
            if (ribbonPanel is null) return ribbonPanel;

            RibbonThemePanels.Remove(ribbonPanel);
            
            if (enable)
            {
                RibbonThemePanels.Add(ribbonPanel);
            }

            return ribbonPanel;
        }

        private static void RibbonThemeUtils_ThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            foreach (var ribbonPanel in RibbonThemePanels)
            {
                foreach (var ribbonItem in ribbonPanel.GetRibbonItems())
                {
                    var isLight = e.IsLight;
                    UpdateImageThemes(ribbonItem, isLight);
                }
            }
        }

        private static void UpdateImageThemes(RibbonItem ribbonItem, bool isLight)
        {
            try
            {
                switch (ribbonItem)
                {
                    case RibbonButton ribbonButton:
                        var image = ribbonButton.Image;
                        ribbonButton.SetLargeImage(ribbonButton.LargeImage);
                        if (image is not null) ribbonButton.SetImage(image);
                        break;
                    case ComboBox comboBox:
                        comboBox.SetImage(comboBox.Image);
                        break;
                    case ComboBoxMember comboBoxMember:
                        comboBoxMember.SetImage(comboBoxMember.Image);
                        break;
                    case TextBox textBox:
                        textBox.SetImage(textBox.Image);
                        break;
                }
            }
            catch { }
        }
    }
}

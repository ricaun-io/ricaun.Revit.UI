using Autodesk.Revit.UI;

namespace ricaun.Revit.UI;

/// <summary>
/// Provides extension methods for <see cref="RibbonPanel"/> to configure toolbar behavior.
/// </summary>
public static class RibbonPanelAllowInToolBarExtension
{
    /// <summary>
    /// Sets the <c>AllowInToolBar</c> property for all ribbon items in the specified <see cref="RibbonPanel"/>.
    /// </summary>
    /// <param name="ribbonPanel">The ribbon panel whose items will be updated.</param>
    /// <param name="allowInToolBar">If <c>true</c>, allows items to appear in the toolbar; otherwise, disables this behavior. Default is <c>false</c>.</param>
    /// <returns>The original <see cref="RibbonPanel"/> instance for method chaining.</returns>
    public static RibbonPanel SetAllowInToolBar(this RibbonPanel ribbonPanel, bool allowInToolBar = false)
    {
        foreach (var item in ribbonPanel.GetRibbonItems())
        {
            item.GetRibbonItem().AllowInToolBar = allowInToolBar;
        }
        return ribbonPanel;
    }
}
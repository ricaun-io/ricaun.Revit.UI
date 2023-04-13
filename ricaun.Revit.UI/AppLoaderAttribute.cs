using System;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// Console Attribute for ricaun.AppLoader
    /// </summary>
    [Obsolete("This method gonna be removed, use AppLoaderAttribute")]
    public class ConsoleAttribute : Attribute
    {
    }

    /// <summary>
    /// AppLoader Attribute for ricaun.AppLoader
    /// </summary>
    public class AppLoaderAttribute : Attribute
    {
    }
}

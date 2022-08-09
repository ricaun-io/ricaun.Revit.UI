using System.Reflection;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonSaveExtension
    /// </summary>
    internal static class RibbonSafeExtension
    {
        /// <summary>
        /// Safe Button Name
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        internal static string SafeButtonName(string buttonName)
        {
            return $"{buttonName}_{TickNumber}";
        }

        /// <summary>
        /// Safe Ribbon Panel Name
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        internal static string SafeRibbonPanelName(string panelName)
        {
            return $"{System.DateTime.Now.Ticks + TickNumber}%{panelName}";
        }

        /// <summary>
        /// _TickNumber
        /// </summary>
        private static long _TickNumber;
        /// <summary>
        /// TickNumber ++ 
        /// </summary>
        private static long TickNumber => _TickNumber++;

        /// <summary>
        /// Verify if Panel/Item has Name
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static bool VerifyNameExclusive<T>(T ribbonItem, string name)
        {
            var type = typeof(T);
            try
            {
                type.GetMethod("verifyNameExclusive",
                    BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.Invoke(ribbonItem, new[] { name });
                return false;
            }
            catch { }
            return true;
        }
    }
}

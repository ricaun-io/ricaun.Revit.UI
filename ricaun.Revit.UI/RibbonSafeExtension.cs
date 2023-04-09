using Autodesk.Revit.UI;
using System;
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
        internal static bool VerifyNameExclusive<T>(T ribbonItem, string name) where T : class
        {
            //var type = typeof(T);
            var type = ribbonItem.GetType();
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

        /// <summary>
        /// Generate Safe Button Name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="targetName"></param>
        /// <param name="targetText"></param>
        /// <returns></returns>
        internal static string GenerateSafeButtonName<T>(T ribbonItem, string targetName, string targetText) where T : class
        {
            while (RibbonSafeExtension.VerifyNameExclusive(ribbonItem, targetName))
                targetName = RibbonSafeExtension.SafeButtonName(targetText);
            return targetName;
        }

        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="commandType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData(object ribbonItem, Type commandType, string text = null)
        {
            var targetName = commandType.GetName();
            var targetText = targetName;
            var assemblyName = commandType.Assembly.Location;
            var className = commandType.FullName;

            if (text != null && text != "") targetText = text;

            targetName = RibbonSafeExtension.GenerateSafeButtonName(ribbonItem, targetName, targetText);

            PushButtonData buttonData = new PushButtonData(targetName, targetText, assemblyName, className);

            if (typeof(IExternalCommandAvailability).IsAssignableFrom(commandType))
                buttonData.AvailabilityClassName = commandType.FullName;

            if (text == "") buttonData.Text = "-";

            return buttonData;
        }

        public static PushButtonData NewPushButtonData<TExternalCommand>(object ribbonItem, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            var commandType = typeof(TExternalCommand);
            return RibbonSafeExtension.NewPushButtonData(ribbonItem, commandType, text);
        }

        public static PushButtonData NewPushButtonData<TExternalCommand, TAvailability>(object ribbonItem, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            PushButtonData buttonData = RibbonSafeExtension
                .NewPushButtonData<TExternalCommand>(ribbonItem, text)
                .SetAvailability<TAvailability>();
            return buttonData;
        }
    }
}

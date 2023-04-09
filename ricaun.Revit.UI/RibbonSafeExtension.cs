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
        internal static string GenerateSafeButtonName<T>(T ribbonItem, string targetName, string targetText = null) where T : class
        {
            if (targetText == null)
                targetText = targetName;

            while (RibbonSafeExtension.VerifyNameExclusive(ribbonItem, targetName))
                targetName = RibbonSafeExtension.SafeButtonName(targetText);

            return targetName;
        }

        #region NewPushButtonData
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <param name="commandType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        internal static PushButtonData NewPushButtonData(object ribbonItem, Type commandType, string text = null)
        {
            return NewPushButtonData<PushButtonData>(ribbonItem, commandType, text);
        }

        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="commandType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        internal static T NewPushButtonData<T>(object ribbonItem, Type commandType, string text = null) where T : PushButtonData
        {
            var targetName = commandType.GetName();
            var targetText = targetName;
            var assemblyName = commandType.Assembly.Location;
            var className = commandType.FullName;

            if (text != null && text != "") targetText = text;

            targetName = RibbonSafeExtension.GenerateSafeButtonName(ribbonItem, targetName, targetText);

            // var buttonData = new PushButtonData(targetName, targetText, assemblyName, className);
            // var buttonData = new ToggleButtonData(targetName, targetText, assemblyName, className);
            var buttonData = (T)Activator.CreateInstance(typeof(T), targetName, targetText, assemblyName, className);

            if (typeof(IExternalCommandAvailability).IsAssignableFrom(commandType))
                buttonData.AvailabilityClassName = commandType.FullName;

            if (text == "") buttonData.Text = "-";

            return buttonData;
        }

        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        internal static PushButtonData NewPushButtonData<TExternalCommand>(object ribbonItem, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            var commandType = typeof(TExternalCommand);
            return RibbonSafeExtension.NewPushButtonData(ribbonItem, commandType, text);
        }

        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        internal static PushButtonData NewPushButtonData<TExternalCommand, TAvailability>(object ribbonItem, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            PushButtonData buttonData = RibbonSafeExtension
                .NewPushButtonData<TExternalCommand>(ribbonItem, text)
                .SetAvailability<TAvailability>();
            return buttonData;
        }
        #endregion
    }
}

using Autodesk.Revit.UI;
using System;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonButtonExtension
    /// </summary>
    public static class RibbonButtonExtension
    {
        #region PushButtonData
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="commandType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData(this RibbonPanel ribbonPanel, Type commandType, string text = null)
        {
            var targetName = commandType.GetName();
            var targetText = targetName;
            var assemblyName = commandType.Assembly.Location;
            var className = commandType.FullName;

            if (text != null && text != "") targetText = text;

            while (RibbonSafeExtension.VerifyNameExclusive(ribbonPanel, targetName))
                targetName = RibbonSafeExtension.SafeButtonName(targetText);

            PushButtonData currentBtn = new PushButtonData(targetName, targetText, assemblyName, className);

            if (typeof(IExternalCommandAvailability).IsAssignableFrom(commandType))
                currentBtn.AvailabilityClassName = commandType.FullName;

            if (text == "") currentBtn.Text = "-";

            return currentBtn;
        }

        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData<TExternalCommand>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            var commandType = typeof(TExternalCommand);
            return ribbonPanel.NewPushButtonData(commandType, text);
        }
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData<TExternalCommand, TAvailability>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            PushButtonData currentBtn = ribbonPanel.NewPushButtonData<TExternalCommand>(text);
            currentBtn.AvailabilityClassName = typeof(TAvailability).FullName;
            return currentBtn;
        }
        #endregion

        #region AddPushButton
        /// <summary>
        /// AddPushButton
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButton AddPushButton<TExternalCommand>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            PushButton pushButton = ribbonPanel.AddItem(ribbonPanel.NewPushButtonData<TExternalCommand>(text)) as PushButton;
            return pushButton;
        }
        /// <summary>
        /// AddPushButton
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButton AddPushButton<TExternalCommand, TAvailability>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            PushButton pushButton = ribbonPanel.AddPushButton<TExternalCommand>(text);
            pushButton.AvailabilityClassName = typeof(TAvailability).FullName;
            return pushButton;
        }
        #endregion
    }
}

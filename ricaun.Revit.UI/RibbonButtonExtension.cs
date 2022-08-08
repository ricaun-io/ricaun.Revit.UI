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

            PushButtonData buttonData = new PushButtonData(targetName, targetText, assemblyName, className);

            if (typeof(IExternalCommandAvailability).IsAssignableFrom(commandType))
                buttonData.AvailabilityClassName = commandType.FullName;

            if (text == "") buttonData.Text = "-";

            return buttonData;
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
            PushButtonData buttonData = ribbonPanel.NewPushButtonData<TExternalCommand>(text);
            buttonData.AvailabilityClassName = typeof(TAvailability).FullName;
            return buttonData;
        }
        #endregion

        #region CreatePushButton
        /// <summary>
        /// CreatePushButton
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButton CreatePushButton<TExternalCommand>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            PushButton pushButton = ribbonPanel.AddItem(ribbonPanel.NewPushButtonData<TExternalCommand>(text)) as PushButton;
            return pushButton;
        }
        /// <summary>
        /// CreatePushButton
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButton CreatePushButton<TExternalCommand, TAvailability>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            PushButton pushButton = ribbonPanel.CreatePushButton<TExternalCommand>(text);
            pushButton.AvailabilityClassName = typeof(TAvailability).FullName;
            return pushButton;
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
        [Obsolete("AddPushButton is deprecated, please use CreatePushButton instead.")]
        public static PushButton AddPushButton<TExternalCommand>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            return ribbonPanel.CreatePushButton<TExternalCommand>(text);
        }
        /// <summary>
        /// AddPushButton
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [Obsolete("AddPushButton is deprecated, please use CreatePushButton instead.")]
        public static PushButton AddPushButton<TExternalCommand, TAvailability>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            return ribbonPanel.CreatePushButton<TExternalCommand, TAvailability>(text);
        }
        #endregion
    }
}

using Autodesk.Revit.UI;
using System;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonButtonExtension
    /// </summary>
    public static class RibbonButtonExtension
    {
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
            PushButton pushButton = ribbonPanel
                .CreatePushButton<TExternalCommand>(text)
                .SetAvailability<TAvailability>();

            return pushButton;
        }
        #endregion

        #region NewPushButtonData
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="commandType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData(this RibbonPanel ribbonPanel, Type commandType, string text = null)
        {
            return RibbonSafeExtension.NewPushButtonData(ribbonPanel, commandType, text);
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
            return RibbonSafeExtension.NewPushButtonData<TExternalCommand>(ribbonPanel, text);
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
            return RibbonSafeExtension.NewPushButtonData<TExternalCommand, TAvailability>(ribbonPanel, text);
        }
        #endregion

        #region Availability
        /// <summary>
        /// SetAvailability
        /// </summary>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="pushButtonData"></param>
        /// <returns></returns>
        public static PushButtonData SetAvailability<TAvailability>(this PushButtonData pushButtonData) where TAvailability : class, IExternalCommandAvailability, new()
        {
            pushButtonData.AvailabilityClassName = typeof(TAvailability).FullName;
            return pushButtonData;
        }
        /// <summary>
        /// SetAvailability
        /// </summary>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="pushButton"></param>
        /// <returns></returns>
        public static PushButton SetAvailability<TAvailability>(this PushButton pushButton) where TAvailability : class, IExternalCommandAvailability, new()
        {
            pushButton.AvailabilityClassName = typeof(TAvailability).FullName;
            return pushButton;
        }
        #endregion
    }
}

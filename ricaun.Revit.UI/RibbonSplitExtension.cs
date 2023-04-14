using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonSplitExtension
    /// </summary>
    public static class RibbonSplitExtension
    {
        #region SplitButton
        /// <summary>
        /// CreateSplitButton
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="pushButtons"></param>
        /// <returns></returns>
        public static SplitButton CreateSplitButton(this RibbonPanel ribbonPanel, params PushButtonData[] pushButtons)
        {
            return ribbonPanel.CreateSplitButton(null, pushButtons);
        }

        /// <summary>
        /// CreateSplitButton
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetText"></param>
        /// <param name="pushButtons"></param>
        /// <returns></returns>
        public static SplitButton CreateSplitButton(this RibbonPanel ribbonPanel, string targetText, params PushButtonData[] pushButtons)
        {
            SplitButton splitButton = null;

            if (string.IsNullOrWhiteSpace(targetText))
                targetText = pushButtons.FirstOrDefault()?.Text ?? nameof(SplitButton);

            var targetName = targetText;

            targetName = RibbonSafeExtension.GenerateSafeButtonName(ribbonPanel, targetName, targetText);

            splitButton = ribbonPanel.AddItem(new SplitButtonData(targetName, targetText)) as SplitButton;

            splitButton.AddPushButtons(pushButtons);

            return splitButton;
        }
        #endregion

        #region CreatePushButton
        /// <summary>
        /// CreatePushButton
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <param name="splitButton"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButton CreatePushButton<TExternalCommand>(this SplitButton splitButton, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            PushButton pushButton = splitButton.AddPushButton(splitButton.NewPushButtonData<TExternalCommand>(text)) as PushButton;
            return pushButton;
        }
        /// <summary>
        /// CreatePushButton
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="splitButton"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButton CreatePushButton<TExternalCommand, TAvailability>(this SplitButton splitButton, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            PushButton pushButton = splitButton
                .CreatePushButton<TExternalCommand>(text)
                .SetAvailability<TAvailability>();

            return pushButton;
        }
        #endregion

        #region NewPushButtonData
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <param name="splitButton"></param>
        /// <param name="commandType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData(this SplitButton splitButton, Type commandType, string text = null)
        {
            return RibbonSafeExtension.NewPushButtonData(splitButton, commandType, text);
        }
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <param name="splitButton"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData<TExternalCommand>(this SplitButton splitButton, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            return RibbonSafeExtension.NewPushButtonData<TExternalCommand>(splitButton, text);
        }
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="splitButton"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData<TExternalCommand, TAvailability>(this SplitButton splitButton, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            return RibbonSafeExtension.NewPushButtonData<TExternalCommand, TAvailability>(splitButton, text);
        }
        #endregion
    }
}

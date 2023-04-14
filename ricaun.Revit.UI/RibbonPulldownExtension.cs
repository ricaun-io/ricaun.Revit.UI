using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonPulldownExtension
    /// </summary>
    public static class RibbonPulldownExtension
    {
        #region PulldownButton
        /// <summary>
        /// CreatePulldownButton
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="pushButtons"></param>
        /// <returns></returns>
        public static PulldownButton CreatePulldownButton(this RibbonPanel ribbonPanel, params PushButtonData[] pushButtons)
        {
            return ribbonPanel.CreatePulldownButton(null, pushButtons);
        }

        /// <summary>
        /// CreatePulldownButton
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetText"></param>
        /// <param name="pushButtons"></param>
        /// <returns></returns>
        public static PulldownButton CreatePulldownButton(this RibbonPanel ribbonPanel, string targetText, params PushButtonData[] pushButtons)
        {
            PulldownButton pulldownButton = null;

            if (string.IsNullOrWhiteSpace(targetText))
                targetText = pushButtons.FirstOrDefault()?.Text ?? nameof(PulldownButton);

            var targetName = targetText;

            targetName = RibbonSafeExtension.GenerateSafeButtonName(ribbonPanel, targetName, targetText);

            pulldownButton = ribbonPanel.AddItem(new PulldownButtonData(targetName, targetText)) as PulldownButton;

            pulldownButton.AddPushButtons(pushButtons);

            return pulldownButton;
        }

        /// <summary>
        /// AddPushButtons
        /// </summary>
        /// <param name="pulldownButton"></param>
        /// <param name="pushButtons"></param>
        /// <returns></returns>
        public static T AddPushButtons<T>(this T pulldownButton, params PushButtonData[] pushButtons) where T : PulldownButton
        {
            var targetText = pulldownButton.ItemText;
            foreach (PushButtonData pushButton in pushButtons)
            {
                pushButton.Name = RibbonSafeExtension.GenerateSafeButtonName(pulldownButton, pushButton.Name, targetText);

                pulldownButton.AddPushButton(pushButton);
            }
            return pulldownButton;
        }
        #endregion

        #region CreatePushButton
        /// <summary>
        /// CreatePushButton
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <param name="pulldownButton"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButton CreatePushButton<TExternalCommand>(this PulldownButton pulldownButton, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            PushButton pushButton = pulldownButton.AddPushButton(pulldownButton.NewPushButtonData<TExternalCommand>(text)) as PushButton;
            return pushButton;
        }
        /// <summary>
        /// CreatePushButton
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="pulldownButton"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButton CreatePushButton<TExternalCommand, TAvailability>(this PulldownButton pulldownButton, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            PushButton pushButton = pulldownButton
                .CreatePushButton<TExternalCommand>(text)
                .SetAvailability<TAvailability>();

            return pushButton;
        }
        #endregion

        #region NewPushButtonData
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <param name="pulldownButton"></param>
        /// <param name="commandType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData(this PulldownButton pulldownButton, Type commandType, string text = null)
        {
            return RibbonSafeExtension.NewPushButtonData(pulldownButton, commandType, text);
        }
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <param name="pulldownButton"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData<TExternalCommand>(this PulldownButton pulldownButton, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            return RibbonSafeExtension.NewPushButtonData<TExternalCommand>(pulldownButton, text);
        }
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="pulldownButton"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData<TExternalCommand, TAvailability>(this PulldownButton pulldownButton, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            return RibbonSafeExtension.NewPushButtonData<TExternalCommand, TAvailability>(pulldownButton, text);
        }
        #endregion
    }
}

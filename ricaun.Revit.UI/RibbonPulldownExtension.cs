using Autodesk.Revit.UI;
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
        /// <param name="targetPushButtons"></param>
        /// <returns></returns>
        public static PulldownButton CreatePulldownButton(this RibbonPanel ribbonPanel, params PushButtonData[] targetPushButtons)
        {
            return ribbonPanel.CreatePulldownButton(null, targetPushButtons);
        }

        /// <summary>
        /// CreatePulldownButton
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetText"></param>
        /// <param name="targetPushButtons"></param>
        /// <returns></returns>
        public static PulldownButton CreatePulldownButton(this RibbonPanel ribbonPanel, string targetText, params PushButtonData[] targetPushButtons)
        {
            PulldownButton currentPulldownButton = null;
            if (targetPushButtons.Any())
            {
                if (targetText == null) targetText = targetPushButtons.FirstOrDefault().Text;
                var targetName = targetText;

                while (RibbonSafeExtension.VerifyNameExclusive(ribbonPanel, targetName))
                    targetName = RibbonSafeExtension.SafeButtonName(targetText);

                currentPulldownButton = ribbonPanel.AddItem(new PulldownButtonData(targetName, targetText)) as PulldownButton;

                foreach (PushButtonData currentPushButton in targetPushButtons)
                {
                    while (RibbonSafeExtension.VerifyNameExclusive(currentPulldownButton, currentPushButton.Name))
                        currentPushButton.Name = RibbonSafeExtension.SafeButtonName(targetText);

                    currentPulldownButton.AddPushButton(currentPushButton);
                }
            }
            return currentPulldownButton;
        }
        #endregion

    }
}

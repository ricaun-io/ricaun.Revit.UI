using Autodesk.Revit.UI;
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
        /// <param name="targetPushButtons"></param>
        /// <returns></returns>
        public static SplitButton CreateSplitButton(this RibbonPanel ribbonPanel, params PushButtonData[] targetPushButtons)
        {
            return ribbonPanel.CreateSplitButton(null, targetPushButtons);
        }

        /// <summary>
        /// CreateSplitButton
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetText"></param>
        /// <param name="targetPushButtons"></param>
        /// <returns></returns>
        public static SplitButton CreateSplitButton(this RibbonPanel ribbonPanel, string targetText, params PushButtonData[] targetPushButtons)
        {
            SplitButton currentSplitButton = null;
            if (targetPushButtons.Any())
            {
                if (targetText == null) targetText = targetPushButtons.FirstOrDefault().Text;
                var targetName = targetText;

                while (RibbonSafeExtension.VerifyNameExclusive(ribbonPanel, targetName))
                    targetName = RibbonSafeExtension.SafeButtonName(targetText);


                currentSplitButton = ribbonPanel.AddItem(new SplitButtonData(targetName, targetText)) as SplitButton;

                foreach (PushButtonData currentPushButton in targetPushButtons)
                {
                    while (RibbonSafeExtension.VerifyNameExclusive(currentSplitButton, currentPushButton.Name))
                        currentPushButton.Name = RibbonSafeExtension.SafeButtonName(targetText);

                    currentSplitButton.AddPushButton(currentPushButton);
                }
            }

            return currentSplitButton;
        }
        #endregion

    }
}

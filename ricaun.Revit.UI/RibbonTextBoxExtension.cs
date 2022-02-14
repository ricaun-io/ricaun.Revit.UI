using Autodesk.Revit.UI;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonTextBoxExtension
    /// </summary>
    public static class RibbonTextBoxExtension
    {
        /// <summary>
        /// CreateTextBox
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetName"></param>
        /// <returns></returns>
        public static TextBox CreateTextBox(this RibbonPanel ribbonPanel, string targetName)
        {
            return ribbonPanel.AddItem(ribbonPanel.NewTextBoxData(targetName)) as TextBox;
        }

        #region TextBox


        /// <summary>
        /// ShowImageAsButton
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="showImageAsButton"></param>
        /// <returns></returns>
        public static TextBox SetShowImageAsButton(this TextBox textBox, bool showImageAsButton = true)
        {
            textBox.ShowImageAsButton = showImageAsButton;
            return textBox;
        }

        /// <summary>
        /// SetSelectTextOnFocus
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="selectTextOnFocus"></param>
        /// <returns></returns>
        public static TextBox SetSelectTextOnFocus(this TextBox textBox, bool selectTextOnFocus = true)
        {
            textBox.SelectTextOnFocus = selectTextOnFocus;
            return textBox;
        }

        /// <summary>
        /// SetPromptText
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="promptText"></param>
        /// <returns></returns>
        public static TextBox SetPromptText(this TextBox textBox, string promptText)
        {
            textBox.PromptText = promptText;
            return textBox;
        }

        /// <summary>
        /// SetValue
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TextBox SetValue(this TextBox textBox, object value)
        {
            textBox.Value = value;
            return textBox;
        }
        #endregion

        #region TextBoxData
        /// <summary>
        /// NewTextBoxData
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetName"></param>
        /// <returns></returns>
        public static TextBoxData NewTextBoxData(this RibbonPanel ribbonPanel, string targetName)
        {
            while (RibbonSafeExtension.VerifyNameExclusive(ribbonPanel, targetName))
                targetName = RibbonSafeExtension.SafeButtonName(targetName);

            return new TextBoxData(targetName);
        }
        #endregion
    }
}

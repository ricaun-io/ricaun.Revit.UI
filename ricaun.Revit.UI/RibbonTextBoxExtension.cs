using Autodesk.Revit.UI;
using System;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonTextBoxExtension
    /// </summary>
    public static class RibbonTextBoxExtension
    {
        #region TextBox
        /// <summary>
        /// CreateTextBox
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetName"></param>
        /// <returns></returns>
        public static TextBox CreateTextBox(this RibbonPanel ribbonPanel, string targetName = null)
        {
            return ribbonPanel.AddItem(ribbonPanel.NewTextBoxData(targetName)) as TextBox;
        }

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

        /// <summary>
        /// SetWidth
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static TextBox SetWidth(this TextBox textBox, double width)
        {
            textBox.Width = width;
            return textBox;
        }

        /// <summary>
        /// AddEnterPressed
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="enterPressed"></param>
        /// <returns></returns>
        public static TextBox AddEnterPressed(this TextBox textBox, EventHandler<Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs> enterPressed)
        {
            textBox.EnterPressed += enterPressed;
            return textBox;
        }

        /// <summary>
        /// RemoveEnterPressed
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="enterPressed"></param>
        /// <returns></returns>
        public static TextBox RemoveEnterPressed(this TextBox textBox, EventHandler<Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs> enterPressed)
        {
            textBox.EnterPressed -= enterPressed;
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
        public static TextBoxData NewTextBoxData(this RibbonPanel ribbonPanel, string targetName = null)
        {
            if (string.IsNullOrEmpty(targetName)) targetName = nameof(TextBox);
            targetName = RibbonSafeExtension.GenerateSafeButtonName(ribbonPanel, targetName, targetName);

            return new TextBoxData(targetName);
        }
        #endregion
    }
}

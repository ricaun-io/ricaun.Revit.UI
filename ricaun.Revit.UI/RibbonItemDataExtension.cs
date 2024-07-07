using Autodesk.Revit.UI;
using ricaun.Revit.UI.Utils;
using System.Windows.Media;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonItemDataExtension
    /// </summary>
    public static class RibbonItemDataExtension
    {
        #region Set RibbonItemData
        /// <summary>
        /// Sets the contextual help bound with this RibbonItemData.
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItemData</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="helpPath"></param>
        /// <returns></returns>
        public static TRibbonItem SetContextualHelp<TRibbonItem>(this TRibbonItem ribbonItem, string helpPath) where TRibbonItem : RibbonItemData
        {
            if (helpPath != null)
                ribbonItem.SetContextualHelp(RibbonHelpExtension.GetContextualHelp(helpPath));

            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItemData ToolTip
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItemData</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="toolTip"></param>
        /// <returns></returns>
        public static TRibbonItem SetToolTip<TRibbonItem>(this TRibbonItem ribbonItem, string toolTip) where TRibbonItem : RibbonItemData
        {
            if (toolTip != null)
                ribbonItem.ToolTip = toolTip;

            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItemData LongDescription
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItemData</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="longDescription"></param>
        /// <returns></returns>
        public static TRibbonItem SetLongDescription<TRibbonItem>(this TRibbonItem ribbonItem, string longDescription) where TRibbonItem : RibbonItemData
        {
            if (longDescription != null)
                ribbonItem.LongDescription = longDescription;

            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItemData ToolTipImage
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItemData</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="toolTipImage"></param>
        /// <returns></returns>
        public static TRibbonItem SetToolTipImage<TRibbonItem>(this TRibbonItem ribbonItem, ImageSource toolTipImage) where TRibbonItem : RibbonItemData
        {
            ribbonItem.ToolTipImage = toolTipImage;
            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItemData ToolTipImage
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItemData</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="toolTipImage"></param>
        /// <returns></returns>
        public static TRibbonItem SetToolTipImage<TRibbonItem>(this TRibbonItem ribbonItem, string toolTipImage) where TRibbonItem : RibbonItemData
        {
            var bitmapSource = toolTipImage?.GetBitmapSource();

            if (bitmapSource == null && !string.IsNullOrWhiteSpace(toolTipImage))
                return ribbonItem;

            return ribbonItem.SetToolTipImage(bitmapSource);
        }
        #endregion

        #region Set ButtonData
        /// <summary>
        /// Set ButtonData Text
        /// </summary>
        /// <typeparam name="TRibbonItem">ButtonData</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TRibbonItem SetText<TRibbonItem>(this TRibbonItem ribbonItem, string text = "") where TRibbonItem : RibbonItemData
        {
            if (text == null)
                return ribbonItem;

            if (ribbonItem is ButtonData button)
                button.Text = text;

            if (ribbonItem is ComboBoxMemberData comboBoxMemberData)
                comboBoxMemberData.Text = text;

            return ribbonItem;
        }

        /// <summary>
        /// Set ButtonData/ComboBoxData/ComboBoxMemberData/TextBoxData Image
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static TRibbonItem SetImage<TRibbonItem>(this TRibbonItem ribbonItem, string image) where TRibbonItem : RibbonItemData
        {
            var bitmapSource = image?.GetBitmapSource();

            if (bitmapSource == null && !string.IsNullOrWhiteSpace(image))
                return ribbonItem;

            return ribbonItem.SetImage(bitmapSource);
        }

        /// <summary>
        /// Set ButtonData/ComboBoxData/ComboBoxMemberData/TextBoxData LargeImage
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="largeImage"></param>
        /// <returns></returns>
        public static TRibbonItem SetLargeImage<TRibbonItem>(this TRibbonItem ribbonItem, string largeImage) where TRibbonItem : RibbonItemData
        {
            var bitmapSource = largeImage?.GetBitmapSource();

            if (bitmapSource == null && !string.IsNullOrWhiteSpace(largeImage))
                return ribbonItem;

            return ribbonItem.SetLargeImage(bitmapSource);
        }

        /// <summary>
        /// Set ButtonData/ComboBoxData/ComboBoxMemberData/TextBoxData Image
        /// </summary>
        /// <typeparam name="TRibbonItem">ButtonData</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static TRibbonItem SetImage<TRibbonItem>(this TRibbonItem ribbonItem, ImageSource image) where TRibbonItem : RibbonItemData
        {
            image = image.GetThemeImageSource(RibbonThemeUtils.IsLight);

            if (ribbonItem is ButtonData ribbonButton)
                ribbonButton.Image = image?.GetBitmapFrame(16, (frame) => { ribbonButton.Image = frame; });

            else if (ribbonItem is ComboBoxData comboBox)
                comboBox.Image = image?.GetBitmapFrame(16, (frame) => { comboBox.Image = frame; });

            else if (ribbonItem is ComboBoxMemberData comboBoxMemberData)
                comboBoxMemberData.Image = image?.GetBitmapFrame(16, (frame) => { comboBoxMemberData.Image = frame; });

            else if (ribbonItem is TextBoxData textBoxData)
                textBoxData.Image = image?.GetBitmapFrame(16, (frame) => { textBoxData.Image = frame; });

            return ribbonItem;
        }

        /// <summary>
        /// Set ButtonData/ComboBoxData/ComboBoxMemberData/TextBoxData LargeImage
        /// </summary>
        /// <typeparam name="TRibbonItem">ButtonData</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="largeImage"></param>
        /// <returns></returns>
        public static TRibbonItem SetLargeImage<TRibbonItem>(this TRibbonItem ribbonItem, ImageSource largeImage) where TRibbonItem : RibbonItemData
        {
            largeImage = largeImage.GetThemeImageSource(RibbonThemeUtils.IsLight);

            if (ribbonItem is ButtonData ribbonButton)
            {
                ribbonButton.LargeImage = largeImage?.GetBitmapFrame(32, (frame) => { ribbonButton.LargeImage = frame; });
                if (ribbonButton.Image == null || ribbonButton.LargeImage == null || ribbonButton.LargeImage is System.Windows.Media.Imaging.BitmapFrame)
                    ribbonButton.SetImage(ribbonButton.LargeImage);
            }

            else if (ribbonItem is ComboBoxData comboBox)
                comboBox.SetImage(largeImage);

            else if (ribbonItem is ComboBoxMemberData comboBoxMemberData)
                comboBoxMemberData.SetImage(largeImage);

            else if (ribbonItem is TextBoxData textBoxData)
                textBoxData.SetImage(largeImage);

            return ribbonItem;
        }
        #endregion

    }
}

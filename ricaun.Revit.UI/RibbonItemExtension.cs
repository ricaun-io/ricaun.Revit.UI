using Autodesk.Revit.UI;
using ricaun.Revit.UI.Utils;
using System.Windows.Media;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonItemExtension
    /// </summary>
    public static class RibbonItemExtension
    {
        #region Set RibbonItem
        /// <summary>
        /// Sets the contextual help bound with this RibbonItem.
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="helpPath"></param>
        /// <returns></returns>
        public static TRibbonItem SetContextualHelp<TRibbonItem>(this TRibbonItem ribbonItem, string helpPath) where TRibbonItem : RibbonItem
        {
            if (helpPath != null)
                ribbonItem.SetContextualHelp(RibbonHelpExtension.GetContextualHelp(helpPath));

            return ribbonItem;
        }

        /// <summary>
        /// Enable / Disable Show Text
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="showText"></param>
        /// <returns></returns>
        public static TRibbonItem SetShowText<TRibbonItem>(this TRibbonItem ribbonItem, bool showText = false) where TRibbonItem : RibbonItem
        {
            if (ribbonItem.GetRibbonItem() != null)
                ribbonItem.GetRibbonItem().ShowText = showText;
            return ribbonItem;
        }

        /// <summary>
        /// Enable / Disable Show Image
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="showImage"></param>
        /// <returns></returns>
        public static TRibbonItem SetShowImage<TRibbonItem>(this TRibbonItem ribbonItem, bool showImage = false) where TRibbonItem : RibbonItem
        {
            if (ribbonItem.GetRibbonItem() != null)
                ribbonItem.GetRibbonItem().ShowImage = showImage;
            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItemSize
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="itemSize"></param>
        /// <returns></returns>
        public static TRibbonItem SetItemSize<TRibbonItem>(this TRibbonItem ribbonItem, Autodesk.Windows.RibbonItemSize itemSize = Autodesk.Windows.RibbonItemSize.Large) where TRibbonItem : RibbonItem
        {
            if (ribbonItem.GetRibbonItem() != null)
                ribbonItem.GetRibbonItem().Size = itemSize;
            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItem Text
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TRibbonItem SetText<TRibbonItem>(this TRibbonItem ribbonItem, string text = "") where TRibbonItem : RibbonItem
        {
            if (text == null)
                return ribbonItem;

            if (text.Trim() == string.Empty)
                return ribbonItem.SetShowText(false);

            ribbonItem.ItemText = text;

            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItem ToolTip
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="toolTip"></param>
        /// <returns></returns>
        public static TRibbonItem SetToolTip<TRibbonItem>(this TRibbonItem ribbonItem, string toolTip) where TRibbonItem : RibbonItem
        {
            if (toolTip != null)
                ribbonItem.ToolTip = toolTip;

            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItem LongDescription
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="longDescription"></param>
        /// <returns></returns>
        public static TRibbonItem SetLongDescription<TRibbonItem>(this TRibbonItem ribbonItem, string longDescription) where TRibbonItem : RibbonItem
        {
            if (longDescription != null)
                ribbonItem.LongDescription = longDescription;

            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItem ToolTipImage
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="toolTipImage"></param>
        /// <returns></returns>
        public static TRibbonItem SetToolTipImage<TRibbonItem>(this TRibbonItem ribbonItem, ImageSource toolTipImage) where TRibbonItem : RibbonItem
        {
            ribbonItem.ToolTipImage = toolTipImage;
            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItem ToolTipImage
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="toolTipImage"></param>
        /// <returns></returns>
        public static TRibbonItem SetToolTipImage<TRibbonItem>(this TRibbonItem ribbonItem, string toolTipImage) where TRibbonItem : RibbonItem
        {
            var bitmapSource = toolTipImage?.GetBitmapSource();

            if (bitmapSource == null && !string.IsNullOrWhiteSpace(toolTipImage))
                return ribbonItem;

            return ribbonItem.SetToolTipImage(bitmapSource);
        }
        #endregion

        #region Set RibbonButton
        /// <summary>
        /// Set RibbonButton/ComboBox/ComboBoxMember/TextBox Image
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static TRibbonItem SetImage<TRibbonItem>(this TRibbonItem ribbonItem, string image) where TRibbonItem : RibbonItem
        {
            var bitmapSource = image?.GetBitmapSource();

            if (bitmapSource == null && !string.IsNullOrWhiteSpace(image))
                return ribbonItem;

            return ribbonItem.SetImage(bitmapSource);
        }

        /// <summary>
        /// Set RibbonButton/ComboBox/ComboBoxMember/TextBox LargeImage
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="largeImage"></param>
        /// <returns></returns>
        /// <remarks>When <see cref="ComboBox"/>, <see cref="ComboBoxMember"/> or <see cref="TextBox"/> does not have LargeImage, the Image is changed instead.</remarks>
        public static TRibbonItem SetLargeImage<TRibbonItem>(this TRibbonItem ribbonItem, string largeImage) where TRibbonItem : RibbonItem
        {
            var bitmapSource = largeImage?.GetBitmapSource();

            if (bitmapSource == null && !string.IsNullOrWhiteSpace(largeImage))
                return ribbonItem;

            return ribbonItem.SetLargeImage(bitmapSource);
        }

        /// <summary>
        /// Set RibbonButton/ComboBox/ComboBoxMember/TextBox Image
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonButton</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static TRibbonItem SetImage<TRibbonItem>(this TRibbonItem ribbonItem, ImageSource image) where TRibbonItem : RibbonItem
        {
            image = image.GetThemeImageSource(RibbonThemeUtils.IsLight);

            if (ribbonItem is RibbonButton ribbonButton)
                ribbonButton.Image = image?.GetBitmapFrame(16, (frame) => { ribbonButton.Image = frame; });

            else if (ribbonItem is ComboBox comboBox)
                comboBox.Image = image?.GetBitmapFrame(16, (frame) => { comboBox.Image = frame; });

            else if (ribbonItem is ComboBoxMember comboBoxMember)
                comboBoxMember.Image = image?.GetBitmapFrame(16, (frame) => { comboBoxMember.Image = frame; });

            else if (ribbonItem is TextBox textBox)
                textBox.Image = image?.GetBitmapFrame(16, (frame) => { textBox.Image = frame; });

            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonButton/ComboBox/ComboBoxMember/TextBox LargeImage
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonButton</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="largeImage"></param>
        /// <returns></returns>
        /// <remarks>When <see cref="ComboBox"/>, <see cref="ComboBoxMember"/> or <see cref="TextBox"/> does not have LargeImage, the Image is changed instead.</remarks>
        public static TRibbonItem SetLargeImage<TRibbonItem>(this TRibbonItem ribbonItem, ImageSource largeImage) where TRibbonItem : RibbonItem
        {
            largeImage = largeImage.GetThemeImageSource(RibbonThemeUtils.IsLight);

            if (ribbonItem is RibbonButton ribbonButton)
            {
                ribbonButton.LargeImage = largeImage?.GetBitmapFrame(32, (frame) => { ribbonButton.LargeImage = frame; });
                if (ribbonButton.Image == null || ribbonButton.LargeImage == null || ribbonButton.LargeImage is System.Windows.Media.Imaging.BitmapFrame)
                    ribbonButton.SetImage(ribbonButton.LargeImage);
            }

            else if (ribbonItem is ComboBox comboBox)
                comboBox.SetImage(largeImage);

            else if (ribbonItem is ComboBoxMember comboBoxMember)
                comboBoxMember.SetImage(largeImage);

            else if (ribbonItem is TextBox textBox)
                textBox.SetImage(largeImage);

            return ribbonItem;
        }
        #endregion

        #region Set PulldownButton/SplitButton

        /// <summary>
        /// Set ListImageSize in PulldownButton or SplitButton
        /// </summary>
        /// <typeparam name="TPulldownButton">PulldownButton</typeparam>
        /// <param name="pulldownButton"></param>
        /// <param name="listImageSize"></param>
        /// <returns></returns>
        public static TPulldownButton SetListImageSize<TPulldownButton>(
            this TPulldownButton pulldownButton,
            Autodesk.Windows.RibbonImageSize listImageSize = Autodesk.Windows.RibbonImageSize.Standard) where TPulldownButton : PulldownButton
        {
            if (pulldownButton.GetRibbonItem() is Autodesk.Windows.RibbonSplitButton ribbonSplitButton)
                ribbonSplitButton.ListImageSize = listImageSize;

            return pulldownButton;
        }

        #endregion

        #region QuickAccessToolBar
        /// <summary>
        /// Add RibbonItem to QuickAccessToolBar
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        public static TRibbonItem AddQuickAccessToolBar<TRibbonItem>(this TRibbonItem ribbonItem) where TRibbonItem : RibbonItem
        {
            ribbonItem.GetRibbonItem()?.AddQuickAccessToolBar();
            return ribbonItem;
        }

        /// <summary>
        /// Add RibbonItem to QuickAccessToolBar
        /// </summary>
        /// <typeparam name="TRibbonItem"></typeparam>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        public static TRibbonItem RemoveQuickAccessToolBar<TRibbonItem>(this TRibbonItem ribbonItem) where TRibbonItem : RibbonItem
        {
            ribbonItem.GetRibbonItem()?.RemoveQuickAccessToolBar();
            return ribbonItem;
        }
        #endregion
    }
}

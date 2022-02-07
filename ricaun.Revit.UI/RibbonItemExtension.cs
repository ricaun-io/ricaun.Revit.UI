using Autodesk.Revit.UI;
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
            if (toolTipImage != null)
                ribbonItem.ToolTipImage = toolTipImage;

            return ribbonItem;
        }
        #endregion

        #region Set RibbonButton
        /// <summary>
        /// Set RibbonButton Image
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonButton</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static TRibbonItem SetImage<TRibbonItem>(this TRibbonItem ribbonItem, ImageSource image) where TRibbonItem : RibbonItem
        {
            if (image != null)
                if (ribbonItem is RibbonButton ribbonButton)
                    ribbonButton.Image = image;

            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonButton LargeImage
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonButton</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="largeImage"></param>
        /// <returns></returns>
        public static TRibbonItem SetLargeImage<TRibbonItem>(this TRibbonItem ribbonItem, ImageSource largeImage) where TRibbonItem : RibbonItem
        {
            if (largeImage != null)
            {
                if (ribbonItem is RibbonButton ribbonButton)
                {
                    ribbonButton.LargeImage = largeImage.GetBitmapFrame(32);
                    if (ribbonButton.Image == null)
                    {
                        ribbonButton.Image = largeImage.GetBitmapFrame(16, (frame) => { ribbonButton.Image = frame; });
                        if (ribbonButton.Image.Width != 1 && ribbonButton.Image.Width != 16)
                        {
                            ribbonButton.Image = ribbonButton.Image.Scale(16 / ribbonButton.Image.Width);
                        }
                    }
                }
            }

            return ribbonItem;
        }
        #endregion

    }
}

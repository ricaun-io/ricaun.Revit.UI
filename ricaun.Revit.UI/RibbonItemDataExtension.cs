using Autodesk.Revit.UI;
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
        /// Sets the contextual help bound with this RibbonItem.
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
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
        /// Set RibbonItem ToolTip
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
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
        /// Set RibbonItem LongDescription
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
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
        /// Set RibbonItem ToolTipImage
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="toolTipImage"></param>
        /// <returns></returns>
        public static TRibbonItem SetToolTipImage<TRibbonItem>(this TRibbonItem ribbonItem, ImageSource toolTipImage) where TRibbonItem : RibbonItemData
        {
            if (toolTipImage != null)
                ribbonItem.ToolTipImage = toolTipImage;

            return ribbonItem;
        }
        #endregion

        #region Set ButtonData


        /// <summary>
        /// Set RibbonItem Text
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TRibbonItem SetText<TRibbonItem>(this TRibbonItem ribbonItem, string text = "") where TRibbonItem : RibbonItemData
        {
            if (text == null)
                return ribbonItem;

            if (ribbonItem is ButtonData button)
                button.Text = text;

            return ribbonItem;
        }


        /// <summary>
        /// Set RibbonButton Image
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonButton</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static TRibbonItem SetImage<TRibbonItem>(this TRibbonItem ribbonItem, ImageSource image) where TRibbonItem : RibbonItemData
        {
            if (image != null)
                if (ribbonItem is ButtonData ribbonButton)
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
        public static TRibbonItem SetLargeImage<TRibbonItem>(this TRibbonItem ribbonItem, ImageSource largeImage) where TRibbonItem : RibbonItemData
        {
            if (largeImage != null)
            {
                if (ribbonItem is ButtonData ribbonButton)
                {
                    ribbonButton.LargeImage = largeImage.GetBitmapFrame(32);
                    if (ribbonButton.Image == null)
                    {
                        ribbonButton.Image = largeImage.GetBitmapFrame(16);
                        if (ribbonButton.Image.Width != 16)
                            ribbonButton.Image = ribbonButton.Image.Scale(16 / ribbonButton.Image.Width);
                    }
                }
            }

            return ribbonItem;
        }
        #endregion

    }
}

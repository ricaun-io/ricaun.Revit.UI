using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Windows.Media;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonDescription
    /// </summary>
    public class RibbonDescription
    {
        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// ToolTip
        /// </summary>
        public string ToolTip { get; set; }
        /// <summary>
        /// LongDescription
        /// </summary>
        public string LongDescription { get; set; }
        /// <summary>
        /// Help
        /// </summary>
        public string Help { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public ImageSource Image { get; set; }
        /// <summary>
        /// LargeImage
        /// </summary>
        public ImageSource LargeImage { get; set; }
        /// <summary>
        /// ToolTipImage
        /// </summary>
        public ImageSource ToolTipImage { get; set; }
    }

    /// <summary>
    /// RibbonDescriptionExtension
    /// </summary>
    public static class RibbonDescriptionExtension
    {

        /// <summary>
        /// Get Each RibbonItem
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        public static IList<RibbonItem> GetRibbonItems(this RibbonPanel ribbonPanel)
        {
            var ribbonItems = new List<RibbonItem>();
            foreach (var ribbonItem in ribbonPanel.GetItems())
            {
                ribbonItems.Add(ribbonItem);
                if (ribbonItem is PulldownButton pulldownButton)
                    ribbonItems.AddRange(pulldownButton.GetItems());
                if (ribbonItem is SplitButton splitButton)
                    ribbonItems.AddRange(splitButton.GetItems());
                if (ribbonItem is ToggleButton) { }
                if (ribbonItem is RadioButtonGroup radioButtonGroup)
                {
                    ribbonItems.AddRange(radioButtonGroup.GetItems());
                }
                if (ribbonItem is ComboBoxMember) { }
                if (ribbonItem is ComboBox) { }
                if (ribbonItem is TextBox) { }
            }
            return ribbonItems;
        }


        /// <summary>
        /// Set Description
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static RibbonItem SetDescription(this RibbonItem ribbonItem, RibbonDescription description)
        {
            ribbonItem.SetText(description.Text);
            ribbonItem.ToolTip = description.ToolTip;
            ribbonItem.LongDescription = description.LongDescription;
            ribbonItem.ToolTipImage = description.ToolTipImage;
            ribbonItem.SetContextualHelp(GetContextualHelp(description.Help));

            if (ribbonItem is RibbonButton ribbonButton)
            {
                ribbonButton.LargeImage = description.LargeImage;
                ribbonButton.Image = description.Image;
            }
            return ribbonItem;
        }

        #region Set RibbonItem


        /// <summary>
        /// Sets the contextual help bound with this RibbonItem.
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <param name="helpPath"></param>
        /// <returns></returns>
        public static RibbonItem SetContextualHelp(this RibbonItem ribbonItem, string helpPath)
        {
            ribbonItem.SetContextualHelp(GetContextualHelp(helpPath));
            return ribbonItem;
        }

        /// <summary>
        /// Enable / Disable Show Text
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <param name="showText"></param>
        /// <returns></returns>
        public static RibbonItem SetShowText(this RibbonItem ribbonItem, bool showText = false)
        {
            ribbonItem.GetRibbonItem().ShowText = showText;
            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItemSize
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <param name="itemSize"></param>
        /// <returns></returns>
        public static RibbonItem SetItemSize(this RibbonItem ribbonItem, Autodesk.Windows.RibbonItemSize itemSize = Autodesk.Windows.RibbonItemSize.Large)
        {
            ribbonItem.GetRibbonItem().Size = itemSize;
            return ribbonItem;
        }

        /// <summary>
        /// Set RibbonItem Text
        /// </summary>
        /// <param name="ribbonItem"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static RibbonItem SetText(this RibbonItem ribbonItem, string text = null)
        {
            if (text == null || text.Trim() == string.Empty)
                return ribbonItem.SetShowText(false);

            ribbonItem.ItemText = text;
            return ribbonItem;
        }

        #endregion

        #region ContextualHelp
        /// <summary>
        /// Get ContextualHelp by <paramref name="helpPath"/>
        /// </summary>
        /// <param name="helpPath"></param>
        /// <returns></returns>
        private static ContextualHelp GetContextualHelp(string helpPath)
        {
            ContextualHelp contextHelp = null;
            try
            {
                if (helpPath.StartsWith("http"))
                {
                    contextHelp = new ContextualHelp(ContextualHelpType.Url, helpPath);
                }
                else
                {
                    contextHelp = new ContextualHelp(ContextualHelpType.ChmFile, helpPath);
                }
            }
            catch { }
            return contextHelp;
        }
        #endregion
    }
}

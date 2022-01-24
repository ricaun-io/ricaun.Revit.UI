using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ricaun.Revit.UI
{
    public class RibbonSettings
    {
        internal readonly Dictionary<string, List<RibbonDescription>> valuePairs;

        public RibbonSettings()
        {
            valuePairs = new Dictionary<string, List<RibbonDescription>>();
        }

        public void Add<T>(params RibbonDescription[] ribbonDescriptions)
        {
            var name = typeof(T).Name;
            Add(name, ribbonDescriptions);
        }

        public void Add(string name, RibbonDescription ribbonDescription)
        {
            if (valuePairs.TryGetValue(name, out List<RibbonDescription> value))
            {
                value.Add(ribbonDescription);
                return;
            }
            valuePairs[name] = new List<RibbonDescription>();
            valuePairs[name].Add(ribbonDescription);
        }

        public void Add(string name, params RibbonDescription[] ribbonDescriptions)
        {
            foreach (var ribbonDescription in ribbonDescriptions)
                Add(name, ribbonDescription);
        }

        public int Count => valuePairs.Count;
    }

    /// <summary>
    /// RibbonDescription
    /// </summary>
    public class RibbonDescription
    {
        public RibbonDescription(LanguageType LanguageType = LanguageType.Unknown)
        {
            this.LanguageType = LanguageType;
        }

        /// <summary>
        /// LanguageType
        /// </summary>
        public LanguageType LanguageType { get; private set; }
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


        public static RibbonPanel UpdateRibbonDescription(this RibbonPanel ribbonPanel, Action<RibbonSettings> setting)
        {
            RibbonSettings ribbonSettings = new RibbonSettings();
            setting?.Invoke(ribbonSettings);

            var language = LanguageExtension.GetLanguageType();

            foreach (var item in ribbonPanel.GetRibbonItems())
            {
                if (ribbonSettings.valuePairs.TryGetValue(item.Name, out List<RibbonDescription> value))
                {
                    item.SetDescription(value.First());
                    item.SetDescription(value.FirstOrDefault(v => v.LanguageType == language));
                }
            }

            return ribbonPanel;
        }

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
            if (description == null)
                return ribbonItem;

            ribbonItem.SetText(description.Text);

            if (description.ToolTip != null)
                ribbonItem.ToolTip = description.ToolTip;

            if (description.LongDescription != null)
                ribbonItem.LongDescription = description.LongDescription;

            if (description.ToolTipImage != null)
                ribbonItem.ToolTipImage = description.ToolTipImage;

            ribbonItem.SetContextualHelp(description.Help);

            if (ribbonItem is RibbonButton ribbonButton)
            {
                if (description.LargeImage != null)
                    ribbonButton.LargeImage = description.LargeImage;

                if (description.Image != null)
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
            if (helpPath != null)
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
        public static RibbonItem SetText(this RibbonItem ribbonItem, string text = "")
        {
            if (text == null)
                return ribbonItem;

            if (text.Trim() == string.Empty)
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

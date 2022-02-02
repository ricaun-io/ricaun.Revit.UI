﻿using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonSettings
    /// </summary>
    public class RibbonSettings
    {
        private readonly Dictionary<string, List<RibbonDescription>> nameRibbonDescriptions;

        /// <summary>
        /// RibbonSettings
        /// </summary>
        public RibbonSettings()
        {
            nameRibbonDescriptions = new Dictionary<string, List<RibbonDescription>>();
        }

        /// <summary>
        /// Get Value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ribbonDescriptions"></param>
        /// <returns></returns>
        public bool TryGetValue(string name, out List<RibbonDescription> ribbonDescriptions)
        {
            return nameRibbonDescriptions.TryGetValue(name, out ribbonDescriptions);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ribbonDescription"></param>
        public void Add(string name, RibbonDescription ribbonDescription)
        {
            if (nameRibbonDescriptions.TryGetValue(name, out List<RibbonDescription> value))
            {
                value.Add(ribbonDescription);
                return;
            }
            nameRibbonDescriptions[name] = new List<RibbonDescription>();
            nameRibbonDescriptions[name].Add(ribbonDescription);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ribbonDescriptions"></param>
        public void Add(string name, params RibbonDescription[] ribbonDescriptions)
        {
            foreach (var ribbonDescription in ribbonDescriptions)
                Add(name, ribbonDescription);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ribbonDescriptions"></param>
        public void Add<T>(params RibbonDescription[] ribbonDescriptions)
        {
            var name = typeof(T).GetName();
            Add(name, ribbonDescriptions);
        }

        /// <summary>
        /// Add Default
        /// </summary>
        /// <param name="ribbonDescriptions"></param>
        public void AddDefault(params RibbonDescription[] ribbonDescriptions)
        {
            foreach (var ribbonDescription in ribbonDescriptions)
                Add("", ribbonDescription);
        }

        /// <summary>
        /// Get Count
        /// </summary>
        public int Count => nameRibbonDescriptions.Count;
    }

    /// <summary>
    /// RibbonDescription
    /// </summary>
    public class RibbonDescription
    {
        /// <summary>
        /// RibbonDescription with LanguageType
        /// </summary>
        /// <param name="LanguageType"></param>
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
        /// <summary>
        /// Action
        /// </summary>
        public Action<RibbonItem> Action { get; set; }
    }

    /// <summary>
    /// RibbonDescriptionExtension
    /// </summary>
    public static class RibbonDescriptionExtension
    {
        /// <summary>
        /// Update RibbonPanel RibbonSettings
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static RibbonPanel UpdateRibbonDescription(this RibbonPanel ribbonPanel, Action<RibbonSettings> setting)
        {
            RibbonSettings ribbonSettings = new RibbonSettings();
            setting?.Invoke(ribbonSettings);

            var language = LanguageExtension.GetLanguageType();

            if (ribbonSettings.TryGetValue("", out List<RibbonDescription> valueDefault))
            {
                foreach (var item in ribbonPanel.GetRibbonItems())
                {
                    item.UpdateRibbonDescription(valueDefault.First());
                    item.UpdateRibbonDescription(valueDefault.FirstOrDefault(v => v.LanguageType == language));
                }
            }

            foreach (var item in ribbonPanel.GetRibbonItems())
            {
                if (ribbonSettings.TryGetValue(item.Name, out List<RibbonDescription> value))
                {
                    item.UpdateRibbonDescription(value.First());
                    item.UpdateRibbonDescription(value.FirstOrDefault(v => v.LanguageType == language));
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
        /// Update RibbonDescription
        /// </summary>
        /// <typeparam name="TRibbonItem">RibbonItem</typeparam>
        /// <param name="ribbonItem"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static TRibbonItem UpdateRibbonDescription<TRibbonItem>(this TRibbonItem ribbonItem, RibbonDescription description) where TRibbonItem : RibbonItem
        {
            if (description == null)
                return ribbonItem;

            ribbonItem.SetText(description.Text);
            ribbonItem.SetToolTip(description.ToolTip);
            ribbonItem.SetLongDescription(description.LongDescription);
            ribbonItem.SetToolTipImage(description.ToolTipImage);
            ribbonItem.SetContextualHelp(description.Help);

            ribbonItem.SetImage(description.Image);
            ribbonItem.SetLargeImage(description.LargeImage);

            description.Action?.Invoke(ribbonItem);

            return ribbonItem;
        }

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
                ribbonItem.SetContextualHelp(GetContextualHelp(helpPath));

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
                    ribbonButton.LargeImage = largeImage;
                    if (ribbonButton.Image == null)
                        ribbonButton.Image = largeImage.Scale(0.5);
                }
            }

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

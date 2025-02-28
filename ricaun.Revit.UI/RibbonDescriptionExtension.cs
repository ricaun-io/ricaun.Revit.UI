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
        /// Add <see cref="RibbonDescription"/>
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
        /// Add <see cref="RibbonDescription"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ribbonDescriptions"></param>
        public void Add(string name, params RibbonDescription[] ribbonDescriptions)
        {
            foreach (var ribbonDescription in ribbonDescriptions)
                Add(name, ribbonDescription);
        }

        /// <summary>
        /// Add <see cref="RibbonDescription"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ribbonDescriptions"></param>
        public void Add<T>(params RibbonDescription[] ribbonDescriptions)
        {
            var name = typeof(T).GetName();
            Add(name, ribbonDescriptions);
        }

        /// <summary>
        /// Add <see cref="RibbonDescription"/> Action
        /// </summary>
        /// <param name="name"></param>
        /// <param name="actionRibbonDescriptions"></param>
        public void Add(string name, params Action<RibbonDescription>[] actionRibbonDescriptions)
        {
            foreach (var actionRibbonDescription in actionRibbonDescriptions)
            {
                var ribbonDescription = new RibbonDescription();
                actionRibbonDescription?.Invoke(ribbonDescription);
                Add(name, ribbonDescription);
            }
        }

        /// <summary>
        /// Add <see cref="RibbonDescription"/> Action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actionRibbonDescriptions"></param>
        public void Add<T>(params Action<RibbonDescription>[] actionRibbonDescriptions)
        {
            foreach (var actionRibbonDescription in actionRibbonDescriptions)
            {
                var ribbonDescription = new RibbonDescription();
                actionRibbonDescription?.Invoke(ribbonDescription);
                Add<T>(ribbonDescription);
            }
        }

        /// <summary>
        /// Add Default
        /// </summary>
        /// <param name="actionRibbonDescriptions"></param>
        public void AddDefault(params Action<RibbonDescription>[] actionRibbonDescriptions)
        {
            foreach (var actionRibbonDescription in actionRibbonDescriptions)
            {
                var ribbonDescription = new RibbonDescription();
                actionRibbonDescription?.Invoke(ribbonDescription);
                AddDefault(ribbonDescription);
            }
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
        public LanguageType LanguageType { get; set; }
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
        public object Image { get; set; }
        /// <summary>
        /// LargeImage
        /// </summary>
        public object LargeImage { get; set; }
        /// <summary>
        /// ToolTipImage
        /// </summary>
        public object ToolTipImage { get; set; }
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
            ribbonItem.SetContextualHelp(description.Help);

            //if (description.ToolTipImage is not null)
            //    ribbonItem.SetToolTipImage(description.ToolTipImage);

            //if (description.LargeImage is not null)
            //    ribbonItem.SetLargeImage(description.LargeImage);

            //if (description.Image is not null)
            //    ribbonItem.SetImage(description.Image);

            if (description.ToolTipImage is string stringToolTipImage)
                ribbonItem.SetToolTipImage(stringToolTipImage);
            if (description.ToolTipImage is ImageSource sourceToolTipImage)
                ribbonItem.SetToolTipImage(sourceToolTipImage);

            if (description.LargeImage is string stringLargeImage)
                ribbonItem.SetLargeImage(stringLargeImage);
            if (description.LargeImage is ImageSource sourceLargeImage)
                ribbonItem.SetLargeImage(sourceLargeImage);

            if (description.Image is string stringImage)
                ribbonItem.SetImage(stringImage);
            if (description.Image is ImageSource sourceImage)
                ribbonItem.SetImage(sourceImage);

            description.Action?.Invoke(ribbonItem);

            return ribbonItem;
        }
    }
}

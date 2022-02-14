using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonRadioExtension
    /// </summary>
    public static class RibbonRadioExtension
    {
        /// <summary>
        /// CreateRadioButtonGroup
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetText"></param>
        /// <param name="toggleButtonDatas"></param>
        /// <returns></returns>
        public static RadioButtonGroup CreateRadioButtonGroup(this RibbonPanel ribbonPanel, string targetText, params ToggleButtonData[] toggleButtonDatas)
        {
            RadioButtonGroup radioButtonGroup = null;
            if (toggleButtonDatas.Any())
            {
                if (targetText == null) targetText = toggleButtonDatas.FirstOrDefault().Text;
                var targetName = targetText;

                radioButtonGroup = ribbonPanel.AddItem(ribbonPanel.NewRadioButtonGroupData(targetName)) as RadioButtonGroup;
                AddItems(radioButtonGroup, toggleButtonDatas);
            }
            return radioButtonGroup;
        }

        /// <summary>
        /// AddItems
        /// </summary>
        /// <param name="radioButtonGroup"></param>
        /// <param name="toggleButtonDatas"></param>
        /// <returns></returns>
        public static RadioButtonGroup AddItems(this RadioButtonGroup radioButtonGroup, params ToggleButtonData[] toggleButtonDatas)
        {
            foreach (var toggleButtonData in toggleButtonDatas)
            {
                while (RibbonSafeExtension.VerifyNameExclusive(radioButtonGroup, toggleButtonData.Name))
                    toggleButtonData.Name = RibbonSafeExtension.SafeButtonName(radioButtonGroup.Name);

                radioButtonGroup.AddItem(toggleButtonData);
            }
            return radioButtonGroup;
        }

        #region ToggleButtonData
        /// <summary>
        /// NewToggleButtonData
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetName"></param>
        /// <returns></returns>
        public static ToggleButtonData NewToggleButtonData(this RibbonPanel ribbonPanel, string targetName)
        {
            if (targetName.Trim() == string.Empty)
                targetName = "-";

            while (RibbonSafeExtension.VerifyNameExclusive(ribbonPanel, targetName))
                targetName = RibbonSafeExtension.SafeButtonName(targetName);

            return new ToggleButtonData(targetName, targetName);
        }

        /// <summary>
        /// NewToggleButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ToggleButtonData NewToggleButtonData<TExternalCommand>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            var commandType = typeof(TExternalCommand);
            return ribbonPanel.NewToggleButtonData(commandType, text);
        }

        /// <summary>
        /// NewToggleButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ToggleButtonData NewToggleButtonData<TExternalCommand, TAvailability>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            var button = ribbonPanel.NewToggleButtonData<TExternalCommand>(text);
            button.AvailabilityClassName = typeof(TAvailability).FullName;
            return button;
        }

        /// <summary>
        /// NewToggleButtonData
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="commandType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ToggleButtonData NewToggleButtonData(this RibbonPanel ribbonPanel, Type commandType, string text = null)
        {
            var targetName = commandType.GetName();
            var targetText = targetName;
            var assemblyName = commandType.Assembly.Location;
            var className = commandType.FullName;

            if (text != null && text != "") targetText = text;

            while (RibbonSafeExtension.VerifyNameExclusive(ribbonPanel, targetName))
                targetName = RibbonSafeExtension.SafeButtonName(targetText);

            ToggleButtonData button = new ToggleButtonData(targetName, targetText, assemblyName, className);

            if (text == "") button.Text = "-";

            return button;
        }

        #endregion

        #region RadioButtonGroupData
        /// <summary>
        /// NewRadioButtonGroupData
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetName"></param>
        /// <returns></returns>
        public static RadioButtonGroupData NewRadioButtonGroupData(this RibbonPanel ribbonPanel, string targetName)
        {
            if (targetName.Trim() == string.Empty)
                targetName = "-";

            var radioButtonGroupData = new RadioButtonGroupData(targetName);

            while (RibbonSafeExtension.VerifyNameExclusive(ribbonPanel, radioButtonGroupData.Name))
                radioButtonGroupData.Name = RibbonSafeExtension.SafeButtonName(targetName);

            return radioButtonGroupData;
        }
        #endregion

    }
}

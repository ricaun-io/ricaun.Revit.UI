using Autodesk.Revit.UI;
using System.Linq;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonComboBoxExtension
    /// </summary>
    public static class RibbonComboBoxExtension
    {
        #region ComboBox
        /// <summary>
        /// CreateComboBox
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="comboBoxMemberDatas"></param>
        /// <returns></returns>
        public static ComboBox CreateComboBox(this RibbonPanel ribbonPanel, params ComboBoxMemberData[] comboBoxMemberDatas)
        {
            return ribbonPanel.CreateComboBox(null, comboBoxMemberDatas);
        }

        /// <summary>
        /// CreateComboBox
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetText"></param>
        /// <param name="comboBoxMemberDatas"></param>
        /// <returns></returns>
        public static ComboBox CreateComboBox(this RibbonPanel ribbonPanel, string targetText, params ComboBoxMemberData[] comboBoxMemberDatas)
        {
            ComboBox comboBox = null;

            if (string.IsNullOrWhiteSpace(targetText))
                targetText = comboBoxMemberDatas.FirstOrDefault()?.Text ?? nameof(ComboBox);

            var targetName = targetText;

            comboBox = ribbonPanel.AddItem(ribbonPanel.NewComboBoxData(targetName)) as ComboBox;

            comboBox.AddComboBoxMembers(comboBoxMemberDatas);

            return comboBox;
        }

        /// <summary>
        /// AddComboBoxMembers
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="comboBoxMemberDatas"></param>
        /// <returns></returns>
        public static ComboBox AddComboBoxMembers(this ComboBox comboBox, params ComboBoxMemberData[] comboBoxMemberDatas)
        {
            foreach (var comboBoxMemberData in comboBoxMemberDatas)
            {
                comboBoxMemberData.Name = RibbonSafeExtension.GenerateSafeButtonName(comboBox, comboBoxMemberData.Name, comboBox.Name);

                comboBox.AddItem(comboBoxMemberData);
            }
            return comboBox;
        }

        #region ComboBoxData
        /// <summary>
        /// NewComboBoxData
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetName"></param>
        /// <returns></returns>
        public static ComboBoxData NewComboBoxData(this RibbonPanel ribbonPanel, string targetName = null)
        {
            if (string.IsNullOrWhiteSpace(targetName))
                targetName = nameof(ComboBoxData);

            targetName = RibbonSafeExtension.GenerateSafeButtonName(ribbonPanel, targetName, targetName);

            return new ComboBoxData(targetName);
        }
        #endregion

        #region ComboBoxMemberData
        /// <summary>
        /// SetGroupName
        /// </summary>
        /// <param name="comboBoxMemberData"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static ComboBoxMemberData SetGroupName(this ComboBoxMemberData comboBoxMemberData, string groupName)
        {
            comboBoxMemberData.GroupName = groupName;
            return comboBoxMemberData;
        }
        /// <summary>
        /// NewComboBoxMemberData
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetName"></param>
        /// <returns></returns>
        public static ComboBoxMemberData NewComboBoxMemberData(this RibbonPanel ribbonPanel, string targetName = null)
        {
            if (string.IsNullOrWhiteSpace(targetName))
                targetName = nameof(ComboBoxMemberData);

            var comboBoxMemberData = new ComboBoxMemberData(targetName, targetName);

            var targetText = targetName;

            comboBoxMemberData.Name = RibbonSafeExtension.GenerateSafeButtonName(ribbonPanel, comboBoxMemberData.Name, targetText);

            return comboBoxMemberData;
        }
        #endregion

        #endregion
    }
}

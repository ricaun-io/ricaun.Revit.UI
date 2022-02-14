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
        /// <param name="targetText"></param>
        /// <param name="comboBoxMemberDatas"></param>
        /// <returns></returns>
        public static ComboBox CreateComboBox(this RibbonPanel ribbonPanel, string targetText, params ComboBoxMemberData[] comboBoxMemberDatas)
        {
            ComboBox comboBox = null;
            if (comboBoxMemberDatas.Any())
            {
                if (targetText == null) targetText = comboBoxMemberDatas.FirstOrDefault().Text;
                var targetName = targetText;

                comboBox = ribbonPanel.AddItem(ribbonPanel.NewComboBoxData(targetName)) as ComboBox;
                comboBox.AddItems(comboBoxMemberDatas);
            }
            return comboBox;
        }

        /// <summary>
        /// AddItems
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="comboBoxMemberDatas"></param>
        /// <returns></returns>
        public static ComboBox AddItems(this ComboBox comboBox, params ComboBoxMemberData[] comboBoxMemberDatas)
        {
            foreach (var comboBoxMemberData in comboBoxMemberDatas)
            {
                while (RibbonSafeExtension.VerifyNameExclusive(comboBox, comboBoxMemberData.Name))
                    comboBoxMemberData.Name = RibbonSafeExtension.SafeButtonName(comboBox.Name);

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
        public static ComboBoxData NewComboBoxData(this RibbonPanel ribbonPanel, string targetName)
        {
            while (RibbonSafeExtension.VerifyNameExclusive(ribbonPanel, targetName))
                targetName = RibbonSafeExtension.SafeButtonName(targetName);

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
        public static ComboBoxMemberData NewComboBoxMemberData(this RibbonPanel ribbonPanel, string targetName)
        {
            if (targetName.Trim() == string.Empty)
                targetName = "-";

            var comboBoxMemberData = new ComboBoxMemberData(targetName, targetName);
            while (RibbonSafeExtension.VerifyNameExclusive(ribbonPanel, comboBoxMemberData.Name))
                comboBoxMemberData.Name = RibbonSafeExtension.SafeButtonName(targetName);

            return comboBoxMemberData;
        }
        #endregion

        #endregion
    }
}

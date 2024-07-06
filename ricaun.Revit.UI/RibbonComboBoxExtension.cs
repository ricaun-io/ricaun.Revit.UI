using Autodesk.Revit.UI;
using System;
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

        /// <summary>
        /// SetWidth
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static ComboBox SetWidth(this ComboBox comboBox, double width)
        {
            comboBox.GetRibbonItem().Width = width;
            return comboBox;
        }

        /// <summary>
        /// SetCurrent
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ComboBox SetCurrent(this ComboBox comboBox, ComboBoxMember current)
        {
            comboBox.Current = current;
            return comboBox;
        }

        /// <summary>
        /// AddCurrentChanged
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="currentChanged"></param>
        /// <returns></returns>
        public static ComboBox AddCurrentChanged(this ComboBox comboBox, EventHandler<Autodesk.Revit.UI.Events.ComboBoxCurrentChangedEventArgs> currentChanged)
        {
            comboBox.CurrentChanged += currentChanged;
            return comboBox;
        }

        /// <summary>
        /// RemoveCurrentChanged
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="currentChanged"></param>
        /// <returns></returns>
        public static ComboBox RemoveCurrentChanged(this ComboBox comboBox, EventHandler<Autodesk.Revit.UI.Events.ComboBoxCurrentChangedEventArgs> currentChanged)
        {
            comboBox.CurrentChanged -= currentChanged;
            return comboBox;
        }

        /// <summary>
        /// AddDropDownOpened
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="dropDownOpened"></param>
        /// <returns></returns>
        public static ComboBox AddDropDownOpened(this ComboBox comboBox, EventHandler<Autodesk.Revit.UI.Events.ComboBoxDropDownOpenedEventArgs> dropDownOpened)
        {
            comboBox.DropDownOpened += dropDownOpened;
            return comboBox;
        }

        /// <summary>
        /// RemoveDropDownOpened
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="dropDownOpened"></param>
        /// <returns></returns>
        public static ComboBox RemoveDropDownOpened(this ComboBox comboBox, EventHandler<Autodesk.Revit.UI.Events.ComboBoxDropDownOpenedEventArgs> dropDownOpened)
        {
            comboBox.DropDownOpened -= dropDownOpened;
            return comboBox;
        }

        /// <summary>
        /// AddDropDownClosed
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="dropDownClosed"></param>
        /// <returns></returns>
        public static ComboBox AddDropDownClosed(this ComboBox comboBox, EventHandler<Autodesk.Revit.UI.Events.ComboBoxDropDownClosedEventArgs> dropDownClosed)
        {
            comboBox.DropDownClosed += dropDownClosed;
            return comboBox;
        }

        /// <summary>
        /// RemoveDropDownClosed
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="dropDownClosed"></param>
        /// <returns></returns>
        public static ComboBox RemoveDropDownClosed(this ComboBox comboBox, EventHandler<Autodesk.Revit.UI.Events.ComboBoxDropDownClosedEventArgs> dropDownClosed)
        {
            comboBox.DropDownClosed -= dropDownClosed;
            return comboBox;
        }

        #region ComboBoxMember
        /// <summary>
        /// CreateComboBoxMember
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="targetName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static ComboBoxMember CreateComboBoxMember(this ComboBox comboBox, string targetName = null, string groupName = null)
        {
            if (string.IsNullOrWhiteSpace(targetName))
                targetName = nameof(ComboBoxMemberData);

            var comboBoxMemberData = new ComboBoxMemberData(targetName, targetName);

            if (!string.IsNullOrWhiteSpace(groupName))
                comboBoxMemberData.SetGroupName(groupName);
            
            var targetText = targetName;

            comboBoxMemberData.Name = RibbonSafeExtension.GenerateSafeButtonName(comboBox, comboBoxMemberData.Name, targetText);

            return comboBox.AddItem(comboBoxMemberData);
        }
        #endregion

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

using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// RibbonPanelExtension
    /// </summary>
    public static class RibbonPanelExtension
    {
        #region Panel
        /// <summary>
        /// Create RibbonPanel
        /// </summary>
        /// <param name="application"></param>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static RibbonPanel CreatePanel(this UIControlledApplication application, string panelName)
        {
            RibbonPanel ribbonManager;
            try
            {
                ribbonManager = application.CreateRibbonPanel(panelName);
            }
            catch
            {
                ribbonManager = application.CreateRibbonPanel(SafeRibbonPanelName(panelName));
                ribbonManager.Title = panelName;
            }
            return ribbonManager;
        }

        /// <summary>
        /// Create RibbonPanel
        /// </summary>
        /// <param name="application"></param>
        /// <param name="tabName"></param>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public static RibbonPanel CreatePanel(this UIControlledApplication application, string tabName, string panelName)
        {
            RibbonPanel ribbonManager = null;
            try { application.CreateRibbonTab(tabName); } catch { }
            try
            {
                ribbonManager = application.CreateRibbonPanel(tabName, panelName);
            }
            catch
            {
                if (ribbonManager == null)
                {
                    ribbonManager = application.CreateRibbonPanel(tabName, SafeRibbonPanelName(panelName));
                    ribbonManager.Title = panelName;
                }
            }
            return ribbonManager;
        }

        /// <summary>
        /// Remove RibbonPanel from Tab
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        public static RibbonPanel Remove(this RibbonPanel ribbonPanel)
        {
            foreach (var ribbonItem in ribbonPanel.GetRibbonItems())
                ribbonItem.GetRibbonItem().RemoveQuickAccessToolBar();

            ribbonPanel.Visible = false;
            ribbonPanel.Enabled = false;

            var panel = ribbonPanel.GetRibbonPanel();
            panel.Tab.Panels.Remove(panel);
            return ribbonPanel;
        }

        /// <summary>
        /// Remove RibbonPanel from Tab
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        public static RibbonPanel Close(this RibbonPanel ribbonPanel)
        {
            return ribbonPanel.Remove();
        }
        #endregion

        #region RibbonItems

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
        /// Remove <paramref name="ribbonItem"/> from <paramref name="ribbonPanel"/>
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        public static RibbonPanel Remove(this RibbonPanel ribbonPanel, RibbonItem ribbonItem)
        {
            var aRibbonItem = ribbonItem.GetRibbonItem();
            var aRibbonPanel = ribbonPanel.GetRibbonPanel();

            aRibbonItem.RemoveQuickAccessToolBar();
            aRibbonPanel.Source.Items.Remove(aRibbonItem);

            foreach (var item in aRibbonPanel.Source.Items)
            {
                if (item is Autodesk.Windows.RibbonListButton ribbonListButton)
                    ribbonListButton.Items?.Remove(aRibbonItem);
                if (item is Autodesk.Windows.RibbonRowPanel ribbonRowPanel)
                    ribbonRowPanel.Items?.Remove(aRibbonItem);
            }

            return ribbonPanel;
        }
        #endregion

        #region PushButtonData
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="commandType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData(this RibbonPanel ribbonPanel, Type commandType, string text = null)
        {
            var targetName = commandType.GetName();
            var targetText = targetName;
            var assemblyName = commandType.Assembly.Location;
            var className = commandType.FullName;

            if (text != null && text != "") targetText = text;

            while (verifyNameExclusive(ribbonPanel, targetName))
            {
                targetName = SafeButtonName(targetText);
            }

            PushButtonData currentBtn = new PushButtonData(targetName, targetText, assemblyName, className);

            if (text == "") currentBtn.Text = "-";

            return currentBtn;
        }

        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData<TExternalCommand>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            var commandType = typeof(TExternalCommand);
            return ribbonPanel.NewPushButtonData(commandType, text);
        }
        /// <summary>
        /// NewPushButtonData
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButtonData NewPushButtonData<TExternalCommand, TAvailability>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            PushButtonData currentBtn = ribbonPanel.NewPushButtonData<TExternalCommand>(text);
            currentBtn.AvailabilityClassName = typeof(TAvailability).FullName;
            return currentBtn;
        }
        #endregion

        #region AddPushButton
        /// <summary>
        /// AddPushButton
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButton AddPushButton<TExternalCommand>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new()
        {
            PushButton pushButton = ribbonPanel.AddItem(ribbonPanel.NewPushButtonData<TExternalCommand>(text)) as PushButton;
            return pushButton;
        }
        /// <summary>
        /// AddPushButton
        /// </summary>
        /// <typeparam name="TExternalCommand"></typeparam>
        /// <typeparam name="TAvailability"></typeparam>
        /// <param name="ribbonPanel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static PushButton AddPushButton<TExternalCommand, TAvailability>(this RibbonPanel ribbonPanel, string text = null) where TExternalCommand : class, IExternalCommand, new() where TAvailability : class, IExternalCommandAvailability, new()
        {
            PushButton pushButton = ribbonPanel.AddPushButton<TExternalCommand>(text);
            pushButton.AvailabilityClassName = typeof(TAvailability).FullName;
            return pushButton;
        }
        #endregion

        #region SplitButton

        /// <summary>
        /// CreateSplitButton
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetPushButtons"></param>
        /// <returns></returns>
        public static SplitButton CreateSplitButton(this RibbonPanel ribbonPanel, params PushButtonData[] targetPushButtons)
        {
            return ribbonPanel.CreateSplitButton(null, targetPushButtons);
        }

        /// <summary>
        /// CreateSplitButton
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetText"></param>
        /// <param name="targetPushButtons"></param>
        /// <returns></returns>
        public static SplitButton CreateSplitButton(this RibbonPanel ribbonPanel, string targetText, params PushButtonData[] targetPushButtons)
        {
            SplitButton currentSplitButton = null;
            if (targetPushButtons.Any())
            {
                if (targetText == null) targetText = targetPushButtons.FirstOrDefault().Text;
                var targetName = targetText;

                while (verifyNameExclusive(ribbonPanel, targetName))
                {
                    targetName = SafeButtonName(targetText);
                }

                currentSplitButton = ribbonPanel.AddItem(new SplitButtonData(targetName, targetText)) as SplitButton;

                foreach (PushButtonData currentPushButton in targetPushButtons)
                {
                    while (verifyNameExclusive(currentSplitButton, currentPushButton.Name))
                    {
                        currentPushButton.Name = SafeButtonName(targetText);
                    }
                    currentSplitButton.AddPushButton(currentPushButton);
                }
            }

            return currentSplitButton;
        }
        #endregion

        #region PulldownButton

        /// <summary>
        /// CreatePulldownButton
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetPushButtons"></param>
        /// <returns></returns>
        public static PulldownButton CreatePulldownButton(this RibbonPanel ribbonPanel, params PushButtonData[] targetPushButtons)
        {
            return ribbonPanel.CreatePulldownButton(null, targetPushButtons);
        }

        /// <summary>
        /// CreatePulldownButton
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="targetText"></param>
        /// <param name="targetPushButtons"></param>
        /// <returns></returns>
        public static PulldownButton CreatePulldownButton(this RibbonPanel ribbonPanel, string targetText, params PushButtonData[] targetPushButtons)
        {
            PulldownButton currentPulldownButton = null;
            if (targetPushButtons.Any())
            {
                if (targetText == null) targetText = targetPushButtons.FirstOrDefault().Text;
                var targetName = targetText;

                while (verifyNameExclusive(ribbonPanel, targetName))
                {
                    targetName = SafeButtonName(targetText);
                }

                currentPulldownButton = ribbonPanel.AddItem(new PulldownButtonData(targetName, targetText)) as PulldownButton;

                foreach (PushButtonData currentPushButton in targetPushButtons)
                {
                    while (verifyNameExclusive(currentPulldownButton, currentPushButton.Name))
                    {
                        currentPushButton.Name = SafeButtonName(targetText);
                    }
                    currentPulldownButton.AddPushButton(currentPushButton);
                }
            }
            return currentPulldownButton;
        }
        #endregion

        #region private

        /// <summary>
        /// Safe Button Name
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        private static string SafeButtonName(string buttonName)
        {
            return $"{buttonName}_{TickNumber}";
        }

        /// <summary>
        /// Safe Ribbon Panel Name
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        private static string SafeRibbonPanelName(string panelName)
        {
            return $"{System.DateTime.Now.Ticks + TickNumber}%{panelName}";
        }

        /// <summary>
        /// _TickNumber
        /// </summary>
        private static long _TickNumber;
        /// <summary>
        /// TickNumber ++ 
        /// </summary>
        private static long TickNumber => _TickNumber++;

        /// <summary>
        /// Verify if Panel has Name
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool verifyNameExclusive<T>(T ribbonPanel, string name)
        {
            var type = typeof(T);
            try
            {
                type.GetMethod("verifyNameExclusive",
                    BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.Invoke(ribbonPanel, new[] { name });
                return false;
            }
            catch { }
            return true;
        }

        #endregion
    }
}

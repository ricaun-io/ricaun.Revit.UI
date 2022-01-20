using Autodesk.Revit.UI;
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
                //ribbonManager = application.GetRibbonPanels(tabName).FirstOrDefault(r => r.Name.StartsWith(panelName) && r.Visible);
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

        #region PushButtonData
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
            var targetName = commandType.Name;
            var targetText = commandType.Name;
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

        #region 
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
        public static SplitButton CreateSplitButton(this RibbonPanel ribbonPanel, IList<PushButtonData> targetPushButtons)
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
        public static SplitButton CreateSplitButton(this RibbonPanel ribbonPanel, string targetText, IList<PushButtonData> targetPushButtons)
        {
            SplitButton currentSplitButton = null;
            if (targetPushButtons.Count > 0)
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
        public static PulldownButton CreatePulldownButton(this RibbonPanel ribbonPanel, IList<PushButtonData> targetPushButtons)
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
        public static PulldownButton CreatePulldownButton(this RibbonPanel ribbonPanel, string targetText, IList<PushButtonData> targetPushButtons)
        {
            PulldownButton currentPulldownButton = null;
            if (targetPushButtons.Count > 0)
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

        #region AddStackedItems
        /*
        public static IList<RibbonItem> AddStackedItems(IList<RibbonItemData> items)
        {

        }*/

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

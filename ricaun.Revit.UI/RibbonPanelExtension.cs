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
        /// Close Setting Visible off
        /// </summary>
        /// <param name="ribbonPanel"></param>
        /// <returns></returns>
        public static RibbonPanel Close(this RibbonPanel ribbonPanel)
        {
            ribbonPanel.Visible = false;
            return ribbonPanel;
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
            var currentDll = commandType.Assembly.Location;
            string fullname = commandType.FullName;
            string targetName = commandType.Name;
            string targetText = commandType.Name;

            while (verifyNameExclusive(ribbonPanel, targetName))
            {
                targetName = SafeButtonName(targetText);
            }

            PushButtonData currentBtn = new PushButtonData(targetName, targetText, currentDll, fullname);
            if (text != null) currentBtn.Text = text;
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
            PushButton currentBtn = ribbonPanel.AddItem(ribbonPanel.NewPushButtonData<TExternalCommand>(text)) as PushButton;
            return currentBtn;
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
            PushButton currentBtn = ribbonPanel.AddPushButton<TExternalCommand>(text);
            currentBtn.AvailabilityClassName = typeof(TAvailability).FullName;
            return currentBtn;
        }
        #endregion

        #region SplitButton

        /// <summary>
        /// CreateSplitButton
        /// </summary>
        /// <param name="targetPanel"></param>
        /// <param name="targetPushButtons"></param>
        /// <returns></returns>
        public static SplitButton CreateSplitButton(this RibbonPanel targetPanel, IList<PushButtonData> targetPushButtons)
        {
            return targetPanel.CreateSplitButton(null, targetPushButtons);
        }

        /// <summary>
        /// CreateSplitButton
        /// </summary>
        /// <param name="targetPanel"></param>
        /// <param name="targetName"></param>
        /// <param name="targetPushButtons"></param>
        /// <returns></returns>
        public static SplitButton CreateSplitButton(this RibbonPanel targetPanel, string targetName, IList<PushButtonData> targetPushButtons)
        {
            SplitButton currentSplitButton = null;
            if (targetPushButtons.Count > 0)
            {
                if (targetName == null) targetName = targetPushButtons.FirstOrDefault().Text;
                try
                {
                    currentSplitButton = targetPanel.AddItem(new SplitButtonData(targetName, targetName)) as SplitButton;
                }
                catch
                {
                    currentSplitButton = targetPanel.AddItem(new SplitButtonData(SafeButtonName(targetName), targetName)) as SplitButton;
                }

                foreach (PushButtonData currentPushButton in targetPushButtons)
                {
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
                    currentPulldownButton.AddPushButton(currentPushButton);
                }
            }
            return currentPulldownButton;
        }
        #endregion

        #region private

        private static string SafeButtonName(string buttonName)
        {
            return $"{buttonName} {System.DateTime.Now.Ticks}";
        }

        private static string SafeRibbonPanelName(string panelName)
        {
            return $"{System.DateTime.Now.Ticks}%{panelName}";
        }

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

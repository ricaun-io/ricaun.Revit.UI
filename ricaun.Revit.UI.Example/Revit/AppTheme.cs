using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI.Utils;

namespace ricaun.Revit.UI.Example.Revit
{
    [AppLoader]
    public class AppTheme : IExternalApplication
    {
        const string LIGHT = "https://github.com/ricaun-io/Autodesk.Icon.Example/releases/download/1.0.1-alpha/Box-Blue-Light.ico";
        const string DARK = "https://github.com/ricaun-io/Autodesk.Icon.Example/releases/download/1.0.1-alpha/Box-Blue-Dark.ico";

        private RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("Theme");

            ribbonPanel.CreatePushButton<CommandTheme>("Light")
                .SetLargeImage(LIGHT);
            ribbonPanel.CreatePushButton<CommandTheme>("Dark")
                .SetLargeImage(DARK);
            string stringNull = null;
            ribbonPanel.CreatePushButton<CommandTheme>("Null")
                .SetLargeImage(LIGHT)
                .SetLargeImage(stringNull);

            ribbonPanel.FlowStackedItems(
                ribbonPanel.CreatePushButton<CommandTheme>("1").SetLargeImage(LIGHT),
                ribbonPanel.CreatePushButton<CommandTheme>("2").SetLargeImage(DARK),
                ribbonPanel.CreatePushButton<CommandTheme>("3").SetLargeImage(LIGHT)
                );

            ribbonPanel.AddSeparator();

            var pushButton = ribbonPanel.CreatePushButton<CommandTheme>("Theme")
                .SetLargeImage(LIGHT);

            var textBox = ribbonPanel.CreateTextBox()
                .SetPromptText("TextBox")
                .SetLargeImage(LIGHT);

            var comboBox = ribbonPanel.CreateComboBox()
                .SetLargeImage(LIGHT);
            comboBox.CreateComboBoxMember("ComboBox")
                .SetLargeImage(LIGHT);

            ribbonPanel.RowStackedItems(pushButton, textBox, comboBox);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }

        [Transaction(TransactionMode.Manual)]
        public class CommandTheme : IExternalCommand, IExternalCommandAvailability
        {
            public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
            {
                UIApplication uiapp = commandData.Application;

                UIThemeManager.CurrentTheme = UIThemeManager.CurrentTheme == UITheme.Light ? UITheme.Dark : UITheme.Light;
                return Result.Succeeded;
            }

            public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
            {
                return true;
            }
        }
    }
}
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI.Utils;

namespace ricaun.Revit.UI.Example.Revit
{
    [AppLoader]
    public class AppTheme : IExternalApplication
    {
        static string LIGHT = "https://github.com/ricaun-io/Autodesk.Icon.Example/releases/download/1.1.0/Box-Grey-Dark.ico";
        static string DARK = "https://github.com/ricaun-io/Autodesk.Icon.Example/releases/download/1.1.0/Box-Grey-Dark.ico";
        static string LIGHT_RED = "https://github.com/ricaun-io/Autodesk.Icon.Example/releases/download/1.1.0/Box-Red-Light.ico";
        static string DARK_RED = "https://github.com/ricaun-io/Autodesk.Icon.Example/releases/download/1.1.0/Box-Red-Dark.ico";
        static string LIGHT_GREEN = "https://github.com/ricaun-io/Autodesk.Icon.Example/releases/download/1.1.0/Box-Green-Light.ico";
        static string DARK_GREEN = "https://github.com/ricaun-io/Autodesk.Icon.Example/releases/download/1.1.0/Box-Green-Dark.ico";
        static string LIGHT_BLUE = "https://github.com/ricaun-io/Autodesk.Icon.Example/releases/download/1.1.0/Box-Blue-Light.ico";
        static string DARK_BLUE = "https://github.com/ricaun-io/Autodesk.Icon.Example/releases/download/1.1.0/Box-Blue-Dark.ico";
        const string REVIT = "/UIFrameworkRes;component/ribbon/images/revit.ico";
        const string Base64Image = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAHYcAAB2HAY/l8WUAAABOSURBVEhLtccxDQAwDMCw8odRdIWxP7cn+fHM3l8913M913M913M913M913M913M913M913M913M913M913M913M913M913M913O9tfcAG98oW3bd33wAAAAASUVORK5CYII=";

        private RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("Theme");

            //ribbonPanel.CreatePushButton<CommandTheme>("Revit")
            //    .SetLargeImage(REVIT);

            //ribbonPanel.CreatePushButton<CommandTheme>("Base64")
            //    .SetLargeImage(Base64Image);
            
            ribbonPanel.CreatePushButton<CommandTheme>("Light")
                .SetLargeImage(LIGHT);
            ribbonPanel.CreatePushButton<CommandTheme>("Dark")
                .SetLargeImage(DARK);

            ribbonPanel.CreatePushButton<CommandTheme>(".")
                .SetLargeImage(LIGHT_GREEN)
                .SetImage(LIGHT_RED);

            ribbonPanel.CreatePushButton<CommandTheme>(".")
                .SetLargeImage(LIGHT_GREEN)
                .SetImage(LIGHT_RED)
                .SetItemSize(Autodesk.Windows.RibbonItemSize.Standard);

            ribbonPanel.RowStackedItems(
                ribbonPanel.CreatePushButton<CommandTheme>("Grey")
                    .SetLargeImage("Resources/Box-Grey-Light.ico"),
                ribbonPanel.CreatePushButton<CommandTheme>("Grey")
                    .SetLargeImage("Resources/Box-Grey-Light.ico"),
                ribbonPanel.CreatePushButton<CommandTheme>("Grey")
                    .SetLargeImage("Resources/Box-Grey-Light.ico")
                );
            ribbonPanel.RowLargeStackedItems(
                ribbonPanel.CreatePushButton<CommandTheme>("Grey")
                    .SetLargeImage("Resources/Box-Grey-Light.ico"),
                ribbonPanel.CreatePushButton<CommandTheme>("Grey")
                    .SetLargeImage("Resources/Box-Grey-Light.ico")
                );

            ribbonPanel.AddSeparator();


            {
                var buttonTiff = ribbonPanel.CreatePushButton<CommandTheme>("Grey")
                    .SetLargeImage("Resources/Cube-Grey-Light.tiff");
                if (buttonTiff.LargeImage is System.Windows.Media.Imaging.BitmapSource largeImage)
                {
                    System.Console.WriteLine($"{largeImage.GetType().Name} | {largeImage.Width:0}x{largeImage.Height:0} ({largeImage.PixelWidth}x{largeImage.PixelHeight}) {largeImage.DpiX:0}:{largeImage.DpiY:0}");
                }
                if (buttonTiff.Image is System.Windows.Media.Imaging.BitmapSource smallImage)
                {
                    System.Console.WriteLine($"{smallImage.GetType().Name} | {smallImage.Width:0}x{smallImage.Height:0} ({smallImage.PixelWidth}x{smallImage.PixelHeight}) {smallImage.DpiX:0}:{smallImage.DpiY:0}");
                }
            }

            ribbonPanel.RowStackedItems(
                ribbonPanel.CreatePushButton<CommandTheme>("Grey")
                    .SetLargeImage("Resources/Cube-Grey-Light.tiff"),
                ribbonPanel.CreatePushButton<CommandTheme>("Grey")
                    .SetLargeImage("Resources/Cube-Grey-Light.tiff"),
                ribbonPanel.CreatePushButton<CommandTheme>("Grey")
                    .SetLargeImage("Resources/Cube-Grey-Light.tiff")
                );
            ribbonPanel.RowLargeStackedItems(
                ribbonPanel.CreatePushButton<CommandTheme>("Grey")
                    .SetLargeImage("Resources/Cube-Grey-Light.tiff"),
                ribbonPanel.CreatePushButton<CommandTheme>("Grey")
                    .SetLargeImage("Resources/Cube-Grey-Light.tiff")
                );

            ribbonPanel.AddSeparator();

            ribbonPanel.FlowStackedItems(
                ribbonPanel.CreatePushButton<CommandTheme>("1").SetLargeImage(LIGHT_RED),
                ribbonPanel.CreatePushButton<CommandTheme>("2").SetLargeImage(DARK_RED),
                ribbonPanel.CreatePushButton<CommandTheme>("3").SetLargeImage(LIGHT_BLUE),
                ribbonPanel.CreatePushButton<CommandTheme>("4").SetLargeImage(DARK_GREEN),
                ribbonPanel.CreatePushButton<CommandTheme>("5").SetLargeImage(LIGHT_GREEN),
                ribbonPanel.CreatePushButton<CommandTheme>("6").SetLargeImage(DARK_BLUE)
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
            comboBox.CreateComboBoxMember("Red")
                .SetLargeImage(LIGHT_RED);
            comboBox.CreateComboBoxMember("Green")
                .SetLargeImage(LIGHT_GREEN);
            comboBox.CreateComboBoxMember("Blue")
                .SetLargeImage(LIGHT_BLUE);

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
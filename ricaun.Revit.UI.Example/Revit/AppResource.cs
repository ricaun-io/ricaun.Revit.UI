using Autodesk.Revit.UI;

namespace ricaun.Revit.UI.Example.Revit
{
    //[AppLoader]
    public class AppResource : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("Resource");

            ribbonPanel.RowLargeStackedItems(
                ribbonPanel.CreatePushButton<Commands.CommandAvailable>("Revit")
                    .SetLargeImage("/Resources/Revit.ico"),
                ribbonPanel.CreatePushButton<Commands.CommandAvailable>("Revit")
                    .SetLargeImage($"/{typeof(AppResource).Assembly.GetName().Name};component/Resources/revit.ico"),
                ribbonPanel.CreatePushButton<Commands.CommandAvailable>("Revit")
                    .SetLargeImage("UIFrameworkRes;component/ribbon/images/revit.ico"),
                 ribbonPanel.CreatePushButton<Commands.CommandAvailable>("Revit")
                    .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico")
                );

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }
}
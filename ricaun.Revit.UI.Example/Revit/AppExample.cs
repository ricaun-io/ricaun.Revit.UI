using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;

namespace ricaun.Revit.UI.Example.Revit
{
    //[AppLoader]
    public class AppExample : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("ricaun", "ricaun");

            var ribbonItem = ribbonPanel.CreatePushButton<Commands.Command>()
                .SetText("Command")
                .SetToolTip("This is a tooltip.")
                .SetLongDescription("This is a description.")
                .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");

            if (LanguageExtension.IsBrazilianPortuguese)
            {
                ribbonItem.SetText("Comando")
                    .SetToolTip("Esta é uma dica de ferramenta.")
                    .SetLongDescription("Esta é uma descrição.");
            }

            //ribbonPanel.MoveToRibbonTab();

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }
}
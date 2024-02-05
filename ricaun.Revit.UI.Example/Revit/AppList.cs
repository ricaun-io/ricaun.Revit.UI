using Autodesk.Revit.UI;
using ricaun.Revit.UI.Example.Proprieties;
using System.Linq;

namespace ricaun.Revit.UI.Example.Revit
{
    [AppLoader]
    public class AppList : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("List");

            PushButtonData[] NewButtons(int number)
            {
                var itens = Enumerable.Range(1, number).Select(i =>
                    ribbonPanel.NewPushButtonData<Commands.Command>($"{i}")
                        .SetToolTip($"{i}")
                        .SetToolTip(Icons.Icon.ToString())
                        .SetLargeImage(Icons.Icon)
                );
                return itens.ToArray();
            }

            var pulldownButton = ribbonPanel.CreatePulldownButton("PulldownButton", NewButtons(3))
                .SetLargeImage(Icons.Icon)
                .SetListImageSize();

            var splitButton = ribbonPanel.CreateSplitButton("PulldownButton", NewButtons(3))
                .SetListImageSize();

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }

}
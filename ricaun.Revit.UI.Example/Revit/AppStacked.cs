using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System.Linq;

namespace ricaun.Revit.UI.Example.Revit
{
    [AppLoader]
    public class AppStacked : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("Stacked");

            //ribbonPanel.RowStackedItems(ribbonPanel.CreatePushButton<Commands.Command>(), ribbonPanel.CreatePushButton<Commands.Command>());

            //ribbonPanel.AddStackedItems(ribbonPanel.NewPushButtonData<Commands.Command>(), ribbonPanel.NewPushButtonData<Commands.Command>());

            //ribbonPanel.RowStackedItems(ribbonPanel.CreatePushButton<Commands.Command>(), ribbonPanel.CreatePushButton<Commands.Command>(), ribbonPanel.CreatePushButton<Commands.Command>());

            //ribbonPanel.AddStackedItems(ribbonPanel.NewPushButtonData<Commands.Command>(), ribbonPanel.NewPushButtonData<Commands.Command>(), ribbonPanel.NewPushButtonData<Commands.Command>());

            RibbonItem[] CreateButtons(int number)
            {
                var itens = Enumerable.Range(1, number).Select(i =>
                    ribbonPanel.CreatePushButton<Commands.Command>($"{i}")
                        .SetToolTip($"{i}")
                        .SetShowText(false)
                        .SetItemSize()
                        .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico")
                );
                return itens.ToArray();
            }
            ribbonPanel.FlowStackedItems(CreateButtons(9));
            ribbonPanel.AddSeparator();
            ribbonPanel.RowStackedItems(CreateButtons(9));

            ribbonPanel.AddSlideOut();
            ribbonPanel.RowStackedItems(CreateButtons(8));
            ribbonPanel.AddSeparator();
            ribbonPanel.RowStackedItems(CreateButtons(7));
            ribbonPanel.AddSeparator();
            ribbonPanel.RowStackedItems(CreateButtons(6));
            ribbonPanel.AddSeparator();
            ribbonPanel.RowStackedItems(CreateButtons(5));
            ribbonPanel.AddSeparator();
            ribbonPanel.RowStackedItems(CreateButtons(4));
            ribbonPanel.AddSeparator();
            ribbonPanel.RowStackedItems(CreateButtons(3));
            ribbonPanel.AddSeparator();
            ribbonPanel.RowStackedItems(CreateButtons(2));
            ribbonPanel.AddSeparator();
            ribbonPanel.RowStackedItems(CreateButtons(1));

            ribbonPanel.AddSeparator();
            ribbonPanel.RowStackedItems(
                ribbonPanel.CreatePushButton<Commands.Command>(),
                ribbonPanel.CreateTextBox().SetShowImageAsButton().SetWidth(100),
                ribbonPanel.CreateTextBox().SetShowImageAsButton().SetWidth(100));



            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove(true);
            return Result.Succeeded;
        }
    }

}
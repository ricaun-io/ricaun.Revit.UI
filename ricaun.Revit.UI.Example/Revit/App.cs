using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;

namespace ricaun.Revit.UI.Example.Revit
{
    [Console]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("Example");
            ribbonPanel.AddPushButton<Commands.Command>();

            ribbonPanel.CreatePulldownButton(new[] {
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>() });

            ribbonPanel.CreateSplitButton(new[] {
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>() });

            foreach (var item in ribbonPanel.GetItems())
            {
                if (item is PushButton pushButton)
                {
                    pushButton.LargeImage = Proprieties.Resource.icon.GetBitmapSource();
                    pushButton.Image = Proprieties.Resource.icon.GetBitmapSource().Scale(0.5);
                    pushButton.ToolTipImage = Proprieties.Resource.icon.GetBitmapSource().Scale(2);
                }
                if (item is PulldownButton pulldownButton)
                {
                    pulldownButton.LargeImage = Proprieties.Resource.icon.GetBitmapSource();
                    pulldownButton.Image = Proprieties.Resource.icon.GetBitmapSource().Scale(0.5);
                    pulldownButton.ToolTipImage = Proprieties.Resource.icon.GetBitmapSource().Scale(2);
                }
                if (item is SplitButton splitButton)
                {
                    foreach (var i in splitButton.GetItems())
                    {
                        if (i is PushButton pb)
                        {
                            pb.LargeImage = Proprieties.Resource.icon.GetBitmapSource();
                            pb.Image = Proprieties.Resource.icon.GetBitmapSource().Scale(0.5);
                            pb.ToolTipImage = Proprieties.Resource.icon.GetBitmapSource().Scale(2);
                        }
                    }
                }
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel.Close();
            return Result.Succeeded;
        }
    }

    internal class ConsoleAttribute : Attribute
    {
    }
}
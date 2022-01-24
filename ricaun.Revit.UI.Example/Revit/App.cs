using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ricaun.Revit.UI.Example.Revit
{
    [Console]
    public class App : IExternalApplication
    {
        private const string TabName = "ricaun";
        private const string PanelName = "Example";
        private static RibbonPanel ribbonPanel;

        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel(TabName, PanelName);
            var button = ribbonPanel.AddPushButton<Commands.Command>();

            ribbonPanel.CreatePulldownButton("PulldownButton", new[] {
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>() });

            ribbonPanel.CreateSplitButton("SplitButton", new[] {
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>() });

            ribbonPanel.AddPushButton<Commands.Command>("");

            var items = ribbonPanel.AddStackedItems(
                ribbonPanel.NewPushButtonData<Commands.Command>("Item1"),
                ribbonPanel.NewPushButtonData<Commands.Command>("Item2"));

            foreach (var item in items)
            {
                var ri = item.GetRibbonItem();
                ri.ShowText = false;
                ri.Size = Autodesk.Windows.RibbonItemSize.Large;
            }

            var item3s = ribbonPanel.AddStackedItems(
                ribbonPanel.NewPushButtonData<Commands.Command>("1"),
                ribbonPanel.NewPushButtonData<Commands.Command>("2"),
                ribbonPanel.NewPushButtonData<Commands.Command>("3"));

            foreach (var item in item3s)
            {
                var ri = item.GetRibbonItem();
                ri.ShowText = false;
            }

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

            button.SetDescription(new RibbonDescription()
            {
                Text = "-",
                LongDescription = "LongDescription",
                ToolTip = "ToolTip",
                LargeImage = Proprieties.Resource.icon.GetBitmapSource()
            });

            ribbonPanel.GetRibbonPanel().Tab.SetOrderPanels();
            var ric = ribbonPanel.GetRibbonPanel().Tab.Panels.ToList().FirstOrDefault(e => e.Source.Title == "ricaun");
            ric?.MoveRibbonPanel();
            ric?.MoveRibbonPanel();

            foreach (var item in ribbonPanel.GetRibbonItems())
            {
                //Console.WriteLine($"{item} {item.Name}");
            }

            ribbonPanel.UpdateRibbonDescription(setting =>
            {
                setting.Add<Commands.Command>(
                    new RibbonDescription()
                    {
                        Text = "Hello",
                        ToolTip = "This is a Tool Tip",
                        LongDescription = "This is a Long Description",
                        Image = Proprieties.Resource.icon.GetBitmapSource().Scale(0.5),
                        LargeImage = Proprieties.Resource.icon.GetBitmapSource(),
                        Help = "https://ricaun.com"
                    },
                    new RibbonDescription(LanguageType.Brazilian_Portuguese)
                    {
                        Text = "Ola",
                        ToolTip = "Este é um Tool Tip",
                        LongDescription = "Este é um Long Description",
                    }
                );

                setting.Add("PulldownButton", new RibbonDescription()
                {
                    Text = "PulldownButton",
                    Help = "https://ricaun.com"
                });

                setting.Add("SplitButton", new RibbonDescription()
                {
                    Text = "SplitButton",
                    Help = "https://ricaun.com"
                });
            });

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel.Close();
            return Result.Succeeded;
        }
    }
}
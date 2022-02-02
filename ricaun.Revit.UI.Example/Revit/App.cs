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
                ribbonPanel.NewPushButtonData<Commands.Command<int>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<double>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<bool>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<string>>()
            });

            ribbonPanel.CreateSplitButton("SplitButton", new[] {
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>(),
                ribbonPanel.NewPushButtonData<Commands.Command>()
            });

            var button2 = ribbonPanel.AddPushButton<Commands.Command>("");

            var items = ribbonPanel.AddStackedItems(
                ribbonPanel.NewPushButtonData<Commands.Command>("Item1"),
                ribbonPanel.NewPushButtonData<Commands.Command>("Item2"));

            foreach (var item in items)
            {
                item.SetItemSize();
                item.SetText();
            }

            var t = typeof(Commands.Command);

            ribbonPanel.NewPushButtonData(t, "1");

            var item3s = ribbonPanel.AddStackedItems(
                ribbonPanel.NewPushButtonData<Commands.Command>("1"),
                ribbonPanel.NewPushButtonData<Commands.Command>("2"),
                ribbonPanel.NewPushButtonData<Commands.Command>("3"));

            foreach (var item in item3s)
            {
                item.SetText();
                item.GetRibbonItem().AddQuickAccessToolBar();
            }

            OrderPanelAndMove(ribbonPanel);

            foreach (var item in ribbonPanel.GetRibbonItems())
            {
                //Console.WriteLine($"{item} {item.Name}");
            }

            ribbonPanel.UpdateRibbonDescription(setting =>
            {
                setting.AddDefault(
                    new RibbonDescription()
                    {
                        LargeImage = Proprieties.Resource.LargeImage.GetBitmapSource(),
                        Help = "https://ricaun.com"
                    }
                );

                setting.Add<Commands.Command>(
                    new RibbonDescription()
                    {
                        Text = "Hello",
                        ToolTip = "This is a Tool Tip",
                        LongDescription = "This is a Long Description",
                    },
                    new RibbonDescription(LanguageType.Brazilian_Portuguese)
                    {
                        Text = "Ola",
                        ToolTip = "Este é um Tool Tip",
                        LongDescription = "Este é um Long Description",
                    }
                );

                setting.Add<Commands.Command<int>>(
                    new RibbonDescription()
                    {
                        Text = "int",
                    }
                );

                setting.Add<Commands.Command<double>>(
                    new RibbonDescription()
                    {
                        Text = "double",
                        Action = (ribbonItem) =>
                        {
                            ribbonItem.SetLargeImage(Proprieties.Resource.LargeImage.GetBitmapSource().Scale(0.5));
                        }
                    }
                );

                setting.Add<Commands.Command<bool>>(
                    new RibbonDescription()
                    {
                        Text = "bool",
                        Action = (ribbonItem) =>
                        {
                            ribbonItem.SetShowImage();
                        }
                    }
                );

                setting.Add<Commands.Command<string>>(
                    new RibbonDescription()
                    {
                        Action = (ribbonItem) =>
                        {
                            ribbonItem.GetRibbonItem().LargeImage = null;
                        }
                    }
                );

                setting.Add("PulldownButton", new RibbonDescription()
                {
                    Text = "PulldownButton",
                });

                setting.Add("SplitButton", new RibbonDescription()
                {
                    Text = "SplitButton",
                });
            });

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel.Close();
            return Result.Succeeded;
        }

        private void OrderPanelAndMove(RibbonPanel ribbonPanel)
        {
            ribbonPanel.GetRibbonPanel().Tab.SetOrderPanels();
            var ric = ribbonPanel.GetRibbonPanel().Tab.Panels.ToList().FirstOrDefault(e => e.Source.Title == "ricaun");
            ric?.MoveRibbonPanel();
        }
    }
}
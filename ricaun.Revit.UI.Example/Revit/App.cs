using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System;
using System.Linq;
using ricaun.Revit.UI.Example.Proprieties;

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

            ribbonPanel.AddPushButton<Commands.Command>();

            ribbonPanel.CreatePulldownButton("T",
                ribbonPanel.NewPushButtonData<Commands.Command<Edge>>()
                    .SetText("1")
                    .SetToolTip("One")
                    .SetLongDescription("The One")
                    .SetLargeImage(Icons8.Document.Scale(0.5))
                    .SetToolTipImage(Icons8.Document),
                ribbonPanel.NewPushButtonData<Commands.Command<EdgeArray>>()
                    .SetText("2")
                    .SetLargeImage(Icons8.Document.Scale(0.5)),
                ribbonPanel.NewPushButtonData<Commands.Command<EdgeArrayArray>>()
                    .SetText("3")
                    .SetLargeImage(Icons8.Document.Scale(0.5)),
                ribbonPanel.NewPushButtonData<Commands.Command<EdgeArrayArrayIterator>>()
                    .SetText("4")
                    .SetLargeImage(Icons8.Document.Scale(0.5))
            )
            //.SetLargeImage(@"pack://application:,,,/UIFrameworkRes;component/Ribbon/images/system_electrical_circuit_power_create.ico".GetBitmapSource())
            .SetLargeImage(string.Format(@"https://ricaun.com/img/icon2.ico?teste={0}", DateTime.Now).GetBitmapSource())
            .SetToolTip("T")
            .GetRibbonItem().AddQuickAccessToolBar();

            ribbonPanel.CreatePulldownButton("PulldownButton",
                ribbonPanel.NewPushButtonData<Commands.Command<int>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<double>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<bool>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<string>>()
            );

            ribbonPanel.CreateSplitButton("SplitButton",
                ribbonPanel.NewPushButtonData<Commands.Command<int>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<double>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<bool>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<string>>()
            );

            ribbonPanel.AddPushButton<Commands.Command<Commands.Command>>();

            ribbonPanel.AddStackedItems(
                ribbonPanel.NewPushButtonData<Commands.Command<UIApplication>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<UIDocument>>());

            ribbonPanel.AddStackedItems(
                ribbonPanel.NewPushButtonData<Commands.Command<Application>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<Document>>());


            ribbonPanel.AddStackedItems(
                ribbonPanel.NewPushButtonData<Commands.Command<RibbonItem>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<RibbonButton>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<RibbonPanel>>());

            ribbonPanel.AddStackedItems(
                ribbonPanel.NewPushButtonData<Commands.Command<Element>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<ElementType>>(),
                ribbonPanel.NewPushButtonData<Commands.Command<ElementArray>>());

            OrderPanelAndMove(ribbonPanel);

            foreach (var item in ribbonPanel.GetRibbonItems())
            {
                //Console.WriteLine($"{item} {item.Name}");
            }

            UpdateRibbonDescription(ribbonPanel);

            return Result.Succeeded;
        }

        private void UpdateRibbonDescription(RibbonPanel ribbonPanel)
        {
            ribbonPanel.UpdateRibbonDescription(setting =>
            {
                setting.AddDefault(
                    new RibbonDescription()
                    {
                        Help = "https://ricaun.com"
                    }
                );

                setting.Add<Commands.Command>(
                    new RibbonDescription()
                    {
                        LargeImage = Resource.LargeImage.GetBitmapSource(),
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

                setting.Add<Commands.Command<Commands.Command>>(
                    new RibbonDescription()
                    {
                        Text = "Ok",
                        ToolTip = "This Button gonna be Removed",
                        LargeImage = Icons8.Ok,
                        Action = (ribbonItem) =>
                        {
                            var ri = ribbonItem.GetRibbonItem() as Autodesk.Windows.RibbonButton;
                            ri.MouseLeft += (s, e) => { ribbonPanel.Remove(ribbonItem); };
                        }
                    }
                );

                setting.Add<Commands.Command<int>>(
                    new RibbonDescription()
                    {
                        Text = "int",
                        LargeImage = Icons8.Document
                    }
                );

                setting.Add<Commands.Command<double>>(
                    new RibbonDescription()
                    {
                        Text = "double",
                        LargeImage = Icons8.File
                    }
                );

                setting.Add<Commands.Command<bool>>(
                    new RibbonDescription()
                    {
                        Text = "bool",
                        LargeImage = Icons8.Support
                    }
                );

                setting.Add<Commands.Command<string>>(
                    new RibbonDescription()
                    {
                        Text = "text",
                        LargeImage = Icons8.Settings
                    }
                );

                setting.Add("PulldownButton", new RibbonDescription()
                {
                    Text = "Menu",
                    LargeImage = Icons8.About
                });


                setting.Add<Commands.Command<UIApplication>>(new RibbonDescription()
                {
                    Text = "UIApplication",
                    LargeImage = Icons8.Restart,
                    Action = (ribbonItem) =>
                    {
                        ribbonItem.SetItemSize();
                        ribbonItem.SetShowText();
                    }
                });

                setting.Add<Commands.Command<UIDocument>>(new RibbonDescription()
                {
                    Text = "UIDocument",
                    LargeImage = Icons8.Filter,
                    Action = (ribbonItem) =>
                    {
                        ribbonItem.SetItemSize();
                        ribbonItem.SetShowText();
                    }
                });

                setting.Add<Commands.Command<Application>>(new RibbonDescription()
                {
                    Text = "Application",
                    LargeImage = Icons8.Search,
                    Action = (ribbonItem) =>
                    {
                        ribbonItem.SetItemSize();
                        ribbonItem.SetShowText();
                    }
                });

                setting.Add<Commands.Command<Document>>(new RibbonDescription()
                {
                    Text = "Document",
                    LargeImage = Icons8.Trash,
                    Action = (ribbonItem) =>
                    {
                        ribbonItem.SetItemSize();
                        ribbonItem.SetShowText();
                    }
                });

                setting.Add<Commands.Command<RibbonItem>>(new RibbonDescription()
                {
                    Text = "RibbonItem",
                    LargeImage = Icons8.Home,
                    Action = (ribbonItem) =>
                    {
                        ribbonItem.GetRibbonItem().AddQuickAccessToolBar();
                        ribbonItem.SetShowText();
                    }
                });

                setting.Add<Commands.Command<RibbonButton>>(new RibbonDescription()
                {
                    Text = "RibbonButton",
                    LargeImage = Icons8.Menu,
                    Action = (ribbonItem) =>
                    {
                        ribbonItem.GetRibbonItem().AddQuickAccessToolBar();
                        ribbonItem.SetShowText();
                    }
                });

                setting.Add<Commands.Command<RibbonPanel>>(new RibbonDescription()
                {
                    Text = "RibbonPanel",
                    LargeImage = Icons8.Info,
                    Action = (ribbonItem) =>
                    {
                        ribbonItem.GetRibbonItem().AddQuickAccessToolBar();
                        ribbonItem.SetShowText();
                    }
                });

                setting.Add<Commands.Command<Element>>(new RibbonDescription()
                {
                    Text = "Element",
                    LargeImage = Icons8.Circled,
                    Action = (ribbonItem) =>
                    {
                        ribbonItem.SetShowText();
                    }
                });

                setting.Add<Commands.Command<ElementType>>(new RibbonDescription()
                {
                    Text = "ElementType",
                    LargeImage = Icons8.Checked,
                    Action = (ribbonItem) =>
                    {
                        ribbonItem.SetShowText();
                    }
                });

                setting.Add<Commands.Command<ElementArray>>(new RibbonDescription()
                {
                    Text = "ElementArray",
                    LargeImage = Icons8.Cancel,
                    Action = (ribbonItem) =>
                    {
                        ribbonItem.SetShowText();
                    }
                });

            });
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
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System;
using System.Linq;
using ricaun.Revit.UI.Example.Proprieties;
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

            ribbonPanel.AddPushButton<Commands.Command>();

            var down = ribbonPanel.CreatePulldownButton("T",
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
            .SetToolTip("T");

            ribbonPanel.Remove(down);

            ribbonPanel.AddPushButton<Commands.Command<Construction>>("-")
                .SetLargeImage(GetBase64LargeImage());

            ribbonPanel.AddPushButton<Commands.Command<Construction>>("-")
                .SetLargeImage(GetResourcesLargeImage());

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

            ribbonPanel.AddSlideOut();

            ribbonPanel.AddPushButton<Commands.Command<Point>>()
                .SetLargeImage(Pack.Power)
                .SetText("Power")
                .AddQuickAccessToolBar();

            ribbonPanel.AddPushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Data)
                    .SetText("Data")
                    .AddQuickAccessToolBar();

            ribbonPanel.AddPushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Communication)
                    .SetText("Communication")
                    .AddQuickAccessToolBar();

            ribbonPanel.AddPushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Alarm)
                    .SetText("Alarm")
                    .AddQuickAccessToolBar();

            ribbonPanel.AddPushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Nurce)
                    .SetText("Nurce")
                    .AddQuickAccessToolBar();

            ribbonPanel.AddPushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Security)
                    .SetText("Security")
                    .AddQuickAccessToolBar();

            ribbonPanel.AddPushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Telephone)
                    .SetText("Telephone")
                    .AddQuickAccessToolBar();


            OrderPanelAndMove(ribbonPanel);

            foreach (var item in ribbonPanel.GetRibbonItems())
            {
                //Console.WriteLine($"{item} {item.Name} {item.GetRibbonItem()}");
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
                        Text = "ricaun",
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
            ribbonPanel.Remove(true);
            return Result.Succeeded;
        }

        private void OrderPanelAndMove(RibbonPanel ribbonPanel)
        {
            ribbonPanel.GetRibbonPanel().Tab.SetOrderPanels();
            var ric = ribbonPanel.GetRibbonPanel().Tab.Panels.ToList().FirstOrDefault(e => e.Source.Title == "ricaun");
            ric?.MoveRibbonPanel();
        }

        private string GetResourcesLargeImage()
        {
            return string.Format("pack://application:,,,/{0};component/Resources/icon.png", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        private string GetBase64LargeImage()
        {
            const string LargeImage = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAACQAAAAkABgV+WigAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAPaSURBVFiFvZddbBRVGIaf+dnd2bp/dQu0paStho2kQVpNL0RDQ/WiCVZEozHGLImpvS0iBIxKkTRGEU29gVTlRgJBuRQ1xliLEGmDSa1EbWJtKy3VdmmVsmt3Z3/Gi+ngst2fmSHxTc7Fzvm+931nvzPnfEfAPHzANqAV2ATUAYHlub+BSeAHoB/4DLhhgbsoQsBxIAZoJkcM+BBYfzvCbuCI4FCSFoRzhwocBhSr4uuBy4BW0dGnBcPvaWKZ364JDbgIVJkVbwLmjOSKzuNa/SlNq33/muZr69IQJbsmpoB7c8XEPG/+JbBqRaAnSDDcy9qeSyj3bDH7MtmoQV+clYUMKMCZfOLZcNY1UXXgHGv2foq8qt6OibPo6wsAKWvyTWBHbkbZ/dtx1TWtYHJUhfA93Ino9pIYG0JLqWZNVANpYAD++wdCQJdZBgOC042/fR8174zibe0EIbeiBbGb5VIYGfsA2aoBA1J5NRUdfVQfGkQJbTaT4gEOGAZ8wDN2xbPhuruZqu4LrO76BDm4rlR4GPBKwBPFDDjXbcTd0GrehSDgrGnA29oBmQzqxPeQSeelBkYk9NrfV4gvPnqe+M8DuOoakQKVhcJW+pBduDc+gmfLTjLRedQrI/nCFiTgNWBtMbLUtd+50f8BqblxlNBmRMVj2ohY5ueO5h24N7SgTg6Tvj6bPZ0UgAhQYZpQ8eDftgf/Y/sRHC7TRgDQMkQvnGTh5B7Si3MAcxLQw637QXGOlEr8lwFigx8jBSpx1jSYNyAIOGs34d3aAYA6fkk2/eHm5XO6SweVgAwsYqUEZQEC2/fja9t1GyV4ifRiBOC6DEyYMiCIeB56jjufPYzkX2NNGFj6qZ+FEy+iXvkx+/G4jN5GNRdLVja0EAz34qxttCycnP2Nv06/TGzoTL7pERm9h3uhEEHg8Vcof7rHsjDA0uWvmD3SjpZMFAr5WkQ/HmOFIuTVd9kSB0jNTxUTjwFfiEAUOG1bxT5OAVHjM3wLSP6P4ip6/3HzOP4V6LXKoqlLaOqSHQPvAuPZBkA/nwfNMvwzfJbpvQ1M7w4RPX8CNM1s6kXgoPEj20AcvSWbKpatTg7zx6EWZt9uJxWZILUwTeRYmJnuB0iMlfQ/AzwF3FyZuVvxn8CjwHRuZiY6z/xHu7j6ajPx0W9XMCfGhpjpfpDIsZ25J56BKaANuFrKJeid8TlsXkxExaOVP3lQCz5/1Hj2HTntuBm4gNcFhxI1K5w7BIeSAN5Y5rKNSuAo+n5hVjwK9AEldzHBghEP+vV8K9AI1HPr9XwCGAa+AT5fNlES/wLKWXKx84783QAAAABJRU5ErkJggg==";
            return LargeImage;
        }
    }
}
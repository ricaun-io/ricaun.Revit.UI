using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using ricaun.Revit.UI.Example.Proprieties;
using ricaun.Revit.UI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ricaun.Revit.UI.Example.Revit
{
    [AppLoader]
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

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }


    [Console]
    public class App : IExternalApplication
    {
        private const string TabName = "ricaun";
        private const string PanelName = "Example";
        private static RibbonPanel ribbonPanel;

        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel(TabName, PanelName);

            ribbonPanel.CreatePushButton<Commands.CommandWithAvailability>("Revit")
                .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");

            var button = ribbonPanel.CreatePushButton<Commands.Command>();

            var ri = button.GetRibbonItem() as Autodesk.Windows.RibbonButton;

            Models.TestViewModel.TestModel.CommandTest = new Views.RelayCommand(() =>
            {
                Models.TestViewModel.TestModel.Text += ".";
            },
            () =>
            {
                return Autodesk.Windows.ComponentManager.IsApplicationFrameEnabled && UIFramework.ControlHelper.IsEnabled(ri);
            });

            Models.TestViewModel.TestModel.CommandTest2 = new Views.RelayCommand(() =>
            {
                Models.TestViewModel.TestModel.Text = "";
            },
            () =>
            {
                return Autodesk.Windows.ComponentManager.IsApplicationFrameEnabled && UIFramework.ControlHelper.IsEnabled(ri);
            });

            ribbonPanel.CreatePushButton<Commands.Command<Construction>>("-")
                .SetLargeImage(GetBase64LargeImage());

            ribbonPanel.CreatePushButton<Commands.Command<Construction>>("-")
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

            ribbonPanel.CreatePushButton<Commands.Command<Commands.Command>>();

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

            #region PulldownButton Remove
            var pulldown = ribbonPanel.CreatePulldownButton("T",
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

            ribbonPanel.Remove(pulldown);
            #endregion

            #region TextBox

            var textBox = ribbonPanel.CreateTextBox("hi")
                .SetSelectTextOnFocus()
                .SetShowImageAsButton()
                .SetPromptText("Search")
                .SetValue("Search")
                .SetImage(Icons8.Search);

            textBox.EnterPressed += (s, e) =>
            {
                System.Windows.MessageBox.Show(textBox.Value.ToString());
            };

            ribbonPanel.AddStackedItems(
                ribbonPanel.NewTextBoxData("T1")
                    .SetImage(Icons8.Search),
                ribbonPanel.NewTextBoxData("T2")
                    .SetImage(Icons8.Search)
                );

            #endregion

            #region CreateRadioButtonGroup
            var radio = ribbonPanel.CreateRadioButtonGroup("Radio",
                ribbonPanel.NewToggleButtonData("R1")
                    .SetLargeImage(Icons8.Circled),
                ribbonPanel.NewToggleButtonData("R2")
                    .SetLargeImage(Icons8.Checked),
                ribbonPanel.NewToggleButtonData("R3")
                    .SetLargeImage(Icons8.Cancel),
                ribbonPanel.NewToggleButtonData<Commands.Command<ToggleButtonData>>()
                    .SetText("R4")
                    .SetLargeImage(Icons8.Trash)
            );

            radio.AddToggleButtons(
                ribbonPanel.NewToggleButtonData("R5")
                .SetLargeImage(Icons8.About)
                );
            #endregion

            #region ComboBox

            var comboBox = ribbonPanel.CreateComboBox("ComboBox",
                ribbonPanel.NewComboBoxMemberData("C1")
                    .SetImage(Icons8.Restart),
                ribbonPanel.NewComboBoxMemberData("C2")
                    .SetImage(Icons8.Restart),
                ribbonPanel.NewComboBoxMemberData("C3")
                    .SetImage(Icons8.Restart)
                );

            comboBox.CurrentChanged += (s, e) =>
            {
                System.Windows.MessageBox.Show(comboBox.Current.Name);
            };


            ribbonPanel.AddStackedItems(
                ribbonPanel.NewComboBoxData("A"),
                ribbonPanel.NewComboBoxData("B"),
                ribbonPanel.NewComboBoxData("C"));
            #endregion

            #region Autodesk Icons Buttons
            ribbonPanel.CreatePushButton<Commands.Command<Point>>()
                .SetLargeImage(Pack.Power)
                .SetText("Power")
                .AddQuickAccessToolBar();

            ribbonPanel.CreatePushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Data)
                    .SetText("Data")
                    .AddQuickAccessToolBar();

            ribbonPanel.CreatePushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Communication)
                    .SetText("Communication")
                    .AddQuickAccessToolBar();

            ribbonPanel.CreatePushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Alarm)
                    .SetText("Alarm")
                    .AddQuickAccessToolBar();

            ribbonPanel.CreatePushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Nurce)
                    .SetText("Nurce")
                    .AddQuickAccessToolBar();

            ribbonPanel.CreatePushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Security)
                    .SetText("Security")
                    .AddQuickAccessToolBar();

            ribbonPanel.CreatePushButton<Commands.Command<Point>>()
                    .SetLargeImage(Pack.Telephone)
                    .SetText("Telephone")
                    .AddQuickAccessToolBar();

            var sw = ribbonPanel.CreatePushButton<Commands.Command<Point>, Commands.Availability.AvailableOnAnyDocument>()
                    .SetLargeImage(Pack.Switch)
                    .SetText("Switch")
                    .AddQuickAccessToolBar();
            #endregion

            OrderPanelAndMove(ribbonPanel);

            foreach (var item in ribbonPanel.GetRibbonItems())
            {
                //Console.WriteLine($"{item} {item.Name} {item.GetRibbonItem()}");
            }

            UpdateRibbonDescription(ribbonPanel);
            AddNewPanelToMove(application);

            return Result.Succeeded;
        }

        private static RibbonPanel ribbonPanelMove;
        private void AddNewPanelToMove(UIControlledApplication application)
        {
            ribbonPanelMove = application.CreateOrSelectPanel(TabName, PanelName + "0");
            var button = ribbonPanelMove.CreatePushButton<Commands.Command>("Teste")
                .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");

            var splitButtonWithButton = ribbonPanelMove.CreateSplitButton(
                ribbonPanelMove.NewPushButtonData<Commands.Command>("Split"),
                ribbonPanelMove.NewPushButtonData<Commands.Command>("Split"),
                ribbonPanelMove.NewPushButtonData<Commands.Command>("Split"),
                ribbonPanelMove.NewPushButtonData<Commands.Command>(),
                ribbonPanelMove.NewPushButtonData<Commands.Command>());

            var splitButton = ribbonPanelMove.CreateSplitButton();
            var split = ribbonPanelMove.CreateSplitButton("Split");

            for (int i = 0; i < 5; i++)
            {
                split.CreatePushButton<Commands.Command>()
                    .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");
                split.CreatePushButton<Commands.Command, Commands.Availability.AvailableOnAnyDocument>()
                    .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");
            }




            //Console.WriteLine($">> {split}");

            //var ribbonItem = button.GetRibbonItem();
            //var modifyName = "Test";

            ////RibbonModifyUtils.CreatePanel(modifyName, button);
            //RibbonModifyUtils.CreatePanel(modifyName,
            //     ribbonItem,
            //     ribbonItem.CreateCopy(),
            //     ribbonItem.CreateCopy((i) => { i.Text = ""; }),
            //     ribbonItem.CreateCopy((i) => { i.Size = Autodesk.Windows.RibbonItemSize.Standard; }));

            //var task = Task.Run(async () =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        await Task.Delay(1000);
            //    }
            //    UIFramework.RevitRibbonControl.RibbonControl.Dispatcher.Invoke(() =>
            //    {
            //        //RibbonModifyUtils.RemovePanel(modifyName);
            //        RibbonModifyUtils.RemovePanel(modifyName);
            //    });
            //});

            var task = Task.Run(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(1000);
                    ribbonPanelMove.Title = $"{PanelName} {i}";
                }
                UIFramework.RevitRibbonControl.RibbonControl.Dispatcher.Invoke(() =>
                {
                    ribbonPanelMove.MoveToRibbonTab("Add-Ins");
                });
            });


            ribbonPanelMove.MoveToRibbonTab("Modify");
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

                //setting.Add<Commands.Command>(
                //    new RibbonDescription()
                //    {
                //        LargeImage = Resource.LargeImage.GetBitmapSource(),
                //        Text = "ricaun",
                //        ToolTip = "This is a Tool Tip",
                //        LongDescription = "This is a Long Description",
                //    },
                //    new RibbonDescription(LanguageType.Brazilian_Portuguese)
                //    {
                //        Text = "Ola",
                //        ToolTip = "Este é um Tool Tip",
                //        LongDescription = "Este é um Long Description",
                //    }
                //);

                setting.Add<Commands.Command>(
                    (ribbon) =>
                    {
                        ribbon.LargeImage = Resource.LargeImage.GetBitmapSource();
                        ribbon.Text = "ricaun";
                        ribbon.ToolTip = "This is a Tool Tip";
                        ribbon.LongDescription = "This is a Long Description";
                    },
                    (ribbon) =>
                    {
                        ribbon.LanguageType = LanguageType.Brazilian_Portuguese;
                        ribbon.Text = "Ola";
                        ribbon.ToolTip = "Este é um Tool Tip";
                        ribbon.LongDescription = "Este é um Long Description";
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

                setting.Add("A", new RibbonDescription()
                {
                    Text = "A",
                    Action = (ribbonItem) =>
                    {
                        if (ribbonItem is ComboBox combo)
                        {
                            combo.AddItems(
                               ribbonPanel.NewComboBoxMemberData("1"),
                               ribbonPanel.NewComboBoxMemberData("2"),
                               ribbonPanel.NewComboBoxMemberData("3"),
                               ribbonPanel.NewComboBoxMemberData("4"),
                               ribbonPanel.NewComboBoxMemberData("5"),
                               ribbonPanel.NewComboBoxMemberData("6")
                               );
                        }
                    }
                });

                setting.Add("B", new RibbonDescription()
                {
                    Text = "B",
                    Action = (ribbonItem) =>
                    {
                        if (ribbonItem is ComboBox combo)
                        {
                            combo.AddItems(
                                ribbonPanel.NewComboBoxMemberData("1")
                                    .SetText("One")
                                    .SetImage(Icons8.Document)
                                    .SetToolTip("One")
                                    .SetToolTipImage(Icons8.Document)
                                    .SetLongDescription("One"),
                                ribbonPanel.NewComboBoxMemberData("2")
                                    .SetGroupName("G1")
                                    .SetImage(Icons8.Document),
                                ribbonPanel.NewComboBoxMemberData("3")
                                    .SetGroupName("G2")
                                    .SetImage(Icons8.Document)
                            );
                        }
                    }
                });

                setting.Add("C", new RibbonDescription()
                {
                    Text = "C",
                    Action = (ribbonItem) =>
                    {
                        if (ribbonItem is ComboBox combo)
                        {
                            combo.AddItems(
                                ribbonPanel.NewComboBoxMemberData("A"),
                                ribbonPanel.NewComboBoxMemberData("B"),
                                ribbonPanel.NewComboBoxMemberData("C"),
                                ribbonPanel.NewComboBoxMemberData("D"),
                                ribbonPanel.NewComboBoxMemberData("E"),
                                ribbonPanel.NewComboBoxMemberData("F")
                            );
                        }
                    }
                });

                setting.Add("T1", new RibbonDescription()
                {
                    ToolTip = "TextBox1",
                    LongDescription = "TextBox1",
                    Action = (ribbonItem) =>
                    {
                        if (ribbonItem is TextBox box)
                        {
                            box.SetPromptText("T1");
                        }
                    }
                });

                setting.Add("T2", new RibbonDescription()
                {
                    ToolTip = "TextBox2",
                    LongDescription = "TextBox2",
                    Action = (ribbonItem) =>
                    {
                        if (ribbonItem is TextBox box)
                        {
                            box.SetPromptText("T2");
                        }
                    }
                });

            });
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanelMove?.Remove(true);
            ribbonPanel?.Remove(true);
            return Result.Succeeded;
        }

        private void OrderPanelAndMove(RibbonPanel ribbonPanel)
        {
            ribbonPanel.GetRibbonTab().SetPanelsOrderByTitle();
            var ric = ribbonPanel.GetRibbonTab().Panels.FirstOrDefault(e => e.Source.Title == "ricaun");
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
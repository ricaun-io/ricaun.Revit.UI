using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Threading.Tasks;

namespace ricaun.Revit.UI.Example.Revit
{
    [Console]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("Example");
            var button = ribbonPanel.AddPushButton<Commands.Command>();

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

            ribbonPanel.AddSlideOut();

            AddRadioGroup(ribbonPanel);
            AddStackedButtons(ribbonPanel);

            foreach (var item in ribbonPanel.GetRibbonItems())
            {
                Console.WriteLine($"{item} {item.Name}");
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel.Close();
            return Result.Succeeded;
        }

        private void AddRadioGroup(RibbonPanel panel)
        {
            // add radio button group
            RadioButtonGroupData radioData = new RadioButtonGroupData("radioGroup");
            RadioButtonGroup radioButtonGroup = panel.AddItem(radioData) as RadioButtonGroup;

            var commandType = typeof(Commands.Command);
            var targetName = commandType.Name;
            var targetText = commandType.Name;
            var assemblyName = commandType.Assembly.Location;
            var className = commandType.FullName;

            // create toggle buttons and add to radio button group
            ToggleButtonData tb1 = new ToggleButtonData("toggleButton1", "Red", assemblyName, className);
            ToggleButtonData tb2 = new ToggleButtonData("toggleButton2", "Green");
            ToggleButtonData tb3 = new ToggleButtonData("toggleButton3", "Blue");
            radioButtonGroup.AddItem(tb1);
            radioButtonGroup.AddItem(tb2);
            radioButtonGroup.AddItem(tb3);
        }

        private void AddStackedButtons(RibbonPanel panel)
        {
            ComboBoxData cbData = new ComboBoxData("comboBox");

            TextBoxData textData = new TextBoxData("Text Box");
            textData.Name = "Text Box";
            textData.ToolTip = "Enter some text here";
            textData.LongDescription = "This is text that will appear next to the image"
                    + "when the user hovers the mouse over the control";

            var stackedItems = panel.AddStackedItems(textData, cbData);
            if (stackedItems.Count > 1)
            {
                TextBox tBox = stackedItems[0] as TextBox;
                if (tBox != null)
                {
                    tBox.PromptText = "Enter a comment";
                    tBox.ShowImageAsButton = true;
                    tBox.ToolTip = "Enter some text";
                    // Register event handler ProcessText
                    tBox.EnterPressed += (s, e) =>
                    {
                        TextBox textBox = s as TextBox;
                        string strText = textBox.Value as string;
                        Console.WriteLine(strText);
                    };
                }

                ComboBox cBox = stackedItems[1] as ComboBox;
                if (cBox != null)
                {
                    cBox.ItemText = "ComboBox";
                    cBox.ToolTip = "Select an Option";
                    cBox.LongDescription = "Select a number or letter";

                    ComboBoxMemberData cboxMemDataA = new ComboBoxMemberData("A", "Option A");

                    cboxMemDataA.GroupName = "Letters";
                    cBox.AddItem(cboxMemDataA);

                    ComboBoxMemberData cboxMemDataB = new ComboBoxMemberData("B", "Option B");

                    cboxMemDataB.GroupName = "Letters";
                    cBox.AddItem(cboxMemDataB);

                    ComboBoxMemberData cboxMemData = new ComboBoxMemberData("One", "Option 1");

                    cboxMemData.GroupName = "Numbers";
                    cBox.AddItem(cboxMemData);
                    ComboBoxMemberData cboxMemData2 = new ComboBoxMemberData("Two", "Option 2");

                    cboxMemData2.GroupName = "Numbers";
                    cBox.AddItem(cboxMemData2);
                    ComboBoxMemberData cboxMemData3 = new ComboBoxMemberData("Three", "Option 3");

                    cboxMemData3.GroupName = "Numbers";
                    cBox.AddItem(cboxMemData3);
                }
            }
        }

    }
}
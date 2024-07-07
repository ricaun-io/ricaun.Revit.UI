using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using ricaun.Revit.UI.Example.Proprieties;
using ricaun.Revit.UI.Utils;
using System.Linq;

namespace ricaun.Revit.UI.Example.Revit
{
    //[AppLoader]
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
                        .SetToolTip(Icons.Icon.ToString())
                        .SetLargeImage(Icons.Icon)
                );
                return itens.ToArray();
            }

            ribbonPanel.FlowStackedItems(CreateButtons(9));
            ribbonPanel.AddSeparator();
            ribbonPanel.RowStackedItems(CreateButtons(9));

            ribbonPanel.AddSeparator();
            ribbonPanel.RowLargeStackedItems(CreateButtons(4));

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
                ribbonPanel.CreatePushButton<Commands.Command>("StackedItems"),
                ribbonPanel.CreateTextBox().AddEnterPressed(AppStacked_EnterPressed).SetShowImageAsButton().SetWidth(100),
                ribbonPanel.CreateTextBox().AddEnterPressed(AppStacked_EnterPressedNull).SetShowImageAsButton().SetWidth(100)
                );

            var textBox = ribbonPanel.CreateTextBox().SetShowImageAsButton().SetWidth(100);
            textBox.Enabled = false;
            TextBox = textBox;
            var comboBox1 = ribbonPanel.CreateComboBox(ribbonPanel.NewComboBoxMemberData("1"), ribbonPanel.NewComboBoxMemberData("2"), ribbonPanel.NewComboBoxMemberData("3"))
                .SetWidth(100)
                .AddCurrentChanged(ComboBox_CurrentChanged);

            var comboBoxA = ribbonPanel.CreateComboBox(ribbonPanel.NewComboBoxMemberData("A"), ribbonPanel.NewComboBoxMemberData("B"), ribbonPanel.NewComboBoxMemberData("C"))
                .SetWidth(100)
                .AddCurrentChanged(ComboBox_CurrentChanged);

            comboBoxA.SetCurrent(comboBoxA.GetItems().Last());

            ribbonPanel.AddSeparator();
            ribbonPanel.FlowStackedItems(textBox, comboBox1, comboBoxA);

            return Result.Succeeded;
        }

        static TextBox TextBox;

        private void ComboBox_CurrentChanged(object sender, Autodesk.Revit.UI.Events.ComboBoxCurrentChangedEventArgs e)
        {
            var comboBox = sender as Autodesk.Revit.UI.ComboBox;
            TextBox.Value = comboBox.Current.Name;
        }

        private static void AppStacked_EnterPressed(object sender, Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs e)
        {
            var textBox = sender as Autodesk.Revit.UI.TextBox;
            System.Console.WriteLine(textBox.Value);
        }

        private static void AppStacked_EnterPressedNull(object sender, Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs e)
        {
            var textBox = sender as Autodesk.Revit.UI.TextBox;
            System.Console.WriteLine(textBox.Value);
            textBox.Value = null;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove(true);
            return Result.Succeeded;
        }
    }

}
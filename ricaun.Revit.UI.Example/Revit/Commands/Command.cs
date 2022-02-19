using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ricaun.Revit.UI.Example.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            new Views.TestView().Show();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class Command<T> : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            var t = typeof(T);

            System.Windows.MessageBox.Show($"Hello Revit\n{DateTime.Now}\n{t}");

            return Result.Succeeded;
        }
    }
}

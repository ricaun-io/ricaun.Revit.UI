using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace ricaun.Revit.UI.Example.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            System.Windows.MessageBox.Show(AutodeskExtension.GetAutodeskOwner(), $"Hello Revit\n{DateTime.Now}");

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
            Console.WriteLine(t.GetName());
            Console.WriteLine(this.GetType().GetName());
            Console.WriteLine(this.GetType());

            System.Windows.MessageBox.Show(AutodeskExtension.GetAutodeskOwner(), $"Hello Revit\n{DateTime.Now}\n{t}");

            return Result.Succeeded;
        }
    }
}

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

            System.Windows.MessageBox.Show(AutodeskExtension.GetAutodeskOwner(), $"Hello Revit\n{DateTime.Now}\n{t}");

            return Result.Succeeded;
        }
    }
}

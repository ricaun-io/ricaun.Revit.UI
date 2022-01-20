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

            //new Services.ReflectionService().GetFields<RibbonPanel>();
            Console.WriteLine(typeof(Command).Assembly);
            //new Services.ReflectionService().GetMethods<RibbonPanel>();


            System.Windows.MessageBox.Show(AutodeskExtension.GetAutodeskOwner(), $"Hello Revit\n{DateTime.Now}");

            return Result.Succeeded;
        }
    }
}

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
        static Views.TestView testView;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            if (testView == null)
            {
                testView = new Views.TestView();
                testView.Show();
                testView.Closed += (s, e) => { testView = null; };
            }
            testView.Activate();

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


    [Transaction(TransactionMode.Manual)]
    public class CommandWithAvailability : IExternalCommand, IExternalCommandAvailability
    {
        private static bool available = true;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;
            available = false;
            return Result.Succeeded;
        }

        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return available;
        }
    }

}

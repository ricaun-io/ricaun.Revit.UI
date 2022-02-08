using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ricaun.Revit.UI.Example.Revit.Commands.Availability
{
    /// <summary>
    /// The Command will be available on Project and Family environment
    /// </summary>
    public class AvailableOnAnyDocument : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication uiapp, CategorySet categorySet)
        {
            if (uiapp.ActiveUIDocument == null) return false;
            if (uiapp.ActiveUIDocument.Document == null) return false;

            return true;
        }
    }
}
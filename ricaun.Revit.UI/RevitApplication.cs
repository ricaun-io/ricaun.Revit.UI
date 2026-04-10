using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// Provides utility methods and properties for interacting with the Revit application.
    /// </summary>
    public static class RevitApplication
    {
        /// <summary>
        /// Gets the current <see cref="UIApplication"/> instance.
        /// </summary>
        public static UIApplication UIApplication => new RibbonItemEventArgs().Application;
        /// <summary>
        /// Gets the current <see cref="UIControlledApplication"/> instance.
        /// </summary>
        public static UIControlledApplication UIControlledApplication => GetUIControlledApplication(UIApplication);
        /// <summary>
        /// Gets a value indicating whether the current context is within an add-in.
        /// </summary>
        public static bool IsInAddInContext => InAddInEventContext(UIApplication);

        #region Private
        /// <summary>
        /// Get <see cref="Autodesk.Revit.UI.UIControlledApplication"/> using the <paramref name="application"/>
        /// </summary>
        /// <param name="application">Revit UIControlledApplication</param>
        private static UIControlledApplication GetUIControlledApplication(UIApplication application)
        {
            var type = typeof(UIControlledApplication);

            var constructor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { application.GetType() }, null);

            return constructor?.Invoke(new object[] { application }) as UIControlledApplication;
        }
        private static bool InAddInEventContext(UIApplication uiapp)
        {
            try
            {
                uiapp.Idling += Application_Idling;
                uiapp.Idling -= Application_Idling;
                return true;
            }
            catch { } // Invalid call to Revit API! Revit is currently not within an API context.
            return false;
        }
        static void Application_Idling(object sender, IdlingEventArgs e) { }
        [Obsolete("This fails to work in Revit 2027, the Addin 'Revit' with id 'e42ff806-491d-4b17-9afb-ea051d5ebb76'.")]
        private static bool InAddInContext(UIApplication uiapp)
        {
            // ActiveAddInId is only available when Revit is within an API context.
            return uiapp.ActiveAddInId is not null;
        }
        #endregion
    }
}

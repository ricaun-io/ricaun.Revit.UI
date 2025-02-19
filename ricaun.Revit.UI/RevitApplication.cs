using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;
using System.Reflection;

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
        public static bool IsInAddInContext => InAddInContext(UIApplication);

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
        private static bool InAddInContext(UIApplication uiapp)
        {
            // ActiveAddInId is only available when Revit is within an API context.
            return uiapp.ActiveAddInId is not null;
        }
        #endregion
    }
}

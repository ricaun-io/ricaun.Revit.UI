using Autodesk.Revit.ApplicationServices;
using System.Reflection;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// Provides methods to determine if the current release of Revit is a preview release.
    /// </summary>
    public static class RevitApplicationPreview
    {
        /// <summary>
        /// Gets a value indicating whether the current release is a preview release.
        /// </summary>
        public static bool IsPreviewRelease => ApplicationIsPreviewRelease();
        /// <summary>
        /// Gets a value indicating whether the current release is a preview release and the user is not logged in.
        /// </summary>
        public static bool IsPreviewReleaseNotLoggedIn => IsPreviewRelease && !Application.IsLoggedIn;
        /// <summary>
        /// Determines if the current release of Revit is a preview release.
        /// </summary>
        /// <returns>True if the current release is a preview release; otherwise, false.</returns>
        /// <remarks><see cref="Application"/> have an internal static IsPreviewRelease method.</remarks>
        private static bool ApplicationIsPreviewRelease()
        {
            var type = typeof(Application);
            var method = type.GetMethod(nameof(IsPreviewRelease), BindingFlags.Static | BindingFlags.NonPublic);

            if (method is null)
                return false;

            return (bool)method.Invoke(null, null);
        }
    }
}

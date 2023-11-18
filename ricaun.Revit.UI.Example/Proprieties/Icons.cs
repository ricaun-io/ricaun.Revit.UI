using ricaun.Revit.UI;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace ricaun.Revit.UI.Example.Proprieties
{
    public static class Icons
    {
        /// <summary>
        /// Get Icon
        /// </summary>
        public static BitmapSource Icon => GetIcon().GetBitmapSource();
        public static string[] IconResources { get; } = GetIcons();
        private static int IndexResource = 0;
        private static string GetIcon()
        {
            var icon = IconResources[IndexResource];
            IndexResource = (IndexResource + 1) % IconResources.Length;
            return icon;
        }
        private static string AssemblyName => "UIFrameworkRes";
        private static string[] GetIcons()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(e => e.GetName().Name.Equals(AssemblyName, StringComparison.InvariantCultureIgnoreCase));

            return GetResourceNames(assembly)
                .Where(e => e.Contains("ribbon"))
                .Where(e => e.EndsWith(".ico"))
                .Select(e => $"/{AssemblyName};component/{e}")
                .ToArray();
        }
        /// <summary>
        /// GetResourceNames
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static string[] GetResourceNames(Assembly assembly)
        {
            string resName = assembly.GetName().Name + ".g.resources";
            using (var stream = assembly.GetManifestResourceStream(resName))
            using (var reader = new System.Resources.ResourceReader(stream))
            {
                var resources = reader.Cast<DictionaryEntry>().Select(entry => (string)entry.Key).OrderBy(e => e).ToArray();
                return resources;
            }
        }
    }
}

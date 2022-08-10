using System;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// TypeExtension
    /// </summary>
    internal static class TypeExtension
    {
        /// <summary>
        /// Get Name of the <paramref name="type"/> with GenericArguments
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static string GetName(this Type type)
        {
            var name = type.Name;
            if (type.IsGenericType)
            {
                foreach (var generic in type.GetGenericArguments())
                    name += $"[{generic}]";
            }
            return name;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ricaun.Revit.UI
{
    public static class TypeExtension
    {
        /// <summary>
        /// Get Name of the <paramref name="type"/> with GenericArguments
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetName(this Type type)
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

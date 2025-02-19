using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ricaun.Revit.UI.Utils
{
    internal static class StackTraceUtils
    {
        /// <summary>
        /// Check if assembly is repacked.
        /// </summary>
        internal static bool IsAssemblyRepack { get; } = GetAssemblyRepack();

        /// <summary>
        /// The current assembly does not have any resource and name starts with 'ricaun.Revit.UI' namespace.
        /// </summary>
        /// <returns></returns>
        private static bool GetAssemblyRepack()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var isAssemblyName = assembly.GetName().Name.StartsWith(typeof(AppLoaderAttribute).Namespace);
            if (!isAssemblyName) 
                return true;

            return assembly.GetManifestResourceNames().Length != 0;
        }

        /// <summary>
        /// GetCallingAssembly using StackTrace
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Assembly GetCallingAssembly()
        {
            var callingAssembly = Assembly.GetCallingAssembly();
            var executingAssembly = Assembly.GetExecutingAssembly();
            if (callingAssembly != executingAssembly) { return callingAssembly; }

            if (IsAssemblyRepack)
            {
                //Debug.WriteLine($"Assembly is repacked, return {callingAssembly}.");
                return callingAssembly;
            }

            const int skipFrames = 1; // Skip 'StackTraceUtils' method. 
            var stackFrames = new StackTrace(skipFrames).GetFrames();
            if (stackFrames == null) return null;

            foreach (var frame in stackFrames)
            {
                var assembly = frame.GetMethod().DeclaringType.Assembly;
                //System.Console.WriteLine($"[{frame.GetMethod().Name}] \t{assembly}");
                if (assembly != executingAssembly)
                {
                    callingAssembly = assembly;
                    break;
                }
            }
            return callingAssembly;
        }

        /// <summary>
        /// This method is for test if <see cref="GetCallingAssembly"/> is working with StackTrace.
        /// </summary>
        /// <returns></returns>
        internal static Assembly GetCallingAssemblyNested()
        {
            return GetCallingAssembly();
        }
    }
}

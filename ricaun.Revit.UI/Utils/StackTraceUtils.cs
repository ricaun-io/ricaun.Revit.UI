using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ricaun.Revit.UI.Utils
{
    internal static class StackTraceUtils
    {
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

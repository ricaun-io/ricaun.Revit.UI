using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ricaun.Revit.UI.Utils;

/// <summary>
/// Utility class for working with stack traces and assemblies.
/// </summary>
internal static class StackTraceUtils
{
    /// <summary>
    /// Checks if the specified assembly contains any manifest resources.
    /// </summary>
    /// <param name="assembly">The assembly to check.</param>
    /// <returns>True if the assembly contains manifest resources; otherwise, false.</returns>
    private static bool HasManifestResource(this Assembly assembly) => assembly.GetManifestResourceNames().Length != 0;

    /// <summary>
    /// Gets the assemblies that contain manifest resources.
    /// </summary>
    /// <returns>An enumerable of assemblies that contain manifest resources.</returns>
    public static IEnumerable<Assembly> GetResourceAssemblies()
    {
        var assembly = Assembly.GetExecutingAssembly();
        if (assembly.HasManifestResource())
            yield return assembly;

        var callingAssembly = GetCallingAssembly();
        if (callingAssembly.HasManifestResource())
            yield return callingAssembly;
    }

    /// <summary>
    /// Gets the calling assembly using the stack trace.
    /// </summary>
    /// <remarks>
    /// This method uses <see cref="StackTrace"/> to determine the calling assembly
    /// when <see cref="Assembly.GetCallingAssembly"/> returns the executing assembly.
    /// </remarks>
    /// <returns>The calling assembly, or null if it cannot be determined.</returns>
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
    /// Tests if <see cref="GetCallingAssembly"/> works correctly with <see cref="StackTrace"/>.
    /// </summary>
    /// <returns>The calling assembly determined by <see cref="GetCallingAssembly"/>.</returns>
    internal static Assembly GetCallingAssemblyNested()
    {
        return GetCallingAssembly();
    }
}

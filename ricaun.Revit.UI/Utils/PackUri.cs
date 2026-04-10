using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace ricaun.Revit.UI.Utils
{
    /// <summary>
    /// Represents a WPF pack URI for referencing resources embedded in assemblies.
    /// </summary>
    public class PackUri
    {
        /// <summary>
        /// The pack URI prefix used for application resources.
        /// </summary>
        public const string Pack = "pack://application:,,,/";
        /// <summary>
        /// The component segment used in pack URIs.
        /// </summary>
        public const string Component = "component/";
        /// <summary>
        /// The separator character between assembly name and component in a pack URI.
        /// </summary>
        public const char ComponentSeparator = ';';

        /// <summary>
        /// Attempts to parse a pack URI string into a <see cref="PackUri"/> instance.
        /// </summary>
        /// <param name="packUri">The pack URI string to parse.</param>
        /// <param name="result">When this method returns, contains the parsed <see cref="PackUri"/> if successful; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if parsing was successful; otherwise, <c>false</c>.</returns>
        public static bool TryParse(string packUri, out PackUri result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(packUri))
                return false;

            if (!TryParsePackUri(packUri, out string assemblyName, out string resourceName))
                return false;

            result = new PackUri(assemblyName, resourceName);
            return true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackUri"/> class from a pack URI string.
        /// </summary>
        /// <param name="packUri">The pack URI string to parse.</param>
        /// <exception cref="System.ArgumentException">Thrown if the pack URI is invalid.</exception>
        public PackUri(string packUri)
        {
            if (!TryParsePackUri(packUri, out string assemblyName, out string resourceName))
            {
                throw new ArgumentException($"Invalid pack URI: {packUri}", nameof(packUri));
            }
            AssemblyName = assemblyName;
            ResourceName = resourceName;
        }

        /// <summary>
        /// Gets the full pack URI string for this resource.
        /// </summary>
        public string PackPath => ToString();

        /// <summary>
        /// Gets the name of the assembly containing the resource.
        /// </summary>
        public string AssemblyName { get; private set; }

        /// <summary>
        /// Gets the name of the resource within the assembly.
        /// </summary>
        public string ResourceName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackUri"/> class with the specified assembly and resource names.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <param name="resourceName">The name of the resource.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="assemblyName"/> or <paramref name="resourceName"/> is null or whitespace.</exception>
        public PackUri(string assemblyName, string resourceName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName));

            if (string.IsNullOrWhiteSpace(resourceName))
                throw new ArgumentNullException(nameof(resourceName));

            AssemblyName = assemblyName;
            ResourceName = resourceName;
        }

        /// <summary>
        /// Returns the pack URI string representation of this instance.
        /// </summary>
        /// <returns>The pack URI string.</returns>
        public override string ToString()
        {
            return Pack + GetRelativePath();
        }

        /// <summary>
        /// Gets the relative path portion of the pack URI (assembly and resource).
        /// </summary>
        /// <returns>The relative path string.</returns>
        public string GetRelativePath()
        {
            return AssemblyName.ToLowerInvariant() + ComponentSeparator + Component + ResourceName.ToLowerInvariant();
        }

        /// <summary>
        /// Regular expression used to parse pack URIs and extract assembly and resource names.
        /// </summary>
        private static readonly Regex PackUriRegex = new Regex(
            @"^(?:pack://application:,,,/)?/?(?<assembly>[^/;]+);component/+(?<resource>.+?)\s*$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Tries to parse a pack URI and extract the assembly and resource names.
        /// </summary>
        /// <param name="packUri">The pack URI string to parse. Example: "pack://application:,,,/AssemblyName;component/ResourceName"</param>
        /// <param name="assemblyName">
        /// When this method returns, contains the name of the assembly if parsing succeeded; otherwise, <c>null</c>.
        /// </param>
        /// <param name="resourceName">
        /// When this method returns, contains the name of the resource if parsing succeeded; otherwise, <c>null</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the pack URI was successfully parsed and both assembly and resource names were extracted; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// The method uses a regular expression to match the expected pack URI format. It is case-insensitive and tolerant of optional "pack://application:,,,/" prefix.
        /// </remarks>
        public static bool TryParsePackUri(string packUri, out string assemblyName, out string resourceName)
        {
            assemblyName = null;
            resourceName = null;

            if (string.IsNullOrWhiteSpace(packUri))
                return false;

            Match match = PackUriRegex.Match(packUri.ToLowerInvariant());
            if (!match.Success)
                return false;

            assemblyName = match.Groups["assembly"].Value;
            resourceName = match.Groups["resource"].Value;
            return true;
        }
    }

    /// <summary>
    /// Provides extension methods for the <see cref="PackUri"/> class.
    /// </summary>
    public static class PackUriExtensions
    {
        /// <summary>
        /// Determines whether the resource referenced by this pack URI exists in the specified assembly.
        /// </summary>
        /// <param name="packUri">The pack URI to check for resource existence.</param>
        /// <remarks>This uses the <see cref="System.Windows.Application.GetResourceStream(Uri)"/> method to check for the resource's existence.</remarks>
        /// <returns><c>true</c> if the resource exists; otherwise, <c>false</c>.</returns>
        public static bool IsResourceExists(this PackUri packUri)
        {
            try
            {
                var applicationResource = Application.GetResourceStream(new Uri(packUri.PackPath, UriKind.Absolute));
                var exists = applicationResource != null;
                if (exists)
                {
                    applicationResource.Stream?.Dispose();
                }
                return exists;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to load a WPF component from the resource referenced by this pack URI.
        /// </summary>
        /// <param name="packUri">The pack URI referencing the component resource.</param>
        /// <param name="component">When this method returns, contains the loaded component if successful; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the component was successfully loaded; otherwise, <c>false</c>.</returns>
        public static bool TryLoadComponent(this PackUri packUri, out object component)
        {
            component = null;
            if (!packUri.IsResourceExists())
                return false;

            component = packUri.LoadComponent();
            return component is not null;
        }

        /// <summary>
        /// Loads a WPF component from the resource referenced by this pack URI.
        /// </summary>
        /// <param name="packUri">The pack URI referencing the component resource.</param>
        /// <returns>The loaded component object if successful; otherwise, <c>null</c>.</returns>
        /// <remarks>
        /// This method uses a relative URI format as required by <see cref="Application.LoadComponent(Uri)"/>.
        /// If the resource cannot be loaded, the method returns <c>null</c> instead of throwing an exception.
        /// </remarks>
        public static object LoadComponent(this PackUri packUri)
        {
            try
            {
                // This need to be Relative to work, pack uri absolute does not work here.
                return Application.LoadComponent(new Uri(packUri.GetRelativePath(), UriKind.Relative));
            }
            catch
            {
                return null;
            }
        }
    }
}

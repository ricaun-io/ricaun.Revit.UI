using Autodesk.Revit.UI;

namespace ricaun.Revit.UI
{
    public static class RibbonHelpExtension
    {
        #region ContextualHelp
        /// <summary>
        /// Get ContextualHelp by <paramref name="helpPath"/>
        /// </summary>
        /// <param name="helpPath"></param>
        /// <returns></returns>
        public static ContextualHelp GetContextualHelp(string helpPath)
        {
            ContextualHelp contextHelp = null;
            try
            {
                if (helpPath.StartsWith("http"))
                {
                    contextHelp = new ContextualHelp(ContextualHelpType.Url, helpPath);
                }
                else
                {
                    contextHelp = new ContextualHelp(ContextualHelpType.ChmFile, helpPath);
                }
            }
            catch { }
            return contextHelp;
        }
        #endregion
    }
}

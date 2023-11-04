using Autodesk.Revit.ApplicationServices;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// LanguageExtension
    /// </summary>
    public static class LanguageExtension
    {
        #region IsLanguage
        /// <summary>
        /// Is LanguageType == LanguageType.English_USA
        /// </summary>
        public static bool IsEnglish => GetLanguageType() == LanguageType.English_USA;
        /// <summary>
        /// Is LanguageType == LanguageType.French
        /// </summary>
        public static bool IsFrench => GetLanguageType() == LanguageType.French;
        /// <summary>
        /// Is LanguageType == LanguageType.German
        /// </summary>
        public static bool IsGerman => GetLanguageType() == LanguageType.German;
        /// <summary>
        /// Is LanguageType == LanguageType.Italian
        /// </summary>
        public static bool IsItalian => GetLanguageType() == LanguageType.Italian;
        /// <summary>
        /// Is LanguageType == LanguageType.Japanese
        /// </summary>
        public static bool IsJapanese => GetLanguageType() == LanguageType.Japanese;
        /// <summary>
        /// Is LanguageType == LanguageType.Korean
        /// </summary>
        public static bool IsKorean => GetLanguageType() == LanguageType.Korean;
        /// <summary>
        /// Is LanguageType == LanguageType.Polish
        /// </summary>
        public static bool IsPolish => GetLanguageType() == LanguageType.Polish;
        /// <summary>
        /// Is LanguageType == LanguageType.Spanish
        /// </summary>
        public static bool IsSpanish => GetLanguageType() == LanguageType.Spanish;
        /// <summary>
        /// Is LanguageType == LanguageType.Chinese_Simplified
        /// </summary>
        public static bool IsChineseSimplified => GetLanguageType() == LanguageType.Chinese_Simplified;
        /// <summary>
        /// Is LanguageType == LanguageType.Chinese_Traditional
        /// </summary>
        public static bool IsChineseTraditional => GetLanguageType() == LanguageType.Chinese_Traditional;
        /// <summary>
        /// Is LanguageType == LanguageType.Brazilian_Portuguese
        /// </summary>
        public static bool IsBrazilianPortuguese => GetLanguageType() == LanguageType.Brazilian_Portuguese;
        /// <summary>
        /// Is LanguageType == LanguageType.Russian
        /// </summary>
        public static bool IsRussian => GetLanguageType() == LanguageType.Russian;
        /// <summary>
        /// Is LanguageType == LanguageType.Czech
        /// </summary>
        public static bool IsCzech => GetLanguageType() == LanguageType.Czech;
        #endregion

        /// <summary>
        /// Get Base LanguageType
        /// </summary>
        /// <returns></returns>
        public static LanguageType GetLanguageType()
        {
            return GetCultureInfo().GetLanguageType();
        }

        /// <summary>
        /// Get LanguageType as CultureInfoName
        /// </summary>
        /// <param name="languageType"></param>
        /// <returns></returns>
        /// <remarks>LanguageType.Unknown or not valid return null, check <see cref="GetLanguages"/> available.</remarks>
        public static string GetCultureInfoName(this LanguageType languageType)
        {
            var findLanguage = GetLanguages()
                .FirstOrDefault(x => x.Value == languageType).Key;

            return findLanguage;
        }

        /// <summary>
        /// Get LanguageType by Culture Key
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, LanguageType> GetLanguages()
        {
            return new Dictionary<string, LanguageType>
            {
                { "en-US", LanguageType.English_USA },              // English - United States
                // { "en-GB", LanguageType.English_GB },              // English - United Kingdom
                { "fr-FR", LanguageType.French },                   // French
                { "de-DE", LanguageType.German },                   // German
                { "it-IT", LanguageType.Italian },                  // Italian
                { "ja-JP", LanguageType.Japanese },                 // Japanese
                { "ko-KR", LanguageType.Korean },                   // Korean
                { "pl-PL", LanguageType.Polish },                   // Polish
                { "es-ES", LanguageType.Spanish },                  // Spanish
                { "zh-CH", LanguageType.Chinese_Simplified },       // Simplified Chinese
                { "zh-TW", LanguageType.Chinese_Traditional },      // Traditional Chinese
                { "pt-BR", LanguageType.Brazilian_Portuguese },     // Brazilian Portuguese
                { "ru-RU", LanguageType.Russian },                  // Russian
                { "cs-CZ", LanguageType.Czech },                    // Czech
            };
        }

        /// <summary>
        /// Get CultureInfo LanguageType
        /// </summary>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        private static LanguageType GetLanguageType(this CultureInfo cultureInfo)
        {
            var languages = GetLanguages();

            LanguageType languageType;
            var name = cultureInfo.Name;
            if (languages.TryGetValue(name, out languageType))
            {
                return languageType;
            }

            return LanguageType.English_USA;
        }

        /// <summary>
        /// Get CurrentUICulture
        /// </summary>
        /// <returns></returns>
        private static CultureInfo GetCultureInfo()
        {
            var cultureInfo = CultureInfo.CurrentUICulture;
            return cultureInfo;
        }
    }
}

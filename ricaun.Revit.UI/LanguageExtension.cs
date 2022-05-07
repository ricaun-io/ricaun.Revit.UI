using Autodesk.Revit.ApplicationServices;
using System.Collections.Generic;
using System.Globalization;

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
        /// Is LanguageType == LanguageType.German
        /// </summary>
        public static bool IsGerman => GetLanguageType() == LanguageType.German;
        /// <summary>
        /// Is LanguageType == LanguageType.Spanish
        /// </summary>
        public static bool IsSpanish => GetLanguageType() == LanguageType.Spanish;
        /// <summary>
        /// Is LanguageType == LanguageType.French
        /// </summary>
        public static bool IsFrench => GetLanguageType() == LanguageType.French;
        /// <summary>
        /// Is LanguageType == LanguageType.Dutch
        /// </summary>
        public static bool IsDutch => GetLanguageType() == LanguageType.Dutch;
        /// <summary>
        /// Is LanguageType == LanguageType.Chinese_Simplified
        /// </summary>
        public static bool IsChineseSimplified => GetLanguageType() == LanguageType.Chinese_Simplified;
        /// <summary>
        /// Is LanguageType == LanguageType.Chinese_Traditional
        /// </summary>
        public static bool IsChineseTraditional => GetLanguageType() == LanguageType.Chinese_Traditional;
        /// <summary>
        /// Is LanguageType == LanguageType.Korean
        /// </summary>
        public static bool IsKorean => GetLanguageType() == LanguageType.Korean;
        /// <summary>
        /// Is LanguageType == LanguageType.Russian
        /// </summary>
        public static bool IsRussian => GetLanguageType() == LanguageType.Russian;
        /// <summary>
        /// Is LanguageType == LanguageType.Czech
        /// </summary>
        public static bool IsCzech => GetLanguageType() == LanguageType.Czech;
        /// <summary>
        /// Is LanguageType == LanguageType.Polish
        /// </summary>
        public static bool IsPolish => GetLanguageType() == LanguageType.Polish;
        /// <summary>
        /// Is LanguageType == LanguageType.Hungarian
        /// </summary>
        public static bool IsHungarian => GetLanguageType() == LanguageType.Hungarian;
        /// <summary>
        /// Is LanguageType == LanguageType.Brazilian_Portuguese
        /// </summary>
        public static bool IsBrazilianPortuguese => GetLanguageType() == LanguageType.Brazilian_Portuguese;
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
        public static string GetCultureInfoName(this LanguageType languageType)
        {
            string language = "";
            switch (languageType)
            {
                case LanguageType.Unknown:
                    language = "";
                    break;
                case LanguageType.English_USA:
                    language = "en-US";
                    break;
                case LanguageType.German:
                    language = "de-DE";
                    break;
                case LanguageType.Spanish:
                    language = "es-ES";
                    break;
                case LanguageType.French:
                    language = "fr-FR";
                    break;
                case LanguageType.Italian:
                    language = "it-IT";
                    break;
                case LanguageType.Dutch:
                    language = "nl-BE";
                    break;
                case LanguageType.Chinese_Simplified:
                    language = "zh-CHS";
                    break;
                case LanguageType.Chinese_Traditional:
                    language = "zh-CHT";
                    break;
                case LanguageType.Japanese:
                    language = "ja-JP";
                    break;
                case LanguageType.Korean:
                    language = "ko-KR";
                    break;
                case LanguageType.Russian:
                    language = "ru-RU";
                    break;
                case LanguageType.Czech:
                    language = "cs-CZ";
                    break;
                case LanguageType.Polish:
                    language = "pl-PL";
                    break;
                case LanguageType.Hungarian:
                    language = "hu-HU";
                    break;
                case LanguageType.Brazilian_Portuguese:
                    language = "pt-BR";
                    break;
            }
            return language;
        }

        /// <summary>
        /// Get LanguageType by Culture Key
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, LanguageType> GetLanguages()
        {
            var languages = new Dictionary<string, LanguageType>();

            languages.Add("en-US", LanguageType.English_USA);
            languages.Add("de-DE", LanguageType.German);
            languages.Add("es-ES", LanguageType.Spanish);
            languages.Add("fr-FR", LanguageType.French);
            languages.Add("nl-BE", LanguageType.Dutch);
            languages.Add("zh-CHS", LanguageType.Chinese_Simplified);
            languages.Add("zh-CHT", LanguageType.Chinese_Traditional);
            languages.Add("ko-KR", LanguageType.Korean);
            languages.Add("ru-RU", LanguageType.Russian);
            languages.Add("cs-CZ", LanguageType.Czech);
            languages.Add("pl-PL", LanguageType.Polish);
            languages.Add("hu-HU", LanguageType.Hungarian);
            languages.Add("pt-BR", LanguageType.Brazilian_Portuguese);

            return languages;
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

using Autodesk.Revit.ApplicationServices;
using NUnit.Framework;

namespace ricaun.Revit.UI.Tests
{
    public class RevitLanguageTests
    {
        private Application application;
        [OneTimeSetUp]
        public void Setup(Application application)
        {
            this.application = application;
        }

        [Test]
        public void Application_Language()
        {
            var language = application.Language;

            var name = language.GetCultureInfoName();
            Assert.IsNotNull(name);
        }

        [Test]
        public void Languages_Valid_CultureInfoName()
        {
            var languages = new[]
            {
                LanguageType.English_USA,
                LanguageType.French,
                LanguageType.German,
                LanguageType.Italian,
                LanguageType.Japanese,
                LanguageType.Korean,
                LanguageType.Polish,
                LanguageType.Spanish,
                LanguageType.Chinese_Simplified,
                LanguageType.Chinese_Traditional,
                LanguageType.Brazilian_Portuguese,
                LanguageType.Russian,
                LanguageType.Czech,
            };

            foreach (var language in languages)
            {
                var name = language.GetCultureInfoName();
                Assert.IsNotNull(name);
            }
        }

        [Test]
        public void Languages_NotValid_CultureInfoName()
        {
            var languages = new[]
            {
                LanguageType.Unknown,
                LanguageType.English_GB,    // Does not exist in Revit Api 2017
                LanguageType.Dutch,         // Not supported, how to start Revit in Dutch?
                LanguageType.Hungarian,     // Not supported, how to start Revit in Hungarian?
            };

            foreach (var language in languages)
            {
                var name = language.GetCultureInfoName();
                Assert.IsNull(name);
            }
        }

        [Test]
        public void Languages_GetLanguageType()
        {
            var language = application.Language;
            Assert.AreEqual(LanguageExtension.GetLanguageType(), language);
        }

        [Test]
        public void Languages_IsLanguage()
        {
            var language = application.Language;
            Assert.AreEqual(LanguageExtension.IsEnglish, language == LanguageType.English_USA);
            Assert.AreEqual(LanguageExtension.IsFrench, language == LanguageType.French);
            Assert.AreEqual(LanguageExtension.IsGerman, language == LanguageType.German);
            Assert.AreEqual(LanguageExtension.IsItalian, language == LanguageType.Italian);
            Assert.AreEqual(LanguageExtension.IsJapanese, language == LanguageType.Japanese);
            Assert.AreEqual(LanguageExtension.IsKorean, language == LanguageType.Korean);
            Assert.AreEqual(LanguageExtension.IsPolish, language == LanguageType.Polish);
            Assert.AreEqual(LanguageExtension.IsSpanish, language == LanguageType.Spanish);
            Assert.AreEqual(LanguageExtension.IsChineseSimplified, language == LanguageType.Chinese_Simplified);
            Assert.AreEqual(LanguageExtension.IsChineseTraditional, language == LanguageType.Chinese_Traditional);
            Assert.AreEqual(LanguageExtension.IsBrazilianPortuguese, language == LanguageType.Brazilian_Portuguese);
            Assert.AreEqual(LanguageExtension.IsRussian, language == LanguageType.Russian);
            Assert.AreEqual(LanguageExtension.IsCzech, language == LanguageType.Czech);
        }
    }

}
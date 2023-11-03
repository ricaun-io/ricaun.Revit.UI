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
                LanguageType.Dutch,         // Not supported
                LanguageType.Hungarian,     // Not supported
            };

            foreach (var language in languages)
            {
                var name = language.GetCultureInfoName();
                Assert.IsNull(name);
            }
        }
    }

}
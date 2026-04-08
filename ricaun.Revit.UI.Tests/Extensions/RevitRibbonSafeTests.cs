using Autodesk.Revit.UI;
using NUnit.Framework;
using System;
using System.Reflection;

namespace ricaun.Revit.UI.Tests.Extensions
{
    public class RevitRibbonSafeTests
    {
        /// <summary>
        /// This test is to validate the 'RibbonSafeExtension.VerifyNameExclusive' extension.
        /// </summary>
        private const string MethodName = "verifyNameExclusive";
        /// <summary>
        /// Types to test if method exist.
        /// </summary>
        private Type[] Types => new Type[]
        {
            typeof(RibbonPanel),
            typeof(RadioButtonGroup),
            typeof(PulldownButton),
            typeof(ComboBox),
        };
        [Test]
        public void RibbonSafeVerifyNameExclusive_Exists()
        {
            foreach (var type in Types)
            {
                var methodInfo = type.GetMethod(MethodName, BindingFlags.Instance | BindingFlags.NonPublic);
                Assert.IsNotNull(methodInfo, $"Method '{MethodName}' not found in '{type.Name}'.");
                var parameters = methodInfo.GetParameters();
                Assert.AreEqual(1, parameters.Length, $"Method '{MethodName}' in '{type.Name}' should have exactly one parameter.");
                Assert.AreEqual(typeof(string), parameters[0].ParameterType, $"Parameter of method '{MethodName}' in '{type.Name}' should be of type string.");
            }
        }
    }
}
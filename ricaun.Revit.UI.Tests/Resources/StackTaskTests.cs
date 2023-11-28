using NUnit.Framework;
using ricaun.Revit.UI.Utils;
using System.Reflection;

namespace ricaun.Revit.UI.Tests.Resources
{
    public class StackTaskTests
    {
        [Test]
        public void GetCallingAssembly_Equal()
        {
            var assembly = StackTraceUtils.GetCallingAssembly();
            Assert.AreEqual(Assembly.GetExecutingAssembly(), assembly);
        }

        [Test]
        public void GetCallingAssembly_Nested()
        {
            var assembly = StackTraceUtils.GetCallingAssemblyNested();
            Assert.AreEqual(Assembly.GetExecutingAssembly(), assembly);
        }
    }
}
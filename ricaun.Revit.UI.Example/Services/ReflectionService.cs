using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ricaun.Revit.UI.Example.Services
{
    public class ReflectionService
    {
        public void GetFields<T>()
        {
            var type = typeof(T);
            var fieldInfos = type.GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var fieldInfo in fieldInfos)
            {
                Console.WriteLine($"{fieldInfo.Name} {fieldInfo.FieldType}");
            }
        }

        public void GetMethods<T>()
        {
            var type = typeof(T);
            var methods = type.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var method in methods)
            {
                Console.WriteLine($"{method.Name} {method.DeclaringType}");
                foreach (var parameterInfo in method.GetParameters())
                {
                    Console.WriteLine($" {parameterInfo.Name} {parameterInfo.ParameterType}");
                }
            }
        }

        public TResult GetResult<T, TResult>(T obj, string methodName)
        {
            var type = typeof(T);
            var method = type.GetMethod(methodName,
                BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            return (TResult)method?.Invoke(obj, null);
        }

    }
}

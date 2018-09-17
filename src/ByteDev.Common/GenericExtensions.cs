using System;
using System.Linq;
using System.Reflection;

namespace ByteDev.Common
{
    public static class GenericExtensions
    {
        public static object InvokeMethod<T>(this T source, string methodName, params object[] args)
        {
            var type = typeof(T);
            var method = type.GetTypeInfo().GetDeclaredMethod(methodName);

            if (method == null)
                throw new ArgumentException($"Method '{methodName}' does not exist.", nameof(methodName));

            return method.Invoke(source, args);
        }

        /// <summary>
        /// Allows you to compare a value to a list of values analogous to the 'In' statement in SQL.
        /// Can be used instead of: (s=="John" || s=="Peter" or s=="Paul") one can write (s.In("John","Paul","Peter")) 
        /// </summary>
        public static bool In<T>(this T source, params T[] list)
        {
            return list.ToList().Contains(source);
        }
    }
}

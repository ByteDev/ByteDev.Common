using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteDev.Common.Reflection
{
    public static class TypeExtensions
    {
        public static IEnumerable<FieldInfo> GetConstants(this Type source)
        {
            var fieldInfos = source.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }

        public static IEnumerable<T> GetConstantsValues<T>(this Type source) where T : class
        {
            var fieldInfos = GetConstants(source);

            return fieldInfos.Select(fi => fi.GetRawConstantValue() as T);
        }
    }
}
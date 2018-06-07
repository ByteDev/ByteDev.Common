using System;
using System.Reflection;

namespace ByteDev.Common.Reflection
{
    public static class ReflectionPropertyExtensions
    {
        public static object GetPropertyValue(this object source, string name)
        {
            foreach (var propertyName in name.Split('.'))
            {
                if (source == null)
                {
                    return null;
                }

                var type = source.GetType();
                var propertyInfo = GetPropertyInfo(type, propertyName);
                source = propertyInfo.GetValue(source, null);
            }
            return source;
        }

        public static T GetPropertyValue<T>(this object source, string propertyName)
        {
            var value = GetPropertyValue(source, propertyName);
            return (T)value;
        }

        public static T GetStaticPropertyValue<T>(this object source, string propertyName)
        {
            var type = source.GetType();
            var propertyInfo = GetStaticPropertyInfo(propertyName, type);
            return (T)propertyInfo.GetValue(null, null);
        }

        private static PropertyInfo GetStaticPropertyInfo(string propertyName, Type type)
        {
            var propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);

            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Type '{type.Name}' has no property called '{propertyName}'.");
            }
            return propertyInfo;
        }

        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            var propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Type '{type.Name}' has no property called '{propertyName}'.");
            }
            return propertyInfo;
        }
    }
}
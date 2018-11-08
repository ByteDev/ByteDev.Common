using System;

namespace ByteDev.Common.Reflection
{
    public static class ReflectionPropertyExtensions
    {
        public static T GetPropertyValue<T>(this object source, string propertyName)
        {
            var value = GetPropertyValue(source, propertyName);
            return (T)value;
        }

        public static object GetPropertyValue(this object source, string propertyName)
        {
            foreach (var name in propertyName.Split('.'))
            {
                if (source == null)
                {
                    return null;
                }

                var type = source.GetType();
                var propertyInfo = type.GetPropertyInfoOrThrow(name);
                source = propertyInfo.GetValue(source, null);
            }
            return source;
        }

        public static T GetStaticPropertyValue<T>(this Type source, string propertyName)
        {
            var propertyInfo = source.GetStaticPropertyInfoOrThrow(propertyName);
            return (T)propertyInfo.GetValue(null, null);
        }

        public static T GetStaticPropertyValue<T>(this object source, string propertyName)
        {
            var type = source.GetType();
            return GetStaticPropertyValue<T>(type, propertyName);
        }
    }
}
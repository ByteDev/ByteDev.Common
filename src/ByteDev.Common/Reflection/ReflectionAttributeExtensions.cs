using System;
using System.Linq;
using System.Reflection;

namespace ByteDev.Common.Reflection
{
    public static class ReflectionAttributeExtensions
    {
        public static bool HasAttribute(this Type type, Type attributeType)
        {
            return type.GetTypeInfo().IsDefined(attributeType, true);
        }

        public static bool HasAttribute<T>(this Type type, Func<T, bool> predicate) where T : Attribute
        {
            return type.GetTypeInfo().GetCustomAttributes<T>(true).Any(predicate);
        }

        public static bool HasAttribute<TAttribute>(this object source) where TAttribute : Attribute
        {
            var memberInfo = source as MemberInfo;

            if (memberInfo != null)
            {
                return GetMemberAttribute<TAttribute>(memberInfo) != null;
            }

            var type = source.GetType();
            return GetTypeAttribute<TAttribute>(type) != null;
        }

        private static TAttribute GetMemberAttribute<TAttribute>(MemberInfo member) where TAttribute : Attribute
        {
            var attributes = member.GetCustomAttributes(typeof(TAttribute), true);

            if (attributes.Length > 0)
            {
                return (TAttribute)attributes.First();
            }
            return null;
        }

        private static TAttribute GetTypeAttribute<TAttribute>(Type type) where TAttribute : Attribute
        {
            var attributes = type.GetCustomAttributes(typeof (TAttribute), true);

            if (attributes.Length > 0)
            {
                return (TAttribute)attributes.First();
            }
            return null;
        }
    }
}

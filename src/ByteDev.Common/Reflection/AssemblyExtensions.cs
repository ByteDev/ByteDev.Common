using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteDev.Common.Reflection
{
    public static class AssemblyExtensions
    {
        public static List<Type> GetSubClasses<T>(this Assembly source) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return source.GetTypes().Where(type => type.IsSubclassOf(typeof(T))).ToList();
        }

        public static T GetAssemblyAttribute<T>(this Assembly source) where T : Attribute
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            object[] attributes = source.GetCustomAttributes(typeof(T), false);

            if (attributes.Length == 0)
            {
                return null;
            }
            return attributes.OfType<T>().SingleOrDefault();
        }

        public static Version GetVersion(this Assembly source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return source.GetName().Version;
        }

        public static Version GetFileVersion(this Assembly source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var fileVersionAttributes = (AssemblyFileVersionAttribute[])source.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);

            if (fileVersionAttributes.Length > 0)
            {
                return new Version(fileVersionAttributes[0].Version);
            }
            return null;
        }
    }
}
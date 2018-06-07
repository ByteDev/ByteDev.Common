using System;
using System.ComponentModel;

namespace ByteDev.Common
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the Enum's Description attribute text
        /// or null if does not have Description attribute.
        /// </summary>
        public static string GetDescription(this Enum source)
        {
            var fieldInfo = source.GetType().GetField(source.ToString());

            var attributes = (DescriptionAttribute[]) fieldInfo.GetCustomAttributes(typeof (DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return null;
        }

        /// <summary>
        /// Returns the Enum's Description attribute text or if the 
        /// Enum has no Description the Enum name as a string is returned.
        /// </summary>
        public static string GetDescriptionOrName(this Enum source)
        {
            var description = GetDescription(source);

            if (description == null)
            {
                return source.ToString();
            }
            return description;
        }
    }
}
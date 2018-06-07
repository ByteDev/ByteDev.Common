using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteDev.Common
{
    public static class EnumHelper
    {
        /// <summary>
        /// Returns the Enum's value as a list of Enum values
        /// </summary>
        public static IList<TEnum> ToList<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(e => e).ToList();
        }

        /// <summary>
        /// Returns the Enum's Description as a list of Enum values.
        /// If a Enum's value has no Description then the Enum value as a string is returned
        /// </summary>
        public static IList<string> ToDisplayList<TEnum>() where TEnum : struct
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select (e as Enum).GetDescriptionOrName();

            return values.ToList();
        }
    }
}
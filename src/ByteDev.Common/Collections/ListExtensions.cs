using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteDev.Common.Collections
{
    public static class ListExtensions
    {
        public static IList<T> NullToEmpty<T>(this IList<T> source)
        {
            if (source == null)
            {
                return new List<T>();
            }
            return source;
        }

        public static void RemoveWhere<T>(this IList<T> source, Func<T, bool> predicate)
        {
            source.Where(predicate)
                  .ToArray()
                  .ForEach(element => source.Remove(element));
        }

        /// <summary>
        /// Moves the first instance of target value to first position
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static void MoveToFirst<T>(this IList<T> source, T target) where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (!source.Contains(target))
                return;
            
            source.Remove(target);
            source.Insert(0, target);
        }
    }
}

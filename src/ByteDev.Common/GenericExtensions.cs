using System.Linq;

namespace ByteDev.Common
{
    public static class GenericExtensions
    {
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

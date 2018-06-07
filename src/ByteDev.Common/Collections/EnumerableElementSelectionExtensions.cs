using System;
using System.Collections.Generic;

namespace ByteDev.Common.Collections
{
    public static class EnumerableElementSelectionExtensions
    {
        /// <summary>
        /// Returns the second element of a sequence.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if sequence contains no second element.</exception>
        /// <typeparam name="T">Enumerable type</typeparam>
        /// <param name="source">Enumerable</param>
        /// <returns>Second element.</returns>
        public static T Second<T>(this IEnumerable<T> source)
        {
            return GetElement(source, 1);
        }

        /// <summary>
        /// Returns the third element of a sequence.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if sequence contains no third element.</exception>
        /// <typeparam name="T">Enumerable type</typeparam>
        /// <param name="source">Enumerable</param>
        /// <returns>Third element.</returns>
        public static T Third<T>(this IEnumerable<T> source)
        {
            return GetElement(source, 2);
        }

        /// <summary>
        /// Returns the fourth element of a sequence.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if sequence contains no fourth element.</exception>
        /// <typeparam name="T">Enumerable type</typeparam>
        /// <param name="source">Enumerable</param>
        /// <returns>Fourth element.</returns>
        public static T Fourth<T>(this IEnumerable<T> source)
        {
            return GetElement(source, 3);
        }

        /// <summary>
        /// Returns the fifth element of a sequence.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if sequence contains no fifth element.</exception>
        /// <typeparam name="T">Enumerable type</typeparam>
        /// <param name="source">Enumerable</param>
        /// <returns>Fifth element.</returns>
        public static T Fifth<T>(this IEnumerable<T> source)
        {
            return GetElement(source, 4);
        }

        private static T GetElement<T>(IEnumerable<T> list, int index)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            var count = 0;

            foreach (var item in list)
            {
                if (count == index)
                {
                    return item;
                }
                count++;
            }

            if(count == 0)
                throw new InvalidOperationException("Sequence contains no elements.");

            throw new InvalidOperationException(GetErrorMessageNoElementFor(index));
        }

        private static string GetErrorMessageNoElementFor(int index)
        {
            const string template = "Sequence contains no {0} element.";

            switch (index)
            {
                case 1:
                    return string.Format(template, "second");
                case 2:
                    return string.Format(template, "third");
                case 3:
                    return string.Format(template, "fourth");
                case 4:
                    return string.Format(template, "fifth");
                default:
                    throw new InvalidOperationException($"Position: {index}, was unhandled.");
            }
        }
    }
}
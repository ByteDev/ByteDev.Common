using System;

namespace ByteDev.Common
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Random" />.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Randomly return either true or false.
        /// </summary>
        /// <param name="source">Random object to perform the operation on.</param>
        /// <returns>True or false randomly.</returns>
        public static bool CoinToss(this Random source)
        {
            return source.Next(2) == 0;
        }

        /// <summary>
        /// Randomly returns one of the items from the supplied list of options.
        /// </summary>
        /// <typeparam name="TOptions">Type of options in the list.</typeparam>
        /// <param name="source">Random object to perform the operation on.</param>
        /// <param name="options">The list of options.</param>
        /// <returns>One of the options randomly selected.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="options" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="options" /> is empty.</exception>
        public static TOptions OneOf<TOptions>(this Random source, params TOptions[] options)
        {
            if(options == null)
                throw new ArgumentNullException(nameof(options));
            
            if(options.Length < 1)
                throw new ArgumentException($"Argument {nameof(options)} is empty.");

            return options[source.Next(options.Length)];
        }
    }
}

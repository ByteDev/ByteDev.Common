using System;

namespace ByteDev.Common
{
    public static class RandomExtensions
    {
        public static bool CoinToss(this Random source)
        {
            return source.Next(2) == 0;
        }

        public static T OneOf<T>(this Random source, params T[] options)
        {
            if(options == null)
                throw new ArgumentNullException(nameof(options));
            if(options.Length < 1)
                throw new ArgumentException("Options was less than one.");

            return options[source.Next(options.Length)];
        }
    }
}

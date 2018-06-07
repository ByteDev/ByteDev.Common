using System;

namespace ByteDev.Common
{
    public static class DoubleExtensions
    {
        public static string ToMoneyString(this double source)
        {
            var decAmount = (Decimal)source;
            return decAmount.ToString("C2");
        }
    }
}

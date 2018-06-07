using System;

namespace ByteDev.Common.Time
{
    public static class TimespanExtensions
    {
        public static string ToApproxFormat(this TimeSpan source)
        {
            if (source.Days > 0)
                return source.Days + " day".Pluralize(source.Days);
            if (source.Hours > 0)
                return source.Hours + " hour".Pluralize(source.Hours);
            if (source.Minutes > 0)
                return source.Minutes + " minute".Pluralize(source.Minutes);

            return source.Seconds + " second".Pluralize(source.Seconds);
        }
    }
}

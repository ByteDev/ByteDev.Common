using System;

namespace ByteDev.Common.Time
{
    public static class DateTimeExtensions
    {
        private const int DaysInWeek = 7;
        
        public static DateTime TruncateToSecond(this DateTime source)
        {
            return Truncate(source, TimeSpan.FromSeconds(1));
        }

        public static DateTime TruncateToMinute(this DateTime source)
        {
            return Truncate(source, TimeSpan.FromMinutes(1));
        }

        public static DateTime TruncateToHour(this DateTime source)
        {
            return Truncate(source, TimeSpan.FromHours(1));
        }

        public static string GetDaySuffix(this DateTime source)
        {
            switch (source.Day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }

        public static DateTime AddWeeks(this DateTime source, int numberOfWeeks)
        {
            return source.AddDays(DaysInWeek * numberOfWeeks);
        }

        public static DateTime SubtractWeeks(this DateTime source, int numberOfWeeks)
        {
            return source.AddDays(-1 * DaysInWeek * numberOfWeeks);
        }

        public static string ToDisplayDateStringShort(this DateTime source)
        {
            return string.Format("{0:ddd d}{1} {0:MMMM yyyy}", source, GetDaySuffix(source));
        }

        public static string ToDisplayDateStringLong(this DateTime source)
        {
            return string.Format("{0:dddd d}{1} {0:MMMM yyyy}", source, GetDaySuffix(source));
        }
        
        public static string ToDisplayDateDay(this DateTime source)
        {
            return $"{source.ToString("ddd")} {source.Day}{GetDaySuffix(source)}";
        }

        public static string ToSortableString(this DateTime source)
        {
            return ToSortableString(source, string.Empty);
        }

        public static string ToSortableString(this DateTime source, string delimiter)
        {
            return source.ToString($"yyyy{delimiter}MM{delimiter}dd{delimiter}hh{delimiter}mm{delimiter}ss");
        }

        public static bool IsLastDayOfTheMonth(this DateTime source)
        {
            return source == new DateTime(source.Year, source.Month, 1).AddMonths(1).AddDays(-1);
        }

        public static bool IsWeekend(this DateTime source)
        {
            return source.DayOfWeek == DayOfWeek.Saturday || source.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsLeapYear(this DateTime source)
        {
            return DateTime.IsLeapYear(source.Year);
        }

        public static DateTime ConvertUtcToLocalDateTime(this DateTime source, string timeZoneId)
        {
            var utcDateTime = DateTime.SpecifyKind(source, DateTimeKind.Utc);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZoneInfo);
        }

        public static DateTime ConvertLocalToUtcDateTime(this DateTime source, string timeZoneId)
        {
            // To avoid DateTimeKind errors Kind set to Unspecified, this covers the case of Utc dates being passed in
            var dateTime = DateTime.SpecifyKind(source, DateTimeKind.Unspecified);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            return TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZoneInfo);
        }

        private static DateTime Truncate(DateTime source, TimeSpan timeSpan)
        {
            return source.AddTicks(-(source.Ticks % timeSpan.Ticks));
        }
    }
}

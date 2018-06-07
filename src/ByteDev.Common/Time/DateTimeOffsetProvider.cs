using System;

namespace ByteDev.Common.Time
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset Now { get; }
        DateTimeOffset UtcNow { get; }
    }

    public class DateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset Now
        {
            get { return DateTimeOffset.Now; }
        }

        public DateTimeOffset UtcNow
        {
            get { return DateTimeOffset.UtcNow; }
        }
    }
}
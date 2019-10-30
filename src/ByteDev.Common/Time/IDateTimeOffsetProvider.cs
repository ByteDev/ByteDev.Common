using System;

namespace ByteDev.Common.Time
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset Now { get; }

        DateTimeOffset UtcNow { get; }
    }
}
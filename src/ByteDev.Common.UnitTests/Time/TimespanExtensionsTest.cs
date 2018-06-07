using System;
using ByteDev.Common.Time;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Time
{
    [TestFixture]
    public class TimespanExtensionsTest
    {
        [TestFixture]
        public class ToApproxFormat
        {
            [Test]
            public void WhenDaysAreGreaterThanZero_ThenReturnDayString()
            {
                var timeSpan = new TimeSpan(1, 2, 3, 4);
                var result = timeSpan.ToApproxFormat();

                Assert.That(result, Is.EqualTo(timeSpan.Days + " day"));
            }

            [Test]
            public void WhenHoursAreGreaterThanZero_ThenReturnHoursString()
            {
                var timeSpan = new TimeSpan(0, 2, 3, 4);
                var result = timeSpan.ToApproxFormat();

                Assert.That(result, Is.EqualTo(timeSpan.Hours + " hours"));
            }

            [Test]
            public void WhenMinutesAreGreaterThanZero_ThenReturnMinutesString()
            {
                var timeSpan = new TimeSpan(0, 0, 3, 4);
                var result = timeSpan.ToApproxFormat();

                Assert.That(result, Is.EqualTo(timeSpan.Minutes + " minutes"));
            }

            [Test]
            public void WhenSecondsAreGreaterThanZero_ThenReturnSecondsString()
            {
                var timeSpan = new TimeSpan(0, 0, 0, 4);
                var result = timeSpan.ToApproxFormat();

                Assert.That(result, Is.EqualTo(timeSpan.Seconds + " seconds"));
            }

            [Test]
            public void WhenSecondsAreZero_ThenReturnSecondsString()
            {
                var timeSpan = new TimeSpan(0, 0, 0, 0, 10);
                var result = timeSpan.ToApproxFormat();

                Assert.That(result, Is.EqualTo(timeSpan.Seconds + " seconds"));
            }
        }
    }
}

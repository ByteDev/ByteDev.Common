using System;
using ByteDev.Common.Time;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Time
{
    [TestFixture]
    public class DateTimeExtensionsTest
    {
        [TestFixture]
        public class TruncateToSecond
        {
            [Test]
            public void WhenMillisecondsSpecified_ThenSetMillisecondsZero()
            {
                var result = new DateTime(2000, 1, 10, 12, 20, 30, 99).TruncateToSecond();

                Assert.That(result.Millisecond, Is.EqualTo(0));
            }
        }

        [TestFixture]
        public class TruncateToMinute
        {
            [Test]
            public void WhenSecondsSpecified_ThenSetSecondsToZero()
            {
                var result = new DateTime(2000, 1, 10, 12, 20, 30, 99).TruncateToMinute();

                Assert.That(result.Second, Is.EqualTo(0));
                Assert.That(result.Millisecond, Is.EqualTo(0));
            }
        }

        [TestFixture]
        public class TruncateToHour
        {
            [Test]
            public void WhenMinutesSpecified_ThenSetMinutesToZero()
            {
                var result = new DateTime(2000, 1, 10, 12, 20, 30, 99).TruncateToHour();

                Assert.That(result.Minute, Is.EqualTo(0));
                Assert.That(result.Second, Is.EqualTo(0));
                Assert.That(result.Millisecond, Is.EqualTo(0));
            }
        }

        [TestFixture]
        public class GetDaySuffix
        {
            [Test]
            [TestCase(1, ExpectedResult = "st")]
            [TestCase(2, ExpectedResult = "nd")]
            [TestCase(3, ExpectedResult = "rd")]
            [TestCase(4, ExpectedResult = "th")]
            [TestCase(5, ExpectedResult = "th")]
            [TestCase(6, ExpectedResult = "th")]
            [TestCase(7, ExpectedResult = "th")]
            [TestCase(8, ExpectedResult = "th")]
            [TestCase(9, ExpectedResult = "th")]
            [TestCase(10, ExpectedResult = "th")]
            [TestCase(11, ExpectedResult = "th")]
            [TestCase(12, ExpectedResult = "th")]
            [TestCase(13, ExpectedResult = "th")]
            [TestCase(14, ExpectedResult = "th")]
            [TestCase(15, ExpectedResult = "th")]
            [TestCase(16, ExpectedResult = "th")]
            [TestCase(17, ExpectedResult = "th")]
            [TestCase(18, ExpectedResult = "th")]
            [TestCase(19, ExpectedResult = "th")]
            [TestCase(20, ExpectedResult = "th")]
            [TestCase(21, ExpectedResult = "st")]
            [TestCase(22, ExpectedResult = "nd")]
            [TestCase(23, ExpectedResult = "rd")]
            [TestCase(24, ExpectedResult = "th")]
            [TestCase(25, ExpectedResult = "th")]
            [TestCase(26, ExpectedResult = "th")]
            [TestCase(27, ExpectedResult = "th")]
            [TestCase(28, ExpectedResult = "th")]
            [TestCase(29, ExpectedResult = "th")]
            [TestCase(30, ExpectedResult = "th")]
            [TestCase(31, ExpectedResult = "st")]
            public string WhenDayProvided_ThenReturnCorrectSuffix(int day)
            {
                return new DateTime(2000, 1, day).GetDaySuffix();
            }
        }

        [TestFixture]
        public class SubtractWeeks
        {
            [Test]
            public void WhenWeeksIsOne_ThenSubtractOneWeekFromDate()
            {
                var expected = new DateTime(2000, 1, 10);
                var sut = new DateTime(2000, 1, 17);

                var result = sut.SubtractWeeks(1);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class AddWeeks
        {
            [Test]
            public void WhenWeeksIsOne_ThenAddOneWeekFromDate()
            {
                var expected = new DateTime(2000, 1, 24);
                var sut = new DateTime(2000, 1, 17);

                var result = sut.AddWeeks(1);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class ToSortableString
        {
            [Test]
            public void WhenNoDelimiter_ThenReturnNonDelimitedString()
            {
                var sut = new DateTime(2010, 2, 1, 9, 5, 1);

                var result = sut.ToSortableString();

                Assert.That(result, Is.EqualTo("20100201090501"));
            }

            [Test]
            public void WhenDelimiterProvided_ThenReturnDelimitedString()
            {
                var sut = new DateTime(2010, 2, 1, 9, 5, 1);

                var result = sut.ToSortableString("-");

                Assert.That(result, Is.EqualTo("2010-02-01-09-05-01"));
            }
        }

        [TestFixture]
        public class IsLastDayOfTheMonth
        {
            [Test]
            public void WhenDateIsLastDayOfTheMonth_ThenReturnTrue()
            {
                var sut = new DateTime(2014, 10, 31);

                var result = sut.IsLastDayOfTheMonth();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenDateIsNotLastDayOfTheMonth_ThenReturnFalse()
            {
                var sut = new DateTime(2014, 10, 30);

                var result = sut.IsLastDayOfTheMonth();

                Assert.That(result, Is.False);
            } 
        }

        [TestFixture]
        public class IsLeapYear
        {
            [Test]
            public void WhenIsLeapYear_ThenReturnTrue()
            {
                var sut = new DateTime(2016, 1, 1);

                var result = sut.IsLeapYear();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenIsNotLeapYear_ThenReturnFalse()
            {
                var sut = new DateTime(2015, 1, 1);

                var result = sut.IsLeapYear();

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class IsWeekend
        {
            [Test]
            public void WhenDateIsWeekend_ThenReturnTrue()
            {
                var sut = new DateTime(2014, 11, 1);

                var result = sut.IsWeekend();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenDateIsNotWeekend_ThenReturnFalse()
            {
                var sut = new DateTime(2014, 10, 31);

                var result = sut.IsWeekend();

                Assert.That(result, Is.False);
            } 
        }

        [TestFixture]
        public class ToDisplayDateStringShort
        {
            [Test]
            public void WhenDateTime_ThenReturnShortStringVersion()
            {
                var sut = new DateTime(2014, 12, 31, 12, 15, 30);

                var result = sut.ToDisplayDateStringShort();

                Assert.That(result, Is.EqualTo("Wed 31st December 2014"));
            } 
        }

        [TestFixture]
        public class ToDisplayDateStringLong
        {
            [Test]
            public void WhenDateTime_ThenReturnLongVersion()
            {
                var sut = new DateTime(2014, 12, 31, 12, 15, 30);

                var result = sut.ToDisplayDateStringLong();

                Assert.That(result, Is.EqualTo("Wednesday 31st December 2014"));
            } 
        }

        [TestFixture]
        public class ToDisplayDateDay
        {
            [Test]
            public void WhenDateTime_ThenReturnDateDayVersion()
            {
                var sut = new DateTime(2014, 12, 31, 12, 15, 30);

                var result = sut.ToDisplayDateDay();

                Assert.That(result, Is.EqualTo("Wed 31st"));
            } 
        }

        [TestFixture]
        public class ConvertUtcToLocalDateTime
        {
            private const string GbTimeZoneId = "GMT Standard Time";
            private const string CoordinatedUtc = "UTC";

            private readonly DateTime _dateTimeDayLightSavingInGb = new DateTime(2017, 6, 1, 2, 0, 0);
            private readonly DateTime _dateTimeNotDayLightSavingInGb = new DateTime(2017, 1, 1, 2, 0, 0);

            [Test]
            public void WhenIsDayLightSaving_AndGmt_ThenReturnOneHourLater()
            {
                var sut = _dateTimeDayLightSavingInGb;          
                
                var result = sut.ConvertUtcToLocalDateTime(GbTimeZoneId);

                Assert.That(result, Is.EqualTo(sut.AddHours(1)));
            }

            [Test]
            public void WhenIsNotDayLightSaving_AndGmt_ThenReturnSameTime()
            {
                var sut = _dateTimeNotDayLightSavingInGb;

                var result = sut.ConvertUtcToLocalDateTime(GbTimeZoneId);

                Assert.That(result, Is.EqualTo(sut));
            }

            [Test]
            public void WhenIsDayLightSaving_AndCoordinatedUtc_ThenReturnSameTime()
            {
                var sut = _dateTimeDayLightSavingInGb;

                var result = sut.ConvertUtcToLocalDateTime(CoordinatedUtc);

                Assert.That(result, Is.EqualTo(sut));
            }
        }
    }
}

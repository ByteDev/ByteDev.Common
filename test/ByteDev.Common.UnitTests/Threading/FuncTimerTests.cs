using System;
using ByteDev.Common.Threading;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Threading
{
    [TestFixture]
    public class FuncTimerTests
    {
        [TestFixture]
        public class WaitFuncReturnsTrueOrTimeout : FuncTimerTests
        {
            private readonly TimeSpan _timeSpan = new TimeSpan(0, 0, 0, 0, 500);

            [Test]
            public void WhenFuncIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => FuncTimer.WaitFuncReturnsTrueOrTimeout(null, _timeSpan));
            }

            [Test]
            public void WhenFuncDoesNotReturnTrueBeforeTimeout_ThenReturnFalse()
            {
                var result = FuncTimer.WaitFuncReturnsTrueOrTimeout(() => false, _timeSpan);

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenFuncDoesReturnTrueBeforeTimeout_ThenReturnTrue()
            {
                var result = FuncTimer.WaitFuncReturnsTrueOrTimeout(() => true, _timeSpan);

                Assert.That(result, Is.True);
            }
        }
    }
}
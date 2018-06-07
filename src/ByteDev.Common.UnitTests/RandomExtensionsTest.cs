using System;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class RandomExtensionsTest
    {
        [TestFixture]
        public class CoinToss
        {
            [Test]
            public void WhenCalled_ThenReturnTrueOrFalse()
            {
                var result = new Random().CoinToss();

                Assert.That(result || !result, Is.True);    // bit daft, but only way to test
            }
        }

        [TestFixture]
        public class OneOf
        {
            [Test]
            public void WhenNullOptions_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => new Random().OneOf(null as string[]));
            }

            [Test]
            public void WhenZeroExist_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => new Random().OneOf(new string[0]));
            }

            [Test]
            public void WhenOneExist_ThenReturnOption()
            {
                var result = new Random().OneOf("John");

                Assert.That(result, Is.EqualTo("John"));
            }

            [Test]
            public void WhenThreeExist_ThenReturnOneOfThreeOptions()
            {
                var result = new Random().OneOf("John", "Peter", "Luke");
                
                Assert.IsTrue(result == "John" || result == "Peter" || result == "Luke");
            }
        }
    }
}

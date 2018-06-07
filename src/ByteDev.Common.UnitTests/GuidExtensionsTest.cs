using System;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class GuidExtensionsTest
    {
        [TestFixture]
        public class IsEmpty
        {
            [Test]
            public void WhenGuidIsEmpty_ThenReturnTrue()
            {
                var id = Guid.Empty;

                Assert.IsTrue(id.IsEmpty());
            }

            [Test]
            public void WhenGuidIsNotEmpty_ThenReturnFalse()
            {
                var id = Guid.NewGuid();

                Assert.IsFalse(id.IsEmpty());
            }
        }

        [TestFixture]
        public class Comb
        {
            [Test]
            public void WhenGuidIsEmpty_ThenThrowException()
            {
                var id = Guid.Empty;

                Assert.Throws<ArgumentException>(() => id.Comb());
            }

            [Test]
            public void WhenGuidIsNotEmpty_ThenReturnDifferentGuid()
            {
                var id = Guid.NewGuid();

                var result = id.Comb();

                Assert.That(id, Is.Not.EqualTo(result));
            }
        }
    }
}

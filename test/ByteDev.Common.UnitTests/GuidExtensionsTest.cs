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
            public void WhenGuidIsDefault_ThenReturnTrue()
            {
                var id = default(Guid);

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
                var originalId = id;

                var result = id.Comb();

                Assert.That(result, Is.Not.EqualTo(id));
                Assert.That(result.ToString().Substring(0, 20), Is.EqualTo(originalId.ToString().Substring(0, 20)));
            }
        }
    }
}

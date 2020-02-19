using System.Linq;
using ByteDev.Collections;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class EnumHelperTest
    {
        public enum ZeroValueEnum
        {
        }

        [TestFixture]
        public class ToList : EnumExtensionsTest
        {
            [Test]
            public void WhenEnumHasZeroValues_ThenReturnEmpty()
            {
                var result = EnumHelper.ToList<ZeroValueEnum>();

                Assert.That(result.Count, Is.EqualTo(0));
            }

            [Test]
            public void WhenEnumHasTwoValues_ThenReturnTwo()
            {
                var result = EnumHelper.ToList<DummyEnum>();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First(), Is.EqualTo(DummyEnum.HasDescription));
                Assert.That(result.Second(), Is.EqualTo(DummyEnum.HasNoDescription));
            }
        }

        [TestFixture]
        public class ToDisplayList : EnumExtensionsTest
        {
            [Test]
            public void WhenEnumHasZeroValues_ThenReturnEmpty()
            {
                var result = EnumHelper.ToDisplayList<ZeroValueEnum>();

                Assert.That(result.Count, Is.EqualTo(0));
            }

            [Test]
            public void WhenEnumHasOneWithDescriptionAndOneWithout_ThenReturnTwo()
            {
                var result = EnumHelper.ToDisplayList<DummyEnum>();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First(), Is.EqualTo("description"));
                Assert.That(result.Second(), Is.EqualTo("HasNoDescription"));
            }
        }
    }
}
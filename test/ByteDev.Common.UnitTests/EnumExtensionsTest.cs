using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class EnumExtensionsTest
    {
        public enum DummyEnum
        {
            [System.ComponentModel.Description("description")]
            HasDescription,
            HasNoDescription,
        }

        [TestFixture]
        public class GetDescription : EnumExtensionsTest
        {
            [Test]
            public void WhenHasDescription_ThenReturnDescription()
            {
                var result = DummyEnum.HasDescription.GetDescription();

                Assert.That(result, Is.EqualTo("description"));
            }

            [Test]
            public void WhenHasNoDescription_ThenReturnNull()
            {
                var result = DummyEnum.HasNoDescription.GetDescription();

                Assert.That(result, Is.Null);
            }
        }

        [TestFixture]
        public class GetDescriptionOrName : EnumExtensionsTest
        {
            [Test]
            public void WhenHasDescription_ThenReturnDescription()
            {
                var result = DummyEnum.HasDescription.GetDescriptionOrName();

                Assert.That(result, Is.EqualTo("description"));
            }

            [Test]
            public void WhenHasNoDescription_ThenReturnEnumNameAsString()
            {
                var result = DummyEnum.HasNoDescription.GetDescriptionOrName();

                Assert.That(result, Is.EqualTo(DummyEnum.HasNoDescription.ToString()));
            }
        }
    }
}
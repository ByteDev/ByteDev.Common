using System;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class GenericExtensionsTest
    {
        [TestFixture]
        public class CloneSerializable : GenericExtensionsTest
        {
            [Test]
            public void WhenIsReferencingNull_ThenReturnDefault()
            {
                var result = GenericExtensions.CloneSerializable(null as DummySerializable);

                Assert.IsNull(result);
            }

            [Test]
            public void WhenTypeIsNotSerializable_ThenThrowException()
            {
                var sut = new DummyNotSerializable();

                Assert.Throws<ArgumentException>(() => sut.CloneSerializable());
            }

            [Test]
            public void WhenTypeIsSerializable_ThenReturnClone()
            {
                var sut = new DummySerializable();

                var result = sut.CloneSerializable();

                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Not.SameAs(sut));
            }

            [Test]
            public void WhenTypeIsSerializable_AndIsComplex_ThenReturnDeepClone()
            {
                var sut = new DummySerializableComplex { InnerDummy = new DummySerializable { Name = "John" } };

                var result = sut.CloneSerializable();

                Assert.That(result.InnerDummy, Is.Not.SameAs(sut.InnerDummy));
                Assert.That(result.InnerDummy.Name, Is.EqualTo(sut.InnerDummy.Name));
            }

            public class DummyNotSerializable
            {
            }

            [Serializable]
            public class DummySerializable
            {
                public string Name { get; set; }
            }

            [Serializable]
            public class DummySerializableComplex
            {
                public DummySerializable InnerDummy { get; set; }
            }
        }

        [TestFixture]
        public class In
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => GenericExtensions.In(null, "John"));
            }

            [Test]
            public void WhenListIsNull_ThenThrowException()
            {
                const string sut = "John";

                Assert.Throws<ArgumentNullException>(() => sut.In(null));
            }

            [Test]
            public void WhenSourceIsContainedInList_ThenReturnTrue()
            {
                const string sut = "John";

                var result = sut.In("John", "Peter", "Paul");

                Assert.True(result);
            }

            [Test]
            public void WhenSourceIsNotContainedInList_ThenReturnFalse()
            {
                const string sut = "John Smith";

                var result = sut.In("John", "Peter", "Paul");

                Assert.False(result);
            }
        }
    }
}

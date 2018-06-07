using System;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class ObjectExtensionsTest
    {
        [TestFixture]
        public class CloneSerializable
        {
            [Test]
            public void WhenIsReferencingNull_ThenReturnDefault()
            {
                DummySerializable sut = null;

                var result = sut.CloneSerializable();

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
                public string Name;
            }

            [Serializable]
            public class DummySerializableComplex
            {
                public DummySerializable InnerDummy;
            }
        }
    }
}
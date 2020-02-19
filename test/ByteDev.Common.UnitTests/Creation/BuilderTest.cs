using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Creation
{
    [TestFixture]
    public class BuilderTest
    {
        private DummyEntityBuilder _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = DummyEntityBuilder.InMemory;
        }

        [Test]
        public void WhenWithNotCalled_ThenDontSetProperty()
        {
            var result = _sut.Build();

            Assert.That(result.Id, Is.EqualTo(default(int)));
        }

        [Test]
        public void WhenWithMethod_ThenSetProperty()
        {
            var result = _sut.WithId(10).Build();

            Assert.That(result.Id, Is.EqualTo(10));
        }

        [Test]
        public void WhenWithCalledTwice_ThenSetProperty()
        {
            _sut.WithId(10);
            _sut.WithId(20);

            var result = _sut.Build();

            Assert.That(result.Id, Is.EqualTo(20));
        }

        [Test]
        public void WhenMutationsCleared_ThenPropertyNotSet()
        {
            _sut.WithId(10);
            _sut.Clear();

            var result = _sut.Build();

            Assert.That(result.Id, Is.EqualTo(0));
        }
    }
}
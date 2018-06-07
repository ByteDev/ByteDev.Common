using System;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class DisposableExtensionsTest
    {
        [TestFixture]
        public class DisposeIfNotNull
        {
            [Test]
            public void WhenIsNull_ThenDoNotCallDispose()
            {
                var sut = null as DisposableDummy;

                sut.DisposeIfNotNull();
            }

            [Test]
            public void WhenIsNotNull_ThenCallDispose()
            {
                var sut = new DisposableDummy();

                sut.DisposeIfNotNull();

                Assert.That(sut.Disposed, Is.True);
            }
        }
    }

    internal class DisposableDummy : IDisposable
    {
        public bool Disposed { get; private set; }

        public void Dispose()
        {
            Disposed = true;
        }
    }
}
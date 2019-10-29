using System;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class GenericExtensionsTest
    {
        protected internal bool MyProtectedInternalMethod()
        {
            return true;
        }

        internal bool MyInternalMethod()
        {
            return true;
        }

        protected bool MyProtectedMethod()
        {
            return true;
        }

        private bool MyPrivateMethod()
        {
            return true;
        }

        private void MyPrivateMethodReturnVoid()
        {
        }

        [TestFixture]
        public class InvokeMethod : ObjectExtensionsTest
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                object sut = null;

                Assert.Throws<ArgumentNullException>(() => sut.InvokeMethod("MyProtectedInternalMethod"));
            }

            [Test]
            public void WhenMethodNameIsNull_ThenThrowException()
            {
                var sut = CreateSut();

                Assert.Throws<ArgumentNullException>(() => sut.InvokeMethod(null));
            }

            [Test]
            public void WhenMethodNameIsEmpty_ThenThrowException()
            {
                var sut = CreateSut();

                Assert.Throws<ArgumentException>(() => sut.InvokeMethod(string.Empty));
            }

            [Test]
            public void WhenMethodNameDoesNotExist_ThenThrowException()
            {
                var sut = CreateSut();

                Assert.Throws<ArgumentException>(() => sut.InvokeMethod("ThisMethodDoesNotExist"));
            }

            [Test]
            public void WhenInvokingExistingProtectedInternalMethod_ThenReturnValue()
            {
                var sut = CreateSut();

                var result = sut.InvokeMethod("MyProtectedInternalMethod");

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenInvokingExistingInternalMethod_ThenReturnValue()
            {
                var sut = CreateSut();

                var result = sut.InvokeMethod("MyInternalMethod");

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenInvokingExistingProtectedMethod_ThenReturnValue()
            {
                var sut = CreateSut();

                var result = sut.InvokeMethod("MyProtectedMethod");

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenInvokingExistingPrivateMethod_ThenReturnValue()
            {
                var sut = CreateSut();

                var result = sut.InvokeMethod("MyPrivateMethod");

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenInvokingExistingMethodWithVoidReturn_ThenReturnNull()
            {
                var sut = CreateSut();

                var result = sut.InvokeMethod("MyPrivateMethodReturnVoid");

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenInvokingNonExistingMethod_ThenThrowException()
            {
                var sut = CreateSut();

                Assert.Throws<ArgumentException>(() => sut.InvokeMethod("MyInternalMethod1"));
            }

            private static GenericExtensionsTest CreateSut()
            {
                return new GenericExtensionsTest();
            }
        }

        [TestFixture]
        public class In
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                string sut = null;

                Assert.Throws<ArgumentNullException>(() => sut.In("John"));
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

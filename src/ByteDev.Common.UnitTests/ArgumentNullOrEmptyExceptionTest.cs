using System;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class ArgumentNullOrEmptyExceptionTest
    {
        [TestFixture]
        public class Constructor : ArgumentNullOrEmptyExceptionTest
        {
            [Test]
            public void WhenNoArgs_ThenSetMessageToDefault()
            {
                var sut = new ArgumentNullOrEmptyException();

                Assert.That(sut.Message, Is.EqualTo("Value cannot be null or empty."));
            }

            [Test]
            public void WhenParamNameSpecified_ThenSetMessageAndParamName()
            {
                var sut = new ArgumentNullOrEmptyException("myArg");

                Assert.That(sut.Message, Is.EqualTo("Value cannot be null or empty.\r\nParameter name: myArg"));
                Assert.That(sut.ParamName, Is.EqualTo("myArg"));
            }

            [Test]
            public void WhenMessageAndInnerExSpecified_ThenSetMessageAndInnerEx()
            {
                var innerException = new Exception();

                var sut = new ArgumentNullOrEmptyException("some message.", innerException);

                Assert.That(sut.Message, Is.EqualTo("some message."));
                Assert.That(sut.InnerException, Is.SameAs(innerException));
            }

            [Test]
            public void WhenParamNameAndMessageSpecified_ThenSetMessageAndParamName()
            {
                var sut = new ArgumentNullOrEmptyException("myArg", "some message.");

                Assert.That(sut.Message, Is.EqualTo("some message.\r\nParameter name: myArg"));
                Assert.That(sut.ParamName, Is.EqualTo("myArg"));
            }
        }
    }
}
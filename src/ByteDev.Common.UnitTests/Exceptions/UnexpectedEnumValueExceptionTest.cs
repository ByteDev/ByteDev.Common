using System;
using ByteDev.Common.Exceptions;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Exceptions
{
    [TestFixture]
    public class UnexpectedEnumValueExceptionTest
    {
        public enum Color
        {
            Red
        }

        [TestFixture]
        public class Constructor : UnexpectedEnumValueExceptionTest
        {
            [Test]
            public void WhenNoArgs_ThenSetMessageToDefault()
            {
                var sut = new UnexpectedEnumValueException<Color>();

                Assert.That(sut.Message, Is.EqualTo("Unexpected value for enum 'ByteDev.Common.UnitTests.Exceptions.UnexpectedEnumValueExceptionTest+Color'."));
            }

            [Test]
            public void WhenEnumValueSpecified_ThenSetMessage()
            {
                var sut = new UnexpectedEnumValueException<Color>(Color.Red);

                Assert.That(sut.Message, Is.EqualTo("Unexpected value 'Red' for enum 'ByteDev.Common.UnitTests.Exceptions.UnexpectedEnumValueExceptionTest+Color'."));
            }

            [Test]
            public void WhenMessageSpecified_ThenSetMessage()
            {
                var sut = new UnexpectedEnumValueException<Color>("some message.");

                Assert.That(sut.Message, Is.EqualTo("some message."));
            }

            [Test]
            public void WhenMessageAndInnerExSpecified_ThenSetMessageAndInnerEx()
            {
                var innerException = new Exception();

                var sut = new UnexpectedEnumValueException<Color>("some message.", innerException);

                Assert.That(sut.Message, Is.EqualTo("some message."));
                Assert.That(sut.InnerException, Is.SameAs(innerException));
            }
        }
    }
}
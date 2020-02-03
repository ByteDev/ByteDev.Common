using System;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class StringIsExtensionsTest
    {
        [TestFixture]
        public class IsEmpty
        {
            [Test]
            public void WhenEmpty_ThenReturnTrue()
            {
                var sut = string.Empty;
                Assert.That(sut.IsEmpty(), Is.True);
            }

            [Test]
            public void WhenNotEmpty_ThenReturnFalse()
            {
                const string sut = "something";
                Assert.That(sut.IsEmpty(), Is.False);
            }

            [Test]
            public void WhenIsNull_ThenReturnFalse()
            {
                Assert.That((null as string).IsEmpty(), Is.False);
            }
        }

        [TestFixture]
        public class IsNullOrEmpty
        {
            [Test]
            public void WhenNull_ThenReturnTrue()
            {
                Assert.That((null as string).IsNullOrEmpty(), Is.True);
            }

            [Test]
            public void WhenEmpty_ThenReturnTrue()
            {
                Assert.That(string.Empty.IsNullOrEmpty(), Is.True);
            }

            [Test]
            public void WhenIsNotNullOrEmpty_ThenReturnTrue()
            {
                const string sut = "a";

                Assert.That(sut.IsNullOrEmpty(), Is.False);
            }
        }

        [TestFixture]
        public class IsNotNullOrEmpty
        {
            [Test]
            public void WhenNull_ThenReturnFalse()
            {
                Assert.That((null as string).IsNotNullOrEmpty(), Is.False);
            }

            [Test]
            public void WhenEmpty_ThenReturnFalse()
            {
                Assert.That(string.Empty.IsNotNullOrEmpty(), Is.False);
            }

            [Test]
            public void WhenNotNullOrEmpty_ThenReturnTrue()
            {
                const string sut = "test";

                Assert.That(sut.IsNotNullOrEmpty(), Is.True);
            }
        }

        [TestFixture]
        public class IsNullOrWhitespaceOnly
        {
            [TestCase(null, true)]
            [TestCase("", true)]
            [TestCase(" ", true)]
            [TestCase(" ", true)]
            [TestCase(" a", false)]
            public void WhenProvided_ThenReturnExpected(string sut, bool expected)
            {
                var result = sut.IsNullOrWhitespaceOnly();

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class IsEmailAddress
        {
            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("john.com", false)]
            [TestCase("@somewhere.com", false)]
            [TestCase("john@google.com", true)]
            public void WhenProvided_ThenReturnExpected(string sut, bool expected)
            {
                var result = sut.IsEmailAddress();

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class IsIpAddress
        {
            [TestCase("0.0.0.0", true)]
            [TestCase("127.0.0.1", true)]
            [TestCase("192.168.1.254", true)]
            [TestCase("255.255.255.255", true)]
            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("192", false)]
            [TestCase("192.168", false)]
            [TestCase("192.168.1", false)]
            [TestCase("192.168.1.XXX", false)]
            [TestCase("192.168.1.256", false)]
            public void WhenProvided_ThenReturnExpected(string sut, bool expected)
            {
                var result = sut.IsIpAddress();

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class IsGuid
        {
            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("123456567-ABCDEFGH", false)]
            [TestCase("A5EF801D-13BC-4C6F-94AA-C7152C8BC158", true)]
            [TestCase("a5ef801d-13bc-4c6f-94aa-c7152c8bc158", true)]
            [TestCase("{a5ef801d-13bc-4c6f-94aa-c7152c8bc158}", true)]
            [TestCase("00000000-0000-0000-0000-000000000000", true)]
            public void WhenProvided_ThenReturnExpected(string sut, bool expected)
            {
                var result = sut.IsGuid();

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class IsXml
        {
            [Test]
            public void WhenIsNull_ThenReturnFalse()
            {
                var result = (null as string).IsXml();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenIsEmpty_ThenReturnFalse()
            {
                var result = string.Empty.IsXml();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenIsValidXml_AndNoDeclaration_ThenReturnTrue()
            {
                const string sut = "<item><name>wrench</name></item>";

                var result = sut.IsXml();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenIsValidXml_AndDeclaration_ThenReturnTrue()
            {
                string sut = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + Environment.NewLine +
                                        "<items />";

                var result = sut.IsXml();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenIsInvalidXml_AndNoRootElement_ThenReturnFalse()
            {
                const string sut = "<item><name>wrench</name></item><item></item>";

                var result = sut.IsXml();

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class IsUrl
        {
            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("google.co.uk", false)]
            [TestCase("www.google.co.uk", false)]
            [TestCase("http://www.google.co.uk", true)]
            [TestCase("http://www.google.co.uk/", true)]
            [TestCase("http://www.google.co.uk/path", true)]
            [TestCase("http://www.google.co.uk/path/", true)]
            [TestCase("http://www.google.co.uk/path/file", true)]
            [TestCase("http://www.google.co.uk/path/#fragment", true)]
            public void WhenProvided_ThenReturnExpected(string sut, bool expected)
            {
                var result = sut.IsUrl();

                Assert.That(result, Is.EqualTo(expected));
            } 
        }
    }
}
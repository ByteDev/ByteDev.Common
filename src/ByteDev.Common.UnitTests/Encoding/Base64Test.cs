using System;
using ByteDev.Common.Encoding;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Encoding
{
    [TestFixture]
    public class Base64Test
    {
        [TestFixture]
        public class IsBase64Encoded
        {
            [Test]
            public void WhenArgIsNull_ThenReturnFalse()
            {
                var result = Act(null);

                Assert.IsFalse(result);
            }

            [Test]
            public void WhenArgIsEmpty_ThenReturnFalse()
            {
                var result = Act(string.Empty);

                Assert.IsFalse(result);
            }

            [Test]
            public void WhenArgContainsNonBase64Char_ThenReturnFalse()
            {
                const string notBase64 = "£Sm9obiBTbWl0aA==";
                var result = Act(notBase64);

                Assert.IsFalse(result);
            }

            [Test]
            public void WhenArgLengthIsMultipleOfFour_ThenReturnFalse()
            {
                const string notBase64 = "Sm9obiBTbWl0==";
                var result = Act(notBase64);

                Assert.IsFalse(result);
            }

            [Test]
            public void WhenArgIsBase64Encoded_ThenReturnTrue()
            {
                const string base64 = "Sm9obiBTbWl0aA==";           // "John Smith"
                var result = Act(base64);

                Assert.IsTrue(result);
            }

            private static bool Act(string str)
            {
                return Base64.IsBase64Encoded(str);
            }
        }

        [TestFixture]
        public class CalcBase64EncodedSize
        {
            [TestCase(-1, 0)]
            [TestCase(0, 0)]
            [TestCase(10, 16)]
            [TestCase(15, 20)]
            [TestCase(16, 24)]
            [TestCase(50, 68)]
            public void WhenOriginalSizeProvided_ThenReturnExpectedBase64Size(long originalSize, long expected)
            {
                var result = Base64.CalcBase64EncodedSize(originalSize);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class ConvertUtf8ToBase64
        {
            [Test]
            public void WhenArgIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => Base64.ConvertUtf8ToBase64(null));
            }

            [TestCase("", "")]
            [TestCase("John Smith", "Sm9obiBTbWl0aA==")]
            [TestCase("John Smith12345", "Sm9obiBTbWl0aDEyMzQ1")]
            public void WhenUtf8StringIsNotNull_ThenReturnBased64(string text, string expected)
            {
                var result = Base64.ConvertUtf8ToBase64(text);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class ConvertBase64ToUtf8
        {
            [Test]
            public void WhenArgIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => Base64.ConvertBase64ToUtf8(null));
            }

            [TestCase("", "")]
            [TestCase("Sm9obiBTbWl0aA==", "John Smith")]
            [TestCase("Sm9obiBTbWl0aDEyMzQ1", "John Smith12345")]
            public void WithBase64_ThenReturnText(string base64, string expected)
            {
                var result = Base64.ConvertBase64ToUtf8(base64);

                Assert.That(result, Is.EqualTo(expected));
            }
        }
    }
}

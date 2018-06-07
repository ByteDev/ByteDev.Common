using ByteDev.Common.Xml;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Xml
{
    [TestFixture]
    public class XmlEncoderTest
    {
        [TestFixture]
        public class Encode
        {
            private const string Text = "Using {0} special entity";

            [Test]
            public void WhenIsNull_ThenReturnNull()
            {
                var result = XmlEncoder.Encode(null);

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenIsEmpty_ThenReturnEmpty()
            {
                var result = XmlEncoder.Encode(string.Empty);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenContainsDoubleQuote_ThenEncodeToEntity()
            {
                var result = XmlEncoder.Encode(Text.FormatWith("\""));

                Assert.That(result, Is.EqualTo(Text.FormatWith("&quot;")));
            }

            [Test]
            public void WhenContainsAmpersand_ThenEncodeToEntity()
            {
                var result = XmlEncoder.Encode(Text.FormatWith("&"));

                Assert.That(result, Is.EqualTo(Text.FormatWith("&amp;")));
            }

            [Test]
            public void WhenContainsApostraphe_ThenEncodeToEntity()
            {
                var result = XmlEncoder.Encode(Text.FormatWith("'"));

                Assert.That(result, Is.EqualTo(Text.FormatWith("&apos;")));
            }

            [Test]
            public void WhenContainsLessThan_ThenEncodeToEntity()
            {
                var result = XmlEncoder.Encode(Text.FormatWith("<"));

                Assert.That(result, Is.EqualTo(Text.FormatWith("&lt;")));
            }

            [Test]
            public void WhenContainsGreaterThan_ThenEncodeToEntity()
            {
                var result = XmlEncoder.Encode(Text.FormatWith(">"));

                Assert.That(result, Is.EqualTo(Text.FormatWith("&gt;")));
            }
        }
        
        [TestFixture]
        public class SanitizeForXml
        {
            [Test]
            public void WhenIsNull_ThenReturnNull()
            {
                var result = XmlEncoder.SanitizeForXml(null);

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenIsEmpty_ThenReturnEmpty()
            {
                var result = XmlEncoder.SanitizeForXml(string.Empty);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenHasIllegalChars_ThenRemoveIllegalChars()
            {
                const string illegalChar = "\u0008";
                const string s = "this {0} that {0} this";

                var result = XmlEncoder.SanitizeForXml(s.FormatWith(illegalChar));

                Assert.That(result, Is.EqualTo(s.FormatWith("")));
            }
        }

        [TestFixture]
        public class IsLegalXmlChar
        {
            [Test]
            public void WhenIsLegal_ThenReturnTrue()
            {
                var result = XmlEncoder.IsLegalXmlChar(65);

                Assert.That(result, Is.True);
            }
        }
    }
}
using System.Collections.Specialized;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class UriQueryStringConverterTest
    {
        [TestFixture]
        public class ConvertToQueryString : UriQueryStringConverterTest
        {
            private UriQueryStringConverter _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new UriQueryStringConverter();
            }

            [Test]
            public void WhenIsNull_ThenReturnEmpty()
            {
                var result = _sut.ConvertToQueryString(null);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenIsEmpty_ThenReturnEmpty()
            {
                var result = _sut.ConvertToQueryString(new NameValueCollection());

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenHasOnePair_ThenReturnAsString()
            {
                var nameValues = new NameValueCollection
                {
                    {"key", "value"}
                };

                var result = _sut.ConvertToQueryString(nameValues);

                Assert.That(result, Is.EqualTo("key=value"));
            }

            [Test]
            public void WhenHasTwoPairs_ThenReturnWithAmpDelimiter()
            {
                var nameValues = new NameValueCollection
                {
                    {"key1", "value1"},
                    {"key2", "value2"}
                };
                
                var result = _sut.ConvertToQueryString(nameValues);

                Assert.That(result, Is.EqualTo("key1=value1&key2=value2"));
            }
        }
    }
}
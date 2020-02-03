using System;
using System.Linq;
using System.Xml.Linq;
using ByteDev.Common.Xml;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Xml
{
    [TestFixture]
    public class XElementExtensionsTest
    {
        protected const string DoesntExistElement = "Address";

        private static XElement CreateSut()
        {
            const string xml =
                @"<Records>
                <Customer>
                    <Name>John</Name>
                    <Age>35</Age>
                </Customer>
                <Customer>
                    <Name>Peter</Name>
                </Customer>
            </Records>";

            return XDocument.Parse(xml).Descendants("Records").First();
        }            
        
        [TestFixture]
        public class HasDescendants : XElementExtensionsTest
        {
            private XElement _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = CreateSut();
            }

            [Test]
            public void WhenElementExists_ThenReturnTrue()
            {
                var result = _sut.HasDescendants("Name");

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenElementNotExists_ThenReturnFalse()
            {
                var result = _sut.HasDescendants(DoesntExistElement);

                Assert.That(result, Is.False);
            }            
        }

        [TestFixture]
        public class Read : XElementExtensionsTest
        {
            private XElement _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = CreateSut();
            }

            [Test]
            public void WhenElementExists_ThenReturnElementValue()
            {
                var result = _sut.Read("Name");

                Assert.That(result, Is.EqualTo("John"));
            }

            [Test]
            public void WhenElementNotExists_ThenThrowException()
            {
                Assert.Throws<NotSupportedException>(() => _sut.Read(DoesntExistElement));
            }
        }

        [TestFixture]
        public class ReadSoft : XElementExtensionsTest
        {
            private XElement _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = CreateSut();
            }

            [Test]
            public void WhenElementExists_ThenReturnElementValue()
            {
                var result = _sut.ReadSoft("Name");

                Assert.That(result, Is.EqualTo("John"));
            }

            [Test]
            public void WhenElementNotExists_ThenReturnEmpty()
            {
                var result = _sut.ReadSoft(DoesntExistElement);

                Assert.That(result, Is.EqualTo(string.Empty));
            }
        }

        [TestFixture]
        public class GetSingleElement : XElementExtensionsTest
        {
            private XElement _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = CreateSut();
            }

            [Test]
            public void WhenOneExists_ThenReturnElement()
            {
                var ageElement = _sut.GetSingleElement("Age");

                Assert.That(ageElement.Value, Is.EqualTo("35"));
            }

            [Test]
            public void WhenMoreThanOneElementExists_ThenThrowException()
            {
                Assert.Throws<NotSupportedException>(() => _sut.GetSingleElement("Name"));
            }

            [Test]
            public void WhenNoElementExists_ThenThrowException()
            {
                Assert.Throws<NotSupportedException>(() => _sut.GetSingleElement(DoesntExistElement));
            }
        }        
    }
}

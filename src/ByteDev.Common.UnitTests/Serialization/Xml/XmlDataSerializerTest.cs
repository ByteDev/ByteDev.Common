using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using ByteDev.Common.Serialization.Xml;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Serialization.Xml
{
    [TestFixture]
    public class XmlDataSerializerTest
    {
        protected ProductXml[] GetProductXmls()
        {
            return new[] 
            {
                new ProductXml { Code = "A11", Name = "Apple"},
                new ProductXml { Code = "A12", Name = "Microsoft" },
                new ProductXml { Code = "A13", Name = "Google" }
            };
        }

        private static XmlDataSerializer CreateSut()
        {
            return new XmlDataSerializer();
        }

        private static XmlDataSerializer CreateSut(XmlDataSerializer.SerializerType type)
        {
            return new XmlDataSerializer(type);
        }

        private string SerializeToXml(object obj)
        {
            return CreateSut().Serialize(obj);
        }

        private string SerializeToXml(object obj, XmlDataSerializer.SerializerType type)
        {
            return CreateSut(type).Serialize(obj);
        }
 
        [TestFixture]
        public class Serialize : XmlDataSerializerTest
        {
            [Test]
            public void WhenArgIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => CreateSut().Serialize(null));
            }

            [Test]
            public void WhenArgIsProduct_ThenSerializeToXml()
            {
                var product = new Product { Code = "code1", Name = "name1" };

                var result = CreateSut().Serialize(product);

                Assert.That(result.IsXml(), Is.True);
            }

            [Test]
            public void WhenArgIsProductXml_ThenSerializeToXml()
            {
                var product = new ProductXml { Code = "code1", Name = "name1" };

                var result = CreateSut().Serialize(product);

                Assert.That(result.IsXml(), Is.True);
            }

            [Test]
            public void WhenArgIsArrayOfObjects_ThenSerialize()
            {
                ProductXml[] products = GetProductXmls();

                var result = CreateSut().Serialize(products);

                Assert.That(result.IsXml(), Is.True);
            }

            [Test]
            public void WhenIsProductContract_ThenSerializeToXml()
            {
                var product = new ProductContract { Code = "code1", Name = "name1" };

                var result = CreateSut(XmlDataSerializer.SerializerType.DataContract).Serialize(product);

                Assert.That(result.IsXml(), Is.True);
            }
        }

        [TestFixture]
        public class Deserialize : XmlDataSerializerTest
        {
            [Test]
            public void WhenArgIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => CreateSut().Deserialize<ProductXml>(null));
            }

            [Test]
            public void WhenSerializedXmlIsCustomer_AndTryToDeserializeToProduct_ThenThrowException()
            {
                var customer = new Customer { Name = "John" };

                var xml = SerializeToXml(customer);

                Assert.Throws<InvalidOperationException>(() => CreateSut().Deserialize<Product>(xml));
            }

            [Test]
            public void WhenSerializedXmlIsProduct_ThenDeserialize()
            {
                var product = new Product { Code = "code1", Name = "name1" };

                var xml = SerializeToXml(product);

                var result = CreateSut().Deserialize<Product>(xml);

                Assert.That(result.Code, Is.EqualTo(product.Code));
                Assert.That(result.Name, Is.EqualTo(product.Name));
            }

            [Test]
            public void WhenSerializedXmlIsProductXml_ThenDeserialize()
            {
                ProductXml[] products = GetProductXmls();

                var xml = SerializeToXml(products);

                var result = CreateSut().Deserialize<ProductXml[]>(xml);

                Assert.That(result.Length, Is.EqualTo(products.Length));
            }

            [Test]
            public void WhenSerializedXmlIsProductContract_ThenDeserialize()
            {
                var product = new ProductContract { Code = "code1", Name = "name1" };

                var xml = SerializeToXml(product, XmlDataSerializer.SerializerType.DataContract);

                var result = CreateSut(XmlDataSerializer.SerializerType.DataContract).Deserialize<ProductContract>(xml);

                Assert.That(result.Code, Is.EqualTo(product.Code));
                Assert.That(result.Name, Is.EqualTo(product.Name));
            }
        }
    }

    public class Customer
    {
        public string Name;
    }

    public class Product
    {
        public string Code;
        public string Name;
    }

    [XmlRoot("product")]
    public class ProductXml
    {
        [XmlElement("code")]
        public string Code;

        [XmlElement("name")]
        public string Name;
    }

    [DataContract]
    public class ProductContract
    {
        [DataMember] 
        public string Code;

        [DataMember] 
        public string Name;
    }
}

using System;
using System.IO;
using System.Xml;

namespace ByteDev.Common.Serialization.Xml
{
    public class XmlDataSerializer : IXmlDataSerializer
    {
        private readonly XmlSerializerAdaptor _xmlSerializerAdaptor;

        public enum SerializerType
        {
            /// <summary>
            /// System.Xml.Serialization.XmlSerializer
            /// </summary>
            Xml,
            /// <summary>
            /// System.Runtime.Serialization.DataContractSerializer
            /// </summary>
            DataContract
        }

        public XmlDataSerializer() : this(SerializerType.Xml)
        {
        }

        public XmlDataSerializer(SerializerType type)
        {
            _xmlSerializerAdaptor = new XmlSerializerAdaptor(type);
        }

        public string Serialize(object obj)
        {
            if(obj == null)
                throw new ArgumentNullException(nameof(obj));

            using (var memoryStream = new MemoryStream())
            {
                using (var reader = new StreamReader(memoryStream))
                {
                    _xmlSerializerAdaptor.Serialize(obj, memoryStream);

                    return reader.ReadToEnd();
                }
            }
        }

        public T Deserialize<T>(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var buffer = System.Text.Encoding.UTF8.GetBytes(input);

            return Deserialize<T>(buffer, System.Text.Encoding.UTF8);
        }

        public T Deserialize<T>(byte[] input, System.Text.Encoding encoding)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var memoryStream = new MemoryStream(input))
            {
                var reader = XmlDictionaryReader.CreateTextReader(memoryStream, encoding, new XmlDictionaryReaderQuotas(), null);

                return _xmlSerializerAdaptor.Deserialize<T>(reader);
            }
        }
    }
}

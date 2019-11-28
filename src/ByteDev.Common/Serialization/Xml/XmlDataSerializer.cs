using System;
using System.IO;
using System.Xml;

namespace ByteDev.Common.Serialization.Xml
{
    /// <summary>
    /// Represents a XML encoded string serializer.
    /// </summary>
    public class XmlDataSerializer : IXmlDataSerializer
    {
        private readonly XmlSerializerAdaptor _xmlSerializerAdaptor;

        /// <summary>
        /// Serializer type.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Common.Serialization.Xml.XmlDataSerializer" /> class.
        /// </summary>
        public XmlDataSerializer() : this(SerializerType.Xml)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Common.Serialization.Xml.XmlDataSerializer" /> class.
        /// </summary>
        /// <param name="type">Type of serializer to use.</param>
        public XmlDataSerializer(SerializerType type)
        {
            _xmlSerializerAdaptor = new XmlSerializerAdaptor(type);
        }

        /// <summary>
        /// Serializes a object to a XML string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized XML representation of <paramref name="obj" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="obj" /> is null.</exception>
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

        /// <summary>
        /// Deserialize a serialized XML representation to type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="input">Serialized XML string representation.</param>
        /// <returns>Deserialized type.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="input" /> is null.</exception>
        public T Deserialize<T>(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var buffer = System.Text.Encoding.UTF8.GetBytes(input);

            return Deserialize<T>(buffer, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// Deserialize a serialized XML representation to type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="input">Serialized XML string representation.</param>
        /// <param name="encoding">Encoding type to use.</param>
        /// <returns>Deserialized type.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="input" /> is null.</exception>
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

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ByteDev.Common.Serialization.Xml
{
    /// <summary>
    /// Responsible for adapting the Serialize/Deserialize calls
    /// to the correct serializer type.
    /// </summary>
    internal class XmlSerializerAdaptor
    {
        private readonly XmlDataSerializer.SerializerType _type;

        public XmlSerializerAdaptor(XmlDataSerializer.SerializerType type)
        {
            _type = type;
        }

        public void Serialize(object obj, MemoryStream memoryStream)
        {
            if (_type == XmlDataSerializer.SerializerType.DataContract)
            {
                var serializer = CreateDataContractSerializer(obj.GetType());
                serializer.WriteObject(memoryStream, obj);
            }
            else
            {
                var serializer = CreateXmlSerializerFor(obj.GetType());
                serializer.Serialize(memoryStream, obj);
            }

            memoryStream.Position = 0;
        }

        public T Deserialize<T>(XmlDictionaryReader reader)
        {
            if (_type == XmlDataSerializer.SerializerType.DataContract)
            {
                var serializer = CreateDataContractSerializer(typeof(T));
                return (T)serializer.ReadObject(reader);
            }
            else
            {
                var serializer = CreateXmlSerializerFor(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        private static XmlSerializer CreateXmlSerializerFor(Type type)
        {
            return new XmlSerializer(type);
        }

        private static DataContractSerializer CreateDataContractSerializer(Type type)
        {
            return new DataContractSerializer(type);
        }
    }
}
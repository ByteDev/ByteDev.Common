namespace ByteDev.Common.Serialization.Xml
{
    public interface IXmlDataSerializer : ISerializer
    {
        T Deserialize<T>(byte[] input, System.Text.Encoding encoding);
    }
}
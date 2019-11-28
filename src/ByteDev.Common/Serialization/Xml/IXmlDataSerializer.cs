namespace ByteDev.Common.Serialization.Xml
{
    /// <summary>
    /// Provides a way to serialize and deserialize objects.
    /// </summary>
    public interface IXmlDataSerializer : ISerializer
    {
        /// <summary>
        /// Deserialize a serialized XML representation to type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="input">Serialized XML string representation.</param>
        /// <param name="encoding">Encoding type to use.</param>
        /// <returns>Deserialized type.</returns>
        T Deserialize<T>(byte[] input, System.Text.Encoding encoding);
    }
}
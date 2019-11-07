using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace ByteDev.Common.Serialization.Base64
{
    /// <summary>
    /// Represents a base64 encoded string serializer.
    /// </summary>
    public class Base64Serializer : IBase64Serializer
    {
        /// <summary>
        /// Serializes a object to a compressed base64 string.
        /// </summary>
        /// <param name="obj">The object to serialize to base64.</param>
        /// <returns><paramref name="obj" /> serialized to base64 compressed string.</returns>
        public string Serialize(object obj)
        {
            using (var stream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();

                // Convert object to a stream
                binaryFormatter.Serialize(stream, obj);

                var bytes = Compress(stream);
                return Convert.ToBase64String(bytes);
            }
        }

        /// <summary>
        /// Deserializes a compressed base64 string to object of the given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="input">The compressed base64 string to deserialize.</param>
        /// <returns><paramref name="input" /> deserialized to object of type <typeparamref name="T" />.</returns>
        public T Deserialize<T>(string input)
        {
            var compressedBytes = Convert.FromBase64String(input);

            using (var stream = new MemoryStream())
            {
                Decompress(compressedBytes, stream);
                stream.Position = 0;
                var formatter = new BinaryFormatter();

                return (T)formatter.Deserialize(stream);
            }
        }

        private static byte[] Compress(Stream stream)
        {
            using (var resultStream = new MemoryStream())
            {
                using (var writeStream = new GZipStream(resultStream, CompressionMode.Compress, true))
                {
                    CopyBuffered(stream, writeStream);
                }

                var resultArray = resultStream.ToArray();

                return resultArray;
            }
        }

        private static void Decompress(byte[] compressedBytes, Stream outputStream)
        {
            var memoryStream = new MemoryStream(compressedBytes);

            try
            {
                using (var readStream = new GZipStream(memoryStream, CompressionMode.Decompress, true))
                {
                    memoryStream = null;
                    CopyBuffered(readStream, outputStream);
                }
            }
            finally
            {
                memoryStream?.Dispose();
            }
        }

        private static void CopyBuffered(Stream readStream, Stream writeStream)
        {
            if (readStream.CanSeek)
                readStream.Position = 0;

            var bytes = new byte[4096];
            int byteCount;

            while ((byteCount = readStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                writeStream.Write(bytes, 0, byteCount);
            }
        }
    }
}

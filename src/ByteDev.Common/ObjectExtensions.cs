using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ByteDev.Common
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Performs a deep copy of the serializable source object. 
        /// </summary>
        /// <typeparam name="TSource">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to deep clone.</param>
        /// <returns>The cloned object.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="source" /> must be serializable.</exception>
        public static TSource CloneSerializable<TSource>(this TSource source)
        {
            if (!typeof(TSource).IsSerializable)
                throw new ArgumentException("The type must be serializable.");

            if (IsReferencingNull(source))
                return default(TSource);
            
            IFormatter formatter = new BinaryFormatter();
            
            using (Stream stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (TSource)formatter.Deserialize(stream);
            }
        }

        private static bool IsReferencingNull<TSource>(TSource source)
        {
            return ReferenceEquals(source, null);
        }
    }
}
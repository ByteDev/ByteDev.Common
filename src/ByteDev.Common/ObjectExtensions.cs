using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ByteDev.Common
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Perform a deep Copy of the source object
        /// Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
        /// </summary>
        /// <typeparam name="T">The type of object being copied</typeparam>
        /// <param name="source">The object instance to copy</param>
        /// <returns>The cloned object</returns>
        public static T CloneSerializable<T>(this T source)
        {
            CheckTypeIsSerializable<T>();

            if (IsReferencingNull(source))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        private static bool IsReferencingNull<T>(T source)
        {
            return ReferenceEquals(source, null);
        }

        private static void CheckTypeIsSerializable<T>()
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.");
            }
        }
    }
}
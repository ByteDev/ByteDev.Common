using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ByteDev.Common
{
    /// <summary>
    /// Extension methods for generic type.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Invokes a method on a type using reflection.
        /// </summary>
        /// <typeparam name="TSource">The type of the object to invoke the method on.</typeparam>
        /// <param name="source">The object to invoke the method on.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="args">Any arguments to pass to the invoked method.</param>
        /// <returns>The value returned from the invoked method. If the return type is void then null will be returned.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="methodName" /> does not exist.</exception>
        public static object InvokeMethod<TSource>(this TSource source, string methodName, params object[] args)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            var type = typeof(TSource);
            var method = type.GetTypeInfo().GetDeclaredMethod(methodName);

            if (method == null)
                throw new ArgumentException($"Method '{methodName}' does not exist.", nameof(methodName));

            return method.Invoke(source, args);
        }
        
        /// <summary>
        /// Checks whether a value is contained in a list of values.
        /// </summary>
        /// <typeparam name="TSource">The type of the object to check whether is contained in a list of values.</typeparam>
        /// <param name="source">The object to check whether is contained in a list of values.</param>
        /// <param name="list">The list of values to check the object is contained in.</param>
        /// <returns>True if the object is contained in the list; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="list" /> is null.</exception>
        public static bool In<TSource>(this TSource source, params TSource[] list)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            if(list == null)
                throw new ArgumentNullException(nameof(list));

            return list.ToList().Contains(source);
        }

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

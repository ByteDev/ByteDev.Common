using System;
using System.Runtime.Serialization;

namespace ByteDev.Common.Exceptions
{
    [Serializable]
    public class ArgumentNullOrEmptyException : ArgumentException
    {
        private const string DefaultMessage = "Value cannot be null or empty.";

        public ArgumentNullOrEmptyException() : base(DefaultMessage)
        {
        }

        public ArgumentNullOrEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ArgumentNullOrEmptyException(string paramName) : base(DefaultMessage, paramName)
        {
        }

        public ArgumentNullOrEmptyException(string paramName, string message) : base(message, paramName)
        {
        }

        protected ArgumentNullOrEmptyException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace ByteDev.Common.Exceptions
{
    [Serializable]
    public class UnexpectedEnumValueException<TEnum> : Exception
    {
        public UnexpectedEnumValueException()
            :base($"Unexpected value for enum '{typeof(TEnum).FullName}'.")
        {
        }

        public UnexpectedEnumValueException(string message) 
            : base(message)
        {
        }

        public UnexpectedEnumValueException(string message, Exception inner) 
            : base(message, inner)
        {
        }

        public UnexpectedEnumValueException(TEnum enumValue) 
            : base($"Unexpected value '{enumValue}' for enum '{typeof(TEnum).FullName}'.")
        {
        }

        protected UnexpectedEnumValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace ByteDev.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an unexpected enum value is encountered.
    /// </summary>
    [Serializable]
    public class UnexpectedEnumValueException<TEnum> : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Common.Exceptions.UnexpectedEnumValueException" /> class.
        /// </summary>
        public UnexpectedEnumValueException() : base($"Unexpected value for enum '{typeof(TEnum).FullName}'.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Common.Exceptions.UnexpectedEnumValueException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnexpectedEnumValueException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Common.Exceptions.UnexpectedEnumValueException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>       
        public UnexpectedEnumValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Common.Exceptions.UnexpectedEnumValueException" /> class.
        /// </summary>
        /// <param name="enumValue">The enum value that was unexpected.</param>
        public UnexpectedEnumValueException(TEnum enumValue) : base($"Unexpected value '{enumValue}' for enum '{typeof(TEnum).FullName}'.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Common.Exceptions.UnexpectedEnumValueException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected UnexpectedEnumValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
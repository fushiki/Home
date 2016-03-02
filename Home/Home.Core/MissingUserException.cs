using System;
using System.Runtime.Serialization;

namespace Home.Core
{
    [Serializable]
    internal class MissingUserException : Exception
    {
        public MissingUserException()
        {
        }

        public MissingUserException(string message) : base(message)
        {
        }

        public MissingUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
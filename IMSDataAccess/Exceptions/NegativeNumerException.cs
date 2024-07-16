using System.Runtime.Serialization;

namespace IMSDataAccess
{
    [Serializable]
    public class NegativeNumerException : Exception
    {
        public NegativeNumerException()
        {
        }

        public NegativeNumerException(string? message) : base(message)
        {
        }

        public NegativeNumerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NegativeNumerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
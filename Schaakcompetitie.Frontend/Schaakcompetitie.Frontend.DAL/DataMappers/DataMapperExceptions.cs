using System;
using System.Runtime.Serialization;

namespace Schaakcompetitie.Frontend.DAL.DataMappers
{
    [Serializable]
    public class DataMapperException : Exception
    {
        public DataMapperException()
        {
        }

        public DataMapperException(string message) : base(message)
        {
        }

        public DataMapperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataMapperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
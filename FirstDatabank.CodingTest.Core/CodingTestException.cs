namespace FirstDatabank.CodingTest.Core
{
    using System;
    using System.Runtime.Serialization;

    public class CodingTestException : Exception
    {
        public CodingTestException()
            : base()
        { }

        public CodingTestException(SerializationInfo info,StreamingContext context)
            : base(info, context)
        { }

        public CodingTestException(string message)
            :base( message)
        { }

        public CodingTestException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}

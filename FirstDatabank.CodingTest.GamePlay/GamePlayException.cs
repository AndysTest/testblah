namespace FirstDatabank.CodingTest.GamePlay
{
    using System;
    using System.Runtime.Serialization;
    using Core;

    public class GamePlayException : CodingTestException
    {
        public GamePlayException()
            : base()
        { }

        public GamePlayException(SerializationInfo info,StreamingContext context)
            : base(info, context)
        { }

        public GamePlayException(string message)
            :base( message)
        { }

        public GamePlayException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}

namespace FirstDatabank.CodingTest.Hci
{
    using System;

    public class KeyboardReader : IInputSource
    {
        public char GetKeyStroke()
        {
            var key = Console.ReadKey(true);

            return key.KeyChar;
        }
    }
}

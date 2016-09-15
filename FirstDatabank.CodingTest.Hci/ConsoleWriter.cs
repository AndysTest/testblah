namespace FirstDatabank.CodingTest.Hci
{
    using System;

    public class ConsoleWriter : IOutputTarget
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}

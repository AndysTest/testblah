namespace FirstDatabank.CodingTest.Hci
{
    public interface IOutputTarget
    {
        void Clear();
        void Write(string message);
        void WriteLine(string message);
    }
}
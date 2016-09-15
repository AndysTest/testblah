namespace FirstDatabank.CodingTest
{
    using GamePlay;
    using Ninject;

    class Program
    {
        static void Main(string[] args)
        {
            Dependencies.Configure();

            var game = Dependencies.Kernel.Get<IMinefieldGame>();
            var engine = Dependencies.Kernel.Get<IGameEngine>();

            engine.PlayGame(game);
        }
    }
}

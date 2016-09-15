namespace FirstDatabank.CodingTest
{
    using Hci;
    using Core;
    using Ninject;
    using GamePlay;

    public static class Dependencies
    {
        #region Constants

        #endregion // constants

        #region Members

        private static readonly IKernel _kernel = new StandardKernel();

        #endregion // members

        #region Properties

        public static IKernel Kernel { get { return _kernel; } }

        #endregion // properties

        #region Construction

        #endregion // construction

        #region Exposure

        public static void Configure()
        {
            _kernel.Bind<IMinefieldGame>().To<MinefieldGame>();
            _kernel.Bind<IMinefieldBoard>().To<MinefieldBoard>();
            _kernel.Bind<ISquareFactory>().To<SquareFactory>();
#if DEBUG
            // with this seed you can move right along starting row then up right hand column to win
            _kernel.Bind<IRandomGenerator>().To<RandomGenerator>().WithConstructorArgument("seed", 1006);
#else
            _kernel.Bind<IRandomGenerator>().To<RandomGenerator>();
#endif
            _kernel.Bind<IInputSource>().To<KeyboardReader>();
            _kernel.Bind<IOutputTarget>().To<ConsoleWriter>();
            _kernel.Bind<IGameEngine>().To<GameEngine>();
        }

        #endregion // exposure

        #region Helpers

        #endregion // helpers
    }
}

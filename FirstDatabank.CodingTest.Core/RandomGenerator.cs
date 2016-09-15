namespace FirstDatabank.CodingTest.Core
{
    using System;

    public interface IRandomGenerator
    {
        bool GenerateBool(float probability);
    }

    public class RandomGenerator : IRandomGenerator
    {
        #region Constants

        #endregion // constants

        #region Members

        private readonly Random _random;

        #endregion // members

        #region Properties

        #endregion // properties

        #region Construction

        public RandomGenerator()
            : this(null)
        { }

        public RandomGenerator(int? seed)
        {
            _random = (seed.HasValue ? new Random(seed.Value) : new Random());
        }

        #endregion // construction

        #region Exposure

        public bool GenerateBool(float probability)
        {
            return _random.NextDouble() <= probability;
        }

        #endregion // exposure

        #region Helpers

        #endregion // helpers
    }
}

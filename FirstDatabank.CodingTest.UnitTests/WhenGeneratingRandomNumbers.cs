namespace FirstDatabank.CodingTest.UnitTests
{
    using Core;
    using NUnit.Framework;

    [TestFixture]
    public class WhenGeneratingRandomNumbers
    {
        [Test]
        public void WhenGeneratingBoolWithZeroProbability()
        {
            // arrange
            const int iterations = 1000000;
            var generator = new RandomGenerator();
            var trueCount = 0;

            // act
            for (var i = 0; i < iterations; ++i)
            {
                if (generator.GenerateBool(0.0f))
                    ++trueCount;
            }

            // assert
            Assert.AreEqual(0, trueCount);
        }

        [Test]
        public void WhenGeneratingBoolWithGuaranteedProbability()
        {
            // arrange
            const int iterations = 1000000;
            var generator = new RandomGenerator();
            var trueCount = 0;

            // act
            for (var i = 0; i < iterations; ++i)
            {
                if (generator.GenerateBool(1.0f))
                    ++trueCount;
            }

            // assert
            Assert.AreEqual(iterations, trueCount);
        }

    }
}

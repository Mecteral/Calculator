using Calculator.Logic.Simplifying;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class DistributeLawConjunctionSimplifierTests
    {
        [SetUp]
        public void SetUp()
        {
            mUnderTest = new DistributeLawConjunctionSimplifier();
        }

        DistributeLawConjunctionSimplifier mUnderTest;
    }
}
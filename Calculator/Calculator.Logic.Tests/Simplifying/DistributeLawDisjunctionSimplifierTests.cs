using Calculator.Logic.Simplifying;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class DistributeLawDisjunctionSimplifierTests
    {
        [SetUp]
        public void SetUp()
        {
            mUnderTest = new DistributeLawDisjunctionSimplifier();
        }

        DistributeLawDisjunctionSimplifier mUnderTest;
    }
}
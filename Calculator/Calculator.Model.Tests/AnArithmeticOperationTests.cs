using NUnit.Framework;

namespace Calculator.Model.Tests
{
    [TestFixture(typeof(Addition))]
    [TestFixture(typeof(Subtraction))]
    [TestFixture(typeof(Multiplication))]
    [TestFixture(typeof(Division))]
    public class AnArithmeticOperationTests<T> where T: AnArithmeticOperation, new()
    {
        
    }
}
using Calculator.Model;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class DoubleDispatchTests
    {
        [Test]
        public void Usage_01()
        {
            IExpression left = new Constant();
            IExpression right = new Constant();
            var dispatcher = new Dispatcher(left, right) { FallbackHandler = (l, r) => { } };
            dispatcher.OnLeft<Constant>().OnRight<Variable>().Do((c, v) => { });
            dispatcher.OnLeft<Addition>().OnRight<Multiplication>().Do((a, m) => { });
            dispatcher.Dispatch();
        }
    }
}

using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class DispatcherTests
    {
        [Test]
        public void If_No_Fitting_Handler_Defined_Calls_FallbackHandler()
        {
            IExpression left = new Constant();
            IExpression right = new Constant();
            var wasFallbackCalled = false;
            var dispatcher = new Dispatcher(left, right) {FallbackHandler = (l, r) => wasFallbackCalled = true};
            dispatcher.OnLeft<Constant>().OnRight<Variable>().Do((c, v) => { Assert.Fail(); });
            dispatcher.OnLeft<Addition>().OnRight<Constant>().Do((a, m) => { Assert.Fail(); });
            dispatcher.Dispatch();

            wasFallbackCalled.Should().BeTrue();
        }
        [Test]
        public void Picks_Right_Handler_If_Defined()
        {
            IExpression left = new Addition();
            IExpression right = new Constant();
            var dispatcher = new Dispatcher(left, right) {FallbackHandler = (l, r) => Assert.Fail()};

            dispatcher.OnLeft<Constant>().OnRight<Variable>().Do((c, v) => { Assert.Fail(); });
            var wasRightComboPicked = false;
            dispatcher.OnLeft<Addition>().OnRight<Constant>().Do((a, m) => { wasRightComboPicked = true; });

            dispatcher.Dispatch();

            wasRightComboPicked.Should().BeTrue();
        }
        [Test]
        public void Usage_With_Unified_On()
        {
            IExpression left = new Addition();
            IExpression right = new Constant();
            var dispatcher = new Dispatcher(left, right) { FallbackHandler = (l, r) => Assert.Fail() };

            dispatcher.On<Constant, Variable>((c, v) => { Assert.Fail(); });
            var wasRightComboPicked = false;
            dispatcher.On<Addition, Constant>((a, m) => { wasRightComboPicked = true; });

            dispatcher.Dispatch();

            wasRightComboPicked.Should().BeTrue();
        }
    }
}
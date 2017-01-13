using Calculator.Model;
using FluentAssertions;
using November.MultiDispatch;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    /// <summary>
    /// These demo the usage of the DoubleDispatcher. 
    /// </summary>
    [TestFixture]
    // ReSharper disable once TestFileNameWarning
    public class UsingDispatcherTests
    {
        [Test]
        public void If_No_Fitting_Handler_Defined_Calls_FallbackHandler()
        {
            var wasFallbackCalled = false;
            var dispatcher = new DoubleDispatcher<object> {FallbackHandler = (l, r) => wasFallbackCalled = true};
            dispatcher.OnLeft<Constant>().OnRight<Variable>().Do((c, v) => { Assert.Fail(); });
            dispatcher.OnLeft<Addition>().OnRight<Constant>().Do((a, m) => { Assert.Fail(); });

            dispatcher.Dispatch(new Constant(), new Constant());

            wasFallbackCalled.Should().BeTrue();
        }
        [Test]
        public void Picks_Right_Handler_If_Defined()
        {
            var dispatcher = new DoubleDispatcher<object> {FallbackHandler = (l, r) => Assert.Fail()};

            dispatcher.OnLeft<Constant>().OnRight<Variable>().Do((c, v) => { Assert.Fail(); });
            var wasRightComboPicked = false;
            dispatcher.OnLeft<Addition>().OnRight<Constant>().Do((a, m) => { wasRightComboPicked = true; });

            dispatcher.Dispatch(new Addition(), new Constant());

            wasRightComboPicked.Should().BeTrue();
        }
        [Test]
        public void Usage_With_Unified_On()
        {
            var dispatcher = new DoubleDispatcher<object>();
            dispatcher.On<Constant, Variable>((c, v) => { Assert.Fail(); });
            var wasRightComboPicked = false;
            dispatcher.On<Addition, Constant>((a, m) => { wasRightComboPicked = true; });

            dispatcher.Dispatch(new Addition(), new Constant());

            wasRightComboPicked.Should().BeTrue();
        }
        [Test]
        public void Usage_With_Predicates()
        {
            var dispatcher = new DoubleDispatcher<object>();
            var wasHandlerCalled = false;
            dispatcher.OnLeft<Addition>().OnRight<Constant>(c => c.Value > 0).Do((a, c) => wasHandlerCalled= true);

            dispatcher.Dispatch(new Addition(), new Constant {Value = -1});
            wasHandlerCalled.Should().BeFalse();

            dispatcher.Dispatch(new Addition(), new Constant { Value = 13 });
            wasHandlerCalled.Should().BeTrue();
        }
    }
}
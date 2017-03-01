using Calculator.Logic;
using CalculatorWPFViewModels;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace ViewModelsTests
{
    [TestFixture]
    public class InputViewModelTests
    {
        IWpfCalculationExecutor mExecutor;
        InputViewModel mUnderTest;

        [SetUp]
        public void Setup()
        {
            mExecutor = Substitute.For<IWpfCalculationExecutor>();
            mUnderTest= new InputViewModel(mExecutor);
        }
        [Test]
        public void Calculate_Should_Call_Executor_With_InputString()
        {
            mUnderTest.InputString = "Alpha";
            mUnderTest.Calculate();

            mExecutor.Received().InitiateCalculation("Alpha");
        }
        [Test]
        public void Calculate_Should_Set_Result_From_Executor()
        {
            mExecutor.InitiateCalculation("Alpha").Returns("bravo");
            mUnderTest.InputString = "Alpha";
            mUnderTest.Calculate();

            mUnderTest.Result.Should().Be("bravo");
        }
        [Test]
        public void Setting_Result_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.Result = "alpha";
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.Result);
        }
    }
}
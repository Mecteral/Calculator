using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Calculator.Logic;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.WPF.ViewModels;
using Caliburn.Micro;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.WPF.ViewModelsTests
{
    [TestFixture]
    public class InputViewModelTests
    {
        [SetUp]
        public void Setup()
        {
            mExecutor = Substitute.For<IWpfCalculationExecutor>();
            mArguments = Substitute.For<IApplicationArguments>();
            mAggregator = Substitute.For<IEventAggregator>();
            mInputStringValidator = Substitute.For<InputStringValidator>();
            mUnderTest = new InputViewModel(mExecutor, mArguments, mAggregator, mInputStringValidator);
        }

        IEventAggregator mAggregator;
        IApplicationArguments mArguments;
        IWpfCalculationExecutor mExecutor;
        InputStringValidator mInputStringValidator;
        InputViewModel mUnderTest;

        [Test]
        public void Calculate_Should_Call_Executor_With_InputString()
        {
            mUnderTest.InputString = "Alpha";
            mUnderTest.Calculate();

            mExecutor.Received().InitiateCalculation("Alpha", mArguments);
        }

        [Test]
        public void Calculate_Should_Set_Result_From_Executor()
        {
            mExecutor.InitiateCalculation("Alpha", mArguments);
            mExecutor.CalculationResult = "Bravo";
            mUnderTest.Calculate();

            mUnderTest.Result.Should().Be("Bravo");
        }

        [Test]
        public void Calculate_Should_Set_Steps_From_Executor()
        {
            mExecutor.InitiateCalculation("Alpha", mArguments);
            mExecutor.CalculationSteps = new List<string> { "Bravo" };
            mUnderTest.Calculate();

            mUnderTest.Steps.Should().Contain("Bravo");
        }

        [Test]
        public void OnEnter_Calls_Calculate()
        {
            mUnderTest.InputString = "Alpha";
            var presentationSource = Substitute.For<PresentationSource>();
            var context = new ActionExecutionContext();
            context.EventArgs = new KeyEventArgs(null, presentationSource, 0, Key.Enter);
            mUnderTest.OnEnter(context);
            mExecutor.Received().InitiateCalculation("Alpha", mArguments);
        }

        [Test]
        public void Setting_Result_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.Result = "alpha";
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.Result);
        }

        [Test]
        public void Setting_Steps_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.Steps = new List<string> { "Bravo" };
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.Steps);
        }

        [Test]
        public void Setting_InputString_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.InputString = "alpha";
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.InputString);
        }

        [Test]
        public void Setting_StepExpander_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.StepExpander = false;
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.StepExpander);
        }

        [Test]
        public void StepExpander_Calls_EventAggregator()
        {
            mUnderTest.StepExpander = true;
            mAggregator.Received().PublishOnUIThread("Resize");
        }
        [Test]
        public void Setting_CalculationButtonToggle_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.CalculationButtonToggle = true;
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.CalculationButtonToggle);
        }

        [Test]
        public void InputValidation_Changes_Color_To_Black_If_Input_Validates()
        {
            mUnderTest.InputString = "2+2";
            mUnderTest.CalculationButtonToggle.Should().Be(true);
            mUnderTest.CalculationButtonForeground.Should().Be("Black");
        }

        [Test]
        public void InputValidation_Changes_Color_To_Grey_If_Input_Does_Not_Validate()
        {
            mUnderTest.InputString = "2aa+2";
            mUnderTest.CalculationButtonToggle.Should().Be(false);
            mUnderTest.CalculationButtonForeground.Should().Be("Grey");
        }

        [Test]
        public void Calculate_Uses_UseMetric_If_Conversion_Is_Active()
        {
        }
    }
}
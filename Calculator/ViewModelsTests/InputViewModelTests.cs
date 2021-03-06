﻿using System.Collections.Generic;
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

namespace ViewModelsTests
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
        InputViewModel mUnderTest;
        InputStringValidator mInputStringValidator;

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
            mExecutor.CalculationSteps = new List<string> {"Bravo"};
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
            mUnderTest.Steps = new List<string> {"Bravo"};
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.Steps);
        }
    }
}
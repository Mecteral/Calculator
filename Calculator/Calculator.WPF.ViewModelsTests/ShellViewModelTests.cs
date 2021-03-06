﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic;
using Calculator.Logic.WpfApplicationProperties;
using Calculator.WPF.ViewModels;
using Caliburn.Micro;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.WPF.ViewModelsTests
{
    [TestFixture]
    public class ShellViewModelTests
    {
        [SetUp]
        public void SetUp()
        {
            //mInputViewModel = Substitute.For<InputViewModel>();
            //mConversionViewModel = Substitute.For<ConversionViewModel>();
            //mConfigurationWindowViewModel = Substitute.For<ConfigurationWindowViewModel>();
            mEventAggregator = Substitute.For<IEventAggregator>();
            mWindowManager = Substitute.For<IWindowManager>();
            mWindowProperties = Substitute.For<IWindowProperties>();
            mConversionProperties = Substitute.For<IConversionProperties>();
            mUnderTest = new ShellViewModel(mInputViewModel,mConversionViewModel,mEventAggregator,mConfigurationWindowViewModel,mWindowManager, mWindowProperties,mConversionProperties);
        }

        IConversionProperties mConversionProperties;
        IWindowProperties mWindowProperties;
        InputViewModel mInputViewModel;
        ConversionViewModel mConversionViewModel;
        IEventAggregator mEventAggregator;
        ConfigurationWindowViewModel mConfigurationWindowViewModel;
        IWindowManager mWindowManager;
        ShellViewModel mUnderTest;

        [Test]
        public void Setting_CalculationButtonIsVisible_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.CalculationButtonIsVisible = true;
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.CalculationButtonIsVisible);
        }
        [Test]
        public void Setting_IsResizeable_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.IsResizeable = "Resize";
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.IsResizeable);
        }
        [Test]
        public void Setting_ConversionButtonIsVisible_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.ConversionButtonIsVisible = false;
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.ConversionButtonIsVisible);
        }

        [Test]
        public void If_StaticsStepExpander_Is_True_IsResizeAble_Is_Changed()
        {
            mWindowProperties.AreStepsExpanded = true;
            mUnderTest.Handle("");
            mUnderTest.IsResizeable.Should().Be("Resize");
        }
        [Test]
        public void If_StaticsStepExpander_Is_False_IsResizeAble_Is_Changed()
        {
            mWindowProperties.AreStepsExpanded = false;
            mUnderTest.Handle("");
            mUnderTest.IsResizeable.Should().Be("CanMinimize");
        }

        [Test]
        public void OnConfigurationButton_Calls_mWindowManager()
        {
            mUnderTest.OnConfigurationButton();
            mWindowManager.Received().ShowDialog(mConfigurationWindowViewModel);
        }

        [Test]
        public void OnConversionButton_Changes_ButtonVisibility()
        {
            mUnderTest.OnConversionButton();
            mUnderTest.ConversionButtonIsVisible.Should().Be(false);
            mUnderTest.CalculationButtonIsVisible.Should().Be(true);
        }

        [Test]
        public void IsResizeable_Is_Set_If_StepExpander_Is_Open()
        {
            mWindowProperties.AreStepsExpanded.Returns(true);
            var underTest = new ShellViewModel(mInputViewModel,mConversionViewModel,mEventAggregator,mConfigurationWindowViewModel,mWindowManager,mWindowProperties,mConversionProperties);
            underTest.IsResizeable.Should().Be("CanResize");
        }
        [Test]
        public void OnConversionButtonIsCalled_If_Conversion_Is_Active()
        {
            mConversionProperties.IsConversionActive.Returns(true);
            var underTest = new ShellViewModel(mInputViewModel, mConversionViewModel, mEventAggregator, mConfigurationWindowViewModel, mWindowManager, mWindowProperties, mConversionProperties);
            underTest.ConversionButtonIsVisible.Should().Be(false);
            underTest.CalculationButtonIsVisible.Should().Be(true);
        }

        [Test]
        public void OnCloseButton_Reaches_TryClose()
        {
            mUnderTest.OnCloseButton();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.WpfApplicationProperties;
using Calculator.WPF.ViewModels;
using Caliburn.Micro;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.WPF.ViewModelsTests
{
    [TestFixture]
    public class ConversionViewModelTests
    {
        [SetUp]
        public void SetUp()
        {
            mEventAggregator = Substitute.For<IEventAggregator>();
            mWindowProperties = Substitute.For<IWindowProperties>();
            mConversionProperties = Substitute.For<IConversionProperties>();
            mUnderTest = new ConversionViewModel(mEventAggregator, mWindowProperties, mConversionProperties);
        }

        IEventAggregator mEventAggregator;
        IWindowProperties mWindowProperties;
        IConversionProperties mConversionProperties;
        ConversionViewModel mUnderTest;

        [Test]
        public void ToMetric_Is_Set_On_StartUp()
        {
            mUnderTest.ToMetric.Should().Be(false);
        }
        [Test]
        public void ToImperial_Is_Set_On_StartUp()
        {
            mUnderTest.ToImperial.Should().Be(true);
        }

        [Test]
        public void UnitExpander_Calls_EventAggregator()
        {
            mUnderTest.UnitExpander = true;
            mEventAggregator.Received().PublishOnUIThread("Resize");
        }
        [Test]
        public void Setting_UnitExpander_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.UnitExpander = true;
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.UnitExpander);
            mUnderTest.UnitExpander.Should().Be(true);
        }
        [Test]
        public void Setting_ToMetric_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.ToMetric = true;
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.ToMetric);
            mUnderTest.ToMetric.Should().Be(true);
        }
        [Test]
        public void Setting_ToImperial_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.ToImperial = false;
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.ToImperial);
            mUnderTest.ToImperial.Should().Be(false);
        }

        [Test]
        public void UnitAbbeviation_Is_Set_On_StartUp()
        {
            mConversionProperties.LastPickedUnit.Returns("ml");
            var underTest = new ConversionViewModel(mEventAggregator, mWindowProperties, mConversionProperties);
            foreach (var allUnitsAndAbbreviation in underTest.AllUnitsAndAbbreviations)
            {
                foreach (var unit in allUnitsAndAbbreviation)
                {
                    if (unit.Abbreviation == "ml")
                    {
                        unit.IsSelected.Should().Be(true);
                    }
                }
            }
        }
    }
}

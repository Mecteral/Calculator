using Calculator.Logic.WpfApplicationProperties;
using Calculator.WPF.ViewModels;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.WPF.ViewModelsTests
{
    [TestFixture]
    public class ConfigurationOptionTabViewModelTests
    {
        [SetUp]
        public void SetUp()
        {
            mWindowProperties = Substitute.For<IWindowProperties>();
            mUnderTest = new ConfigurationOptionTabViewModel(mWindowProperties);
        }

        IWindowProperties mWindowProperties;
        ConfigurationOptionTabViewModel mUnderTest;

        [Test]
        public void DisplayName_Should_Be_Options()
        {
            mUnderTest.DisplayName.Should().Be("Options");
        }

        [Test]
        public void FontSize_Increase_Changes_WindowProperties_FontSize()
        {
            mUnderTest.IncreaseFontSize();
            mWindowProperties.FontSize.Should().Be(1);
        }
        [Test]
        public void FontSize_Decrease_Changes_WindowProperties_FontSize_Not_Underneath_0()
        {
            mUnderTest.DecreaseFontSize();
            mWindowProperties.FontSize.Should().Be(0);
        }

        [Test]
        public void FontSize_Decrease_Changes_WindowProperties_FontSize()
        {
            mWindowProperties.FontSize = 12;
            var underTest = new ConfigurationOptionTabViewModel(mWindowProperties);
            underTest.DecreaseFontSize();
            mWindowProperties.FontSize.Should().Be(11);
        }

        [Test]
        public void Setting_FontSelection_Notifies()
        {
            mUnderTest.MonitorEvents();
            mUnderTest.FontSelection = "";
            mUnderTest.ShouldRaisePropertyChangeFor(i => i.FontSelection);
            mUnderTest.FontSelection.Should().Be("");
        }
    }
}
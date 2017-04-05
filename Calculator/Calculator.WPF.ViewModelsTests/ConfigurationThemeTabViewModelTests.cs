using Calculator.Logic.WpfApplicationProperties;
using Calculator.WPF.ViewModels;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.WPF.ViewModelsTests
{
    [TestFixture]
    public class ConfigurationThemeTabViewModelTests
    {
        [SetUp]
        public void SetUp()
        {
            mWindowProperties = Substitute.For<IWindowProperties>();
            mUnderTest = new ConfigurationThemeTabViewModel(mWindowProperties);
        }

        IWindowProperties mWindowProperties;
        ConfigurationThemeTabViewModel mUnderTest;

        [Test]
        public void DisplayName_Is_Color_And_Fonts_And_Themes()
        {
            mUnderTest.DisplayName.Should().Be("Colors, Font and Themes");
        }

        [Test]
        public void BureauBlack_Changes_UsedWpfTheme()
        {
            var underTest = new ConfigurationThemeTabViewModel(mWindowProperties);
            underTest.BureauBlack();
            mWindowProperties.UsedWpfTheme.Should().Be("Themes\\BureauBlack.xaml");
        }
        [Test]
        public void BureauBlue_Changes_UsedWpfTheme()
        {
            var underTest = new ConfigurationThemeTabViewModel(mWindowProperties);
            underTest.BureauBlue();
            mWindowProperties.UsedWpfTheme.Should().Be("Themes\\BureauBlue.xaml");
        }
        [Test]
        public void ExpressionDark_Changes_UsedWpfTheme()
        {
            var underTest = new ConfigurationThemeTabViewModel(mWindowProperties);
            underTest.ExpressionDark();
            mWindowProperties.UsedWpfTheme.Should().Be("Themes\\ExpressionDark.xaml");
        }
        [Test]
        public void ExpressionLight_Changes_UsedWpfTheme()
        {
            var underTest = new ConfigurationThemeTabViewModel(mWindowProperties);
            underTest.ExpressionLight();
            mWindowProperties.UsedWpfTheme.Should().Be("Themes\\ExpressionLight.xaml");
        }
    }
}
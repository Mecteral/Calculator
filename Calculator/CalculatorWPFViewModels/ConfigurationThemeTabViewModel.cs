using System;
using System.Windows;
using Calculator.Logic;
using Calculator.Logic.WpfApplicationProperties;
using Caliburn.Micro;

namespace Calculator.WPF.ViewModels
{
    public sealed class ConfigurationThemeTabViewModel : Screen, IMainScreenTabItem
    {
        readonly IWindowProperties mWindowProperties;

        public ConfigurationThemeTabViewModel(IWindowProperties windowProperties)
        {
            mWindowProperties = windowProperties;
            DisplayName = "Colors, Font and Themes";
        }

        public void BureauBlack()
        {
            mWindowProperties.UsedWpfTheme = "Themes\\BureauBlack.xaml";
        }
        public void BureauBlue()
        {
            mWindowProperties.UsedWpfTheme = "Themes\\BureauBlue.xaml";
        }
        public void ExpressionDark()
        {
            mWindowProperties.UsedWpfTheme = "Themes\\ExpressionDark.xaml";
        }
        public void ExpressionLight()
        {
            mWindowProperties.UsedWpfTheme = "Themes\\ExpressionLight.xaml";
        }
    }
}
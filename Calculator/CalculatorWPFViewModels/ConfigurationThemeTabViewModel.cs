using System;
using System.Windows;
using Calculator.Logic;
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
            Application.Current.Resources.Source = new Uri("Themes\\BureauBlack.xaml", UriKind.RelativeOrAbsolute);
            mWindowProperties.UsedWpfTheme = "Themes\\BureauBlack.xaml";
        }
        public void BureauBlue()
        {
            Application.Current.Resources.Source = new Uri("Themes\\BureauBlue.xaml", UriKind.RelativeOrAbsolute);
            mWindowProperties.UsedWpfTheme = "Themes\\BureauBlue.xaml";
        }
        public void ExpressionDark()
        {
            Application.Current.Resources.Source = new Uri("Themes\\ExpressionDark.xaml", UriKind.RelativeOrAbsolute);
            mWindowProperties.UsedWpfTheme = "Themes\\ExpressionDark.xaml";
        }
        public void ExpressionLight()
        {
            Application.Current.Resources.Source = new Uri("Themes\\ExpressionLight.xaml", UriKind.RelativeOrAbsolute);
            mWindowProperties.UsedWpfTheme = "Themes\\ExpressionLight.xaml";
        }
    }
}
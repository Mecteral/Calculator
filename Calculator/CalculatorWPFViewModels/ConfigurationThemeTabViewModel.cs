using System;
using System.Windows;
using Calculator.Logic;
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public sealed class ConfigurationThemeTabViewModel : Screen, IMainScreenTabItem
    {
        public ConfigurationThemeTabViewModel()
        {
            DisplayName = "Colors, Font and Themes";
        }

        public void BureauBlack()
        {
            Application.Current.Resources.Source = new Uri("Themes\\BureauBlack.xaml", UriKind.RelativeOrAbsolute);
            WpfApplicationStatics.UsedWpfTheme = "Themes\\BureauBlack.xaml";
        }
        public void BureauBlue()
        {
            Application.Current.Resources.Source = new Uri("Themes\\BureauBlue.xaml", UriKind.RelativeOrAbsolute);
            WpfApplicationStatics.UsedWpfTheme = "Themes\\BureauBlue.xaml";
        }
        public void ExpressionDark()
        {
            Application.Current.Resources.Source = new Uri("Themes\\ExpressionDark.xaml", UriKind.RelativeOrAbsolute);
            WpfApplicationStatics.UsedWpfTheme = "Themes\\ExpressionDark.xaml";
        }
        public void ExpressionLight()
        {
            Application.Current.Resources.Source = new Uri("Themes\\ExpressionLight.xaml", UriKind.RelativeOrAbsolute);
            WpfApplicationStatics.UsedWpfTheme = "Themes\\ExpressionLight.xaml";
        }
    }
}
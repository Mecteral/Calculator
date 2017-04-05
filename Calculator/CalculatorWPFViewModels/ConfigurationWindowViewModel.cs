using System;
using System.Windows;
using Calculator.Logic.WpfApplicationProperties;
using Caliburn.Micro;
using FontStyle = System.Drawing.FontStyle;

namespace Calculator.WPF.ViewModels
{
    public class ConfigurationWindowViewModel : Screen
    {
        readonly IWindowProperties mWindowProperties;

        public ConfigurationWindowViewModel(ConfigurationViewModel configuration, IWindowProperties windowProperties)
        {
            mWindowProperties = windowProperties;
            ConfigurationTabs = configuration;
        }

        public ConfigurationViewModel ConfigurationTabs { get; private set; }


        public void CloseWithOkay()
        {
            var themeUri = mWindowProperties.UsedWpfTheme;
            var fontSize = mWindowProperties.FontSize;
            var font = mWindowProperties.Font;

            if (fontSize >0)
                Application.Current.MainWindow.FontSize = fontSize;
            Application.Current.Resources.Source = new Uri(themeUri, UriKind.RelativeOrAbsolute);
            TryClose(true);
        }

        public void CloseWithCancel()

        {
            TryClose(false);
        }
    }
}
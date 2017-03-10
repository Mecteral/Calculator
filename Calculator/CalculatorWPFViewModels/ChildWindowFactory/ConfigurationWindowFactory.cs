using System.Windows;

namespace CalculatorWPFViewModels.ChildWindowFactory
{
    public class ConfigurationWindowFactory : IWindowFactory
    {
        readonly ConfigurationWindowViewModel mConfigurationWindow;

        public ConfigurationWindowFactory(ConfigurationWindowViewModel configurationWindow)
        {
            mConfigurationWindow = configurationWindow;
        }

        #region Implementation of INewWindowFactory

        public void CreateNewWindow()
        {
            Window window = new Window()
            {
                DataContext = mConfigurationWindow
            };
            window.Show();
        }

        #endregion
    }

    public interface IWindowFactory
    {
        void CreateNewWindow();
    }
}
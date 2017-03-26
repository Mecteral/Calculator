using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ConfigurationWindowViewModel : Screen
    {
        public ConfigurationWindowViewModel(ConfigurationViewModel configuration)
        {
            ConfigurationTabs = configuration;
        }

        public ConfigurationViewModel ConfigurationTabs { get; private set; }

        public void ChangeData() {}

        public void CloseWithOkay()
        {
            TryClose(true);
        }

        public void CloseWithCancel()

        {
            TryClose(false);
        }
    }
}
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ConfigurationWindowViewModel : Screen
    {
        public ConfigurationWindowViewModel(ConfigurationViewModel configuration)
        {
            Configuration = configuration;
        }

        public ConfigurationViewModel Configuration { get; private set; }

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
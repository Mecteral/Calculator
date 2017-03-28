using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public sealed class ConfigurationOptionTabViewModel : Screen, IMainScreenTabItem
    {
        public ConfigurationOptionTabViewModel()
        {
            DisplayName = "Options";
        }
    }
}
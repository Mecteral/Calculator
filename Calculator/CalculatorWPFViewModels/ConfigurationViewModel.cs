using System.Collections.Generic;
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ConfigurationViewModel : Conductor<IMainScreenTabItem>.Collection.OneActive
    {
        public ConfigurationOptionTabViewModel ConfigurationOptionTab { get; set; }
        public ConfigurationThemeTabViewModel ConfigurationThemeTab { get; set; }

        public ConfigurationViewModel(IEnumerable<IMainScreenTabItem> tabs, ConfigurationOptionTabViewModel configurationOptionTab, ConfigurationThemeTabViewModel configurationThemeTab)
        {
            ConfigurationOptionTab = configurationOptionTab;
            ConfigurationThemeTab = configurationThemeTab;
            Items.AddRange(tabs);
        }
    }
}
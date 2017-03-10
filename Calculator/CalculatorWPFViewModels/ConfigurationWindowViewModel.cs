using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWPFViewModels
{
    public class ConfigurationWindowViewModel
    {
        public ConfigurationViewModel Configuration { get; private set; }

        public ConfigurationWindowViewModel(ConfigurationViewModel configuration)
        {
            Configuration = configuration;
        }
    }
}

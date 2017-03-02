using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel(InputViewModel input, ConversionAndCalculationButtonViewModel button, ConversionViewModel conversion)
        {
            Input = input;
            ConversionAndCalculationButton = button;
            Conversion = conversion;
        }
        public InputViewModel Input { get; private set; }
        public ConversionAndCalculationButtonViewModel ConversionAndCalculationButton { get; private set; }
        public ConversionViewModel Conversion { get; set; }
    }
}
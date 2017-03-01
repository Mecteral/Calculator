using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel(InputViewModel input, ConversionAndCalculationButtonViewModel button)
        {
            Input = input;
            ConversionAndCalculationButton = button;
        }
        public InputViewModel Input { get; private set; }
        public ConversionAndCalculationButtonViewModel ConversionAndCalculationButton { get; private set; }
    }
}
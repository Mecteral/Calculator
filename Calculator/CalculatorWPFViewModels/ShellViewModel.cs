using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel(InputViewModel input, ResultViewModel result, ConversionAndCalculationButtonViewModel button)
        {
            Input = input;
            Result = result;
            ConversionAndCalculationButton = button;
        }
        public InputViewModel Input { get; private set; }
        public ResultViewModel Result { get; private set; }
        public ConversionAndCalculationButtonViewModel ConversionAndCalculationButton { get; private set; }
    }
}
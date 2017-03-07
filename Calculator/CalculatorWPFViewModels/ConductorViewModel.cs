using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ConductorViewModel : Conductor<object>, IConductorViewModel
    {
        public ConversionButtonViewModel ConversionButton { get; private set; }
        public ConversionViewModel Conversion { get; set; }
        public ConductorViewModel(ConversionButtonViewModel button, ConversionViewModel conversion)
        {
            ConversionButton = button;
            Conversion = conversion;
            ShowConversionView();
        }
        public void ShowConversionButton()
        {
            ActivateItem(ConversionButton);
        }

        public void ShowConversionView()
        {
            ActivateItem(Conversion);
        }
    }

    public interface IConductorViewModel
    {
        void ShowConversionButton();
        void ShowConversionView();
    }
}
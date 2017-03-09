using Calculator.Logic;
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<string>
    {
        bool mCalculationButtonIsVisible;
        bool mConversionButtonIsVisible;
        string mIsResizeable;
        string mWindowName = "Calculator";

        public ShellViewModel(InputViewModel input, ConversionViewModel conversion, IEventAggregator aggregator)
        {
            aggregator.Subscribe(this);
            Input = input;
            Conversion = conversion;
            ConversionButtonIsVisible = true;
            CalculationButtonIsVisible = false;
            IsResizeable = "CanMinimize";
        }

        public bool CalculationButtonIsVisible
        {
            get { return mCalculationButtonIsVisible; }
            set
            {
                if (value == mCalculationButtonIsVisible) return;
                mCalculationButtonIsVisible = value;
                NotifyOfPropertyChange(() => CalculationButtonIsVisible);
            }
        }

        public string WindowName
        {
            get { return mWindowName; }
            set
            {
                if (value == mWindowName) return;
                mWindowName = value;
                NotifyOfPropertyChange(() => WindowName);
            }
        }

        public string IsResizeable
        {
            get { return mIsResizeable; }
            set
            {
                if (value == mIsResizeable) return;
                mIsResizeable = value;
                NotifyOfPropertyChange(() => IsResizeable);
            }
        }

        public bool ConversionButtonIsVisible
        {
            get { return mConversionButtonIsVisible; }
            set
            {
                if (value == mConversionButtonIsVisible) return;
                mConversionButtonIsVisible = value;
                NotifyOfPropertyChange(() => ConversionButtonIsVisible);
            }
        }

        public InputViewModel Input { get; private set; }
        public ConversionViewModel Conversion { get; set; }

        public void Handle(string message)
        {
            if (WpfApplicationStatics.StepExpander || WpfApplicationStatics.UnitExpander)
            {
                IsResizeable = "Resize";
            }
            else
            {
                IsResizeable = "CanMinimize";
            }
        }

        public void OnConversionButton()
        {
            ConversionButtonIsVisible = false;
            CalculationButtonIsVisible = true;
            WpfApplicationStatics.IsConversionActive = true;
            ShowConversionView();
        }

        public void OnCalculationButton()
        {
            ConversionButtonIsVisible = true;
            CalculationButtonIsVisible = false;
            WpfApplicationStatics.IsConversionActive = false;
            DeactivateConversionView();
        }

        public void ShowConversionView()
        {
            ActivateItem(Conversion);
        }

        public void DeactivateConversionView()
        {
            DeactivateItem(Conversion, false);
        }
    }
}
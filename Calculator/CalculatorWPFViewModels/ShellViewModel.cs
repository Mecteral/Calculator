using System;
using Calculator.Logic;
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        bool mConversionButtonIsVisible;
        bool mCalculationButtonIsVisible;
        string mIsResizeable;

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

        public static string WindowName { get; set; } = "Calculator";

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

        public ShellViewModel(InputViewModel input, ConversionViewModel conversion)
        {
            Input = input;
            Conversion = conversion;
            ConversionButtonIsVisible = true;
            CalculationButtonIsVisible = false;
            IsResizeable = "noResize";
        }
        public InputViewModel Input { get; private set; }
        public ConversionViewModel Conversion { get; set; }

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
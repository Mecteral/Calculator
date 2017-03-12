using Calculator.Logic;
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<string>
    {
        readonly ConfigurationWindowViewModel mConfigurationWindow;
        readonly IWindowManager mWindowManager;
        bool mCalculationButtonIsVisible;
        bool mConversionButtonIsVisible = true;
        string mIsResizeable = "CanMinimize";
        string mWindowName = "Calculator";
        int mWindowHeight = WpfApplicationStatics.ShellWindowHeight;
        int mWindowWidth = WpfApplicationStatics.ShellWindowWidth;

        public int WindowHeight
        {
            get { return mWindowHeight; }
            set
            {
                if (value == mWindowHeight) return;
                mWindowHeight = value;
                NotifyOfPropertyChange(() => WindowHeight);
            }
        }

        public int WindowWidth
        {
            get { return mWindowWidth; }
            set
            {
                if (value == mWindowWidth) return;
                mWindowWidth = value;
                NotifyOfPropertyChange(() => WindowWidth);
            }
        }

        public ShellViewModel(InputViewModel input, ConversionViewModel conversion, IEventAggregator eventAggregator,
            ConfigurationWindowViewModel configurationWindow, IWindowManager windowManager)
        {
            mConfigurationWindow = configurationWindow;
            mWindowManager = windowManager;
            Input = input;
            Conversion = conversion;
            eventAggregator.Subscribe(this);
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
        public ConversionViewModel Conversion { get; private set; }

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

        public void OnConfigurationButton()
        {
            mWindowManager.ShowDialog(mConfigurationWindow);
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
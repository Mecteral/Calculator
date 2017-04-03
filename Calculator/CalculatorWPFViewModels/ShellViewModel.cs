using Calculator.Logic;
using Caliburn.Micro;

namespace Calculator.WPF.ViewModels
{
    public class ShellViewModel : Conductor<object>, IScreen, IHandle<string>
    {
        readonly ConfigurationWindowViewModel mConfigurationWindow;
        readonly IWindowManager mWindowManager;
        bool mCalculationButtonIsVisible;
        bool mConversionButtonIsVisible = true;
        string mIsResizeable;
        int mWindowHeight = WpfApplicationStatics.ShellWindowHeight;
        int mWindowWidth = WpfApplicationStatics.ShellWindowWidth;

        public void OnCloseButton()
        {
            TryClose();
        }

        public ShellViewModel(InputViewModel input, ConversionViewModel conversion, IEventAggregator eventAggregator,
            ConfigurationWindowViewModel configurationWindow, IWindowManager windowManager)
        {
            mConfigurationWindow = configurationWindow;
            mWindowManager = windowManager;
            Input = input;
            Conversion = conversion;
            eventAggregator.Subscribe(this);
            SetupWindowAttributesFromConfig();
        }

        void SetupWindowAttributesFromConfig()
        {
            if (WpfApplicationStatics.StepExpander || WpfApplicationStatics.UnitExpander)
                mIsResizeable = "CanResize";
            else
                mIsResizeable = "CanMinimize";
            if (WpfApplicationStatics.IsConversionActive)
                OnConversionButton();
            else
                OnCalculationButton();
            if (WpfApplicationStatics.ShellWindowWidth < 500)
                mWindowWidth = 500;
            if (WpfApplicationStatics.ShellWindowHeight < 250)
                mWindowHeight = 250;
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
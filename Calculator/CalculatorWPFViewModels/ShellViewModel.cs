﻿using Calculator.Logic.WpfApplicationProperties;
using Caliburn.Micro;

namespace Calculator.WPF.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<string>
    {
        readonly ConfigurationWindowViewModel mConfigurationWindow;
        readonly IConversionProperties mConversionProperties;
        readonly IWindowManager mWindowManager;
        readonly IWindowProperties mWindowProperties;
        bool mCalculationButtonIsVisible;
        bool mConversionButtonIsVisible = true;
        string mIsResizeable;
        public ShellViewModel(
            InputViewModel input,
            ConversionViewModel conversion,
            IEventAggregator eventAggregator,
            ConfigurationWindowViewModel configurationWindow,
            IWindowManager windowManager,
            IWindowProperties windowProperties,
            IConversionProperties conversionProperties)
        {
            mConfigurationWindow = configurationWindow;
            mWindowManager = windowManager;
            mWindowProperties = windowProperties;
            mConversionProperties = conversionProperties;
            Input = input;
            Conversion = conversion;
            eventAggregator.Subscribe(this);
            SetupWindowAttributesFromConfig();
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
            if (mWindowProperties.AreStepsExpanded || mWindowProperties.AreUnitsExpanded) { IsResizeable = "Resize"; }
            else { IsResizeable = "CanMinimize"; }
        }
        public void OnCloseButton()
        {
            TryClose();
        }
        void SetupWindowAttributesFromConfig()
        {
            if (mWindowProperties.AreStepsExpanded || mWindowProperties.AreUnitsExpanded) mIsResizeable = "CanResize";
            else mIsResizeable = "CanMinimize";
            if (mConversionProperties.IsConversionActive) OnConversionButton();
            else OnCalculationButton();
            if (mWindowProperties.ShellWindowWidth < 500) { }
            if (mWindowProperties.ShellWindowHeight < 250) { }
        }
        public void OnConfigurationButton()
        {
            mWindowManager.ShowDialog(mConfigurationWindow);
        }
        public void OnConversionButton()
        {
            ConversionButtonIsVisible = false;
            CalculationButtonIsVisible = true;
            mConversionProperties.IsConversionActive = true;
            ShowConversionView();
        }
        public void OnCalculationButton()
        {
            ConversionButtonIsVisible = true;
            CalculationButtonIsVisible = false;
            mConversionProperties.IsConversionActive = false;
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
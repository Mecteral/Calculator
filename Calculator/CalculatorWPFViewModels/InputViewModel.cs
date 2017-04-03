using System.Collections.Generic;
using System.Windows.Input;
using Calculator.Logic;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Caliburn.Micro;

namespace Calculator.WPF.ViewModels
{
    public class InputViewModel : PropertyChangedBase
    {
        readonly IApplicationArguments mArguments;
        readonly IEventAggregator mEventAggregator;
        readonly InputStringValidator mValidator;
        readonly IWpfCalculationExecutor mExecutor;
        string mInputString;
        string mResult;
        bool mStepExpander = WpfApplicationStatics.StepExpander;
        List<string> mSteps = new List<string>();
        bool mCalculationButtonToggle;
        string mCalculationButtonForeground = "grey";

        public InputViewModel(IWpfCalculationExecutor executor, IApplicationArguments arguments,
            IEventAggregator eventAggregator, InputStringValidator validator)
        {
            mExecutor = executor;
            mArguments = arguments;
            mEventAggregator = eventAggregator;
            mValidator = validator;
        }

        public List<string> Steps
        {
            get { return mSteps; }

            set
            {
                if (value == mSteps) return;
                mSteps = value;
                NotifyOfPropertyChange(() => Steps);
            }
        }

        public string Result
        {
            get { return mResult; }

            set
            {
                if (value == mResult) return;
                mResult = value;
                NotifyOfPropertyChange(() => Result);
            }
        }

        public string CalculationButtonForeground
        {
            get { return mCalculationButtonForeground; }
            set
            {
                if (value == mCalculationButtonForeground) return;
                mCalculationButtonForeground = value;
                NotifyOfPropertyChange(() => CalculationButtonForeground);
            }
        }

        public bool StepExpander
        {
            get { return mStepExpander; }
            set
            {
                if (value == mStepExpander) return;
                mStepExpander = value;
                NotifyOfPropertyChange(() => StepExpander);
                WpfApplicationStatics.StepExpander = value;
                mEventAggregator.PublishOnUIThread("Resize");
            }
        }

        public bool CalculationButtonToggle
        {
            get { return mCalculationButtonToggle; }
            set
            {
                if (value == mCalculationButtonToggle) return;
                mCalculationButtonToggle = value;
                NotifyOfPropertyChange(() => CalculationButtonToggle);
            }
        }

        void InputValidation(string input)
        {
            try
            {
                mValidator.Validate(input);
                CalculationButtonToggle = true;
                CalculationButtonForeground = "Black";
            }
            catch (CalculationException x)
            {
                CalculationButtonToggle = false;
                CalculationButtonForeground = "Grey";
            }
        }
        public string InputString
        {
            get { return mInputString; }
            set
            {
                InputValidation(value);
                if (value == mInputString) return;
                mInputString = value;
                NotifyOfPropertyChange(() => InputString);
            }
        }

        public void Calculate()
        {
            if (WpfApplicationStatics.IsConversionActive)
            {
                mArguments.UseConversion = true;
                mArguments.ToMetric = WpfApplicationStatics.UseMetric;
                GetUnitAbbreviation();
            }
            else
            {
                mArguments.UseConversion = false;
            }
            mExecutor.InitiateCalculation(mInputString, mArguments);
            Result = mExecutor.CalculationResult;
            Steps = mExecutor.CalculationSteps;
        }

        void GetUnitAbbreviation()
        {
            foreach (var abbreviationList in ConversionViewModel.AllUnitsAndAbbreviations)
            {
                foreach (var units in abbreviationList)
                {
                    if (units.IsSelected)
                    {
                        mArguments.UnitForConversion = units.Abbreviation;
                        WpfApplicationStatics.LastPickedUnit = units.Abbreviation;
                    }
                }
            }
        }

        public void OnEnter(ActionExecutionContext context)
        {
            var keyArgs = context.EventArgs as KeyEventArgs;

            if (keyArgs != null && keyArgs.Key == Key.Enter || keyArgs != null && keyArgs.Key == Key.Return)
            {
                Calculate();
            }
        }

    }
}
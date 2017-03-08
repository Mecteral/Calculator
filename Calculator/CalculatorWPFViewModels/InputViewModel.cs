using System;
using System.Collections.Generic;
using System.Windows.Input;
using Calculator.Logic;
using Calculator.Logic.ArgumentParsing;
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class InputViewModel : PropertyChangedBase
    {
        string mInputString;
        string mResult;
        readonly IWpfCalculationExecutor mExecutor;
        readonly IApplicationArguments mArguments;
        List<string> mSteps = new List<string>();
        bool mStepExpander;

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

        public bool StepExpander
        {
            get { return mStepExpander; }
            set
            {
                if (value == mStepExpander) return;
                mStepExpander = value;
                NotifyOfPropertyChange(() => StepExpander);
            }
        }

        public InputViewModel(IWpfCalculationExecutor executor, IApplicationArguments arguments)
        {
            mExecutor = executor;
            mArguments = arguments;
        }

        public string InputString
        {
            get { return mInputString; }
            set
            {
                if (value == mInputString) return;
                mInputString = value;
                NotifyOfPropertyChange(() => InputString);
            }
        }

        public void Calculate()
        {
            if (ShellViewModel.IsConversionActive)
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
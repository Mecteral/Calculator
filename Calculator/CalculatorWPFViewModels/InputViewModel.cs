using System.Collections.Generic;
using Calculator.Logic;
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class InputViewModel : PropertyChangedBase
    {
        string mInputString;
        readonly IWpfCalculationExecutor mExecutor;

        string mResult;
        List<string> mSteps = new List<string>();

        public List<string> Steps
        {
            get { return mSteps; }

            set
            {
                if (value == mSteps) return;
                mSteps = value;
                NotifyOfPropertyChange(() => mSteps);
            }
        }

        public string Result
        {
            get { return mResult; }

            set
            {
                if (value == mResult) return;
                mResult = value;
                NotifyOfPropertyChange(() => mSteps);
            }
        }

        public InputViewModel(IWpfCalculationExecutor executor)
        {
            mExecutor = executor;
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
            mExecutor.InitiateCalculation(mInputString);
        }
    }
}
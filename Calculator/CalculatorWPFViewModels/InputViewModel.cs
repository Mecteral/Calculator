using System.Collections.Generic;
using System.Windows.Input;
using Calculator.Logic;
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class InputViewModel : PropertyChangedBase
    {
        string mInputString;
        string mResult;
        readonly IWpfCalculationExecutor mExecutor;
        List<string> mSteps = new List<string>();

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
            Result = mExecutor.CalculationResult;
            Steps = mExecutor.CalculationSteps;
        }
        public void OnEnter(Key key)
        {
            if (key == Key.Enter)
            {
                Calculate();
            }
        }
    }
}
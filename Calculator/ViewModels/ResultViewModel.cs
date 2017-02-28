using System;
using System.Collections.Generic;
using Caliburn.PresentationFramework;

namespace ViewModels
{
    public class ResultViewModel : PropertyChangedBase
    {
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
                NotifyOfPropertyChange(() => Result);
            }
        }
    }
}
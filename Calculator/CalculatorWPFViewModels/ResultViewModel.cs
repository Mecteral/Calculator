using System.Collections.Generic;
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ResultViewModel : PropertyChangedBase
    {
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

        public string Result { get; set; }
    }
}
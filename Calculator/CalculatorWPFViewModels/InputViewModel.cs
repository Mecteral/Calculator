using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class InputViewModel : PropertyChangedBase
    {
        string mInputString;
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

        public static void Calculate()
        {
        }
    }
}
using Caliburn.Core.InversionOfControl;
using Caliburn.PresentationFramework;

namespace ViewModels
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

        public void Calculate()
        {
        }
    }
}
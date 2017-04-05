using Caliburn.Micro;

namespace Calculator.WPF.ViewModels
{
    public class UnitAbbreviationsAndNames : PropertyChangedBase
    {

        public UnitAbbreviationsAndNames()
        {
            
        }
        bool mIsSelected;
        string mAbbreviation;
        string mName;

        public bool IsSelected
        {
            get { return mIsSelected; }
            set
            {
                if (value == mIsSelected) return;
                mIsSelected = value;
                NotifyOfPropertyChange();
            }
        }

        public string Abbreviation
        {
            get { return mAbbreviation; }
            set
            {
                if (value == mAbbreviation) return;
                mAbbreviation = value;
                NotifyOfPropertyChange();
            }
        }

        public string Name
        {
            get { return mName; }
            set
            {
                if (value == mName) return;
                mName = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
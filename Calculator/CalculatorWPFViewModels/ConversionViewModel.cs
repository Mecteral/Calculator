using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Calculator.Logic.ArgumentParsing;
using Caliburn.Micro;
using Mecteral.UnitConversion;

namespace CalculatorWPFViewModels
{
    public class ConversionViewModel : PropertyChangedBase
    {
        IApplicationArguments mArguments;
        string mContent;
        string mMilliliter;

        public ConversionViewModel(IApplicationArguments arguments)
        {
            mArguments = arguments;
        }

        public string Content
        {
            get { return mContent; }
            set
            {
                if (value == mContent) return;
                mContent = value;
                NotifyOfPropertyChange(() => Content);
            }
        }
        public string Milliliter
        {
            get { return mMilliliter; }
            set
            {
                if (value == mMilliliter) return;
                mMilliliter = value;
                NotifyOfPropertyChange(() => Milliliter);
                SetUnitAbbreviation("ml");
            }
        }
        protected string RadioButtonGroupName { get; set; }

        public void SetUnitAbbreviation(string abbreviation)
        {
            mArguments.UnitForConversion = abbreviation;
        }
    }
}
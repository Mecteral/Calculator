using System.Windows;
using Calculator.Logic.WpfApplicationProperties;
using Caliburn.Micro;

namespace Calculator.WPF.ViewModels
{
    public sealed class ConfigurationOptionTabViewModel : Screen, IMainScreenTabItem
    {
        readonly IWindowProperties mWindowProperties;
        string mSource;
        double mFontsize;

        public ConfigurationOptionTabViewModel(IWindowProperties windowProperties)
        {
            mWindowProperties = windowProperties;
            DisplayName = "Options";
            mFontsize = mWindowProperties.FontSize;
        }

        public string FontSelection
        {
            get { return mSource; }
            set
            {
                if (value == mSource) return;
                mSource = value;
                NotifyOfPropertyChange();
                mWindowProperties.Font = value;
            }
        }

        public void IncreaseFontSize()
        {
            mFontsize++;
            mWindowProperties.FontSize = mFontsize;
        }

        public void DecreaseFontSize()
        {
            if (mFontsize-1 > 0)
            {
                mFontsize--;
                mWindowProperties.FontSize = mFontsize;
            }
        }
    }
}
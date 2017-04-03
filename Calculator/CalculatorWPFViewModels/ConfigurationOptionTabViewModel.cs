using System.Windows;
using Caliburn.Micro;

namespace Calculator.WPF.ViewModels
{
    public sealed class ConfigurationOptionTabViewModel : Screen, IMainScreenTabItem
    {
        FontStyle mSource;
        double mFontsize;

        public ConfigurationOptionTabViewModel()
        {
            DisplayName = "Options";
        }

        public FontStyle FontSelection
        {
            get { return mSource; }
            set
            {
                if (value == mSource) return;
                mSource = value;
                NotifyOfPropertyChange();
                Application.Current.MainWindow.FontStyle = value;
            }
        }

        public void IncreaseFontSize()
        {
            mFontsize = Application.Current.MainWindow.FontSize;
            mFontsize++;
            Application.Current.MainWindow.FontSize = mFontsize;
        }

        public void DecreaseFontSize()
        {
            mFontsize = Application.Current.MainWindow.FontSize;
            if (mFontsize-1 > 0)
            {
                mFontsize--;
                Application.Current.MainWindow.FontSize = mFontsize;
            }
        }
    }
}
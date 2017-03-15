using System.Windows;
using System.Windows.Input;

namespace CalculatorWPFApplication.Views
{
    /// <summary>
    /// Interaktionslogik für ShellView.xaml
    /// </summary>
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        void MinButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        void TitleBar_OnDragEnter(object sender, DragEventArgs e)
        {
            DragMove();
        }
    }
}
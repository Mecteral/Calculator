using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels;

namespace CalculatorWPFApplication.Views
{
    /// <summary>
    /// Interaktionslogik für InputView.xaml
    /// </summary>
    public partial class InputView : UserControl
    {
        public InputView()
        {
            InitializeComponent();
        }

        void InputString_OnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                InputViewModel.Calculate();
            }
        }
    }
}

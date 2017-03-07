using System;
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel(InputViewModel input, ConductorViewModel conductor)
        {
            Input = input;
            Conductor = conductor;
        }
        public InputViewModel Input { get; private set; }
        public ConductorViewModel Conductor { get; private set; }
    }
}
using Caliburn.Micro;

namespace ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel(InputViewModel input, ResultViewModel result)
        {
            Input = input;
            Result = result;
        }
        public InputViewModel Input { get; private set; }
        public ResultViewModel Result { get; private set; }
    }
}
namespace Calculator.Logic.WpfApplicationProperties
{
    public interface IWindowProperties
    {
        bool AreStepsExpanded { get; set; }
        bool AreUnitsExpanded { get; set; }

        int ShellWindowHeight { get; set; }
        int ShellWindowWidth { get; set; }

        int ShellWindowPositionX { get; set; }
        int ShellWindowPositionY { get; set; }

        string UsedWpfTheme { get; set; }
    }
}
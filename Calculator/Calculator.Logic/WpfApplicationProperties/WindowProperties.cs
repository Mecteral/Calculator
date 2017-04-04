namespace Calculator.Logic.WpfApplicationProperties
{
    public class WindowProperties : IWindowProperties
    {
        public bool AreStepsExpanded { get; set; }
        public bool AreUnitsExpanded { get; set; }
        public int ShellWindowHeight { get; set; }
        public int ShellWindowWidth { get; set; }
        public int ShellWindowPositionX { get; set; }
        public int ShellWindowPositionY { get; set; }
        public string UsedWpfTheme { get; set; }
    }
}
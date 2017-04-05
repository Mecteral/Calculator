namespace Calculator.Logic.WpfApplicationProperties
{
    public class AllSerializableSettings : IAllSerializableSettings
    {
        public bool AreStepsExpanded { get; set; }
        public bool AreUnitsExpanded { get; set; }
        public int ShellWindowHeight { get; set; }
        public int ShellWindowWidth { get; set; }
        public int ShellWindowPositionX { get; set; }
        public int ShellWindowPositionY { get; set; }
        public string UsedWpfTheme { get; set; }
        public string LastPickedUnit { get; set; }
        public bool IsConversionActive { get; set; }
        public bool DoUseMetricSystem { get; set; }
        public double FontSize { get; set; }
        public string Font { get; set; }
    }
}
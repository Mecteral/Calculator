using System.Runtime.Serialization;

namespace Calculator.Logic
{
    public interface IAllSerializableSettings : IWindowProperties, IConversionProperties {}

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
    }

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

    public interface IConversionProperties
    {
        string LastPickedUnit { get; set; }
        bool IsConversionActive { get; set; }
        bool DoUseMetricSystem { get; set; }
    }

    public class ConversionProperties : IConversionProperties
    {
        public string LastPickedUnit { get; set; }
        public bool IsConversionActive { get; set; }
        public bool DoUseMetricSystem { get; set; }
    }
}
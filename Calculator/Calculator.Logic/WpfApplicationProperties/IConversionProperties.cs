namespace Calculator.Logic.WpfApplicationProperties
{
    public interface IConversionProperties
    {
        string LastPickedUnit { get; set; }
        bool IsConversionActive { get; set; }
        bool DoUseMetricSystem { get; set; }
    }
}
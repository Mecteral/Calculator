namespace Calculator.Logic.WpfApplicationProperties
{
    public class ConversionProperties : IConversionProperties
    {
        public string LastPickedUnit { get; set; }
        public bool IsConversionActive { get; set; }
        public bool DoUseMetricSystem { get; set; }
    }
}
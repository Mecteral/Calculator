namespace ImperialAndMetricConverter
{
    public class MetricVolumeToken : AConversionTokens, IConversionToken
    {
        public MetricVolumeToken(string asText, ApplicationArguments arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

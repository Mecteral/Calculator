namespace ImperialAndMetricConverter
{
    public class MetricMassToken : AConversionTokens, IConversionToken
    {
        public MetricMassToken(string asText, ApplicationArguments arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

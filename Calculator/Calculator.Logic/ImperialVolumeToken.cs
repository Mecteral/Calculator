namespace ImperialAndMetricConverter
{
    public class ImperialVolumeToken : AConversionTokens, IConversionToken
    {
        public ImperialVolumeToken(string asText, ApplicationArguments arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

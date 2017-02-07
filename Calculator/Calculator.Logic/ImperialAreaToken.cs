namespace ImperialAndMetricConverter
{
    public class ImperialAreaToken : AConversionTokens, IConversionToken
    {
        public ImperialAreaToken(string asText, ApplicationArguments arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

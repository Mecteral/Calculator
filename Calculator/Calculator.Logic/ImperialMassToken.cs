namespace ImperialAndMetricConverter
{
    public class ImperialMassToken : AConversionTokens, IConversionToken
    {
        public ImperialMassToken(string asText, ApplicationArguments arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

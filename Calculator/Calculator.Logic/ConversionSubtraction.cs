namespace ImperialAndMetricConverter
{
    public class ConversionSubtraction : AnArithmeticConversionOperation
    {
        public override void Accept(IConversionExpressionVisitor visitor) => visitor.Visit(this);
    }
}

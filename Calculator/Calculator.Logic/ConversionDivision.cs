namespace ImperialAndMetricConverter
{
    public class ConversionDivision : AnArithmeticConversionOperation
    {
        public override void Accept(IConversionExpressionVisitor visitor) => visitor.Visit(this);
    }
}

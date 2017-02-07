namespace ImperialAndMetricConverter
{
    public class ConversionMultiplication : AnArithmeticConversionOperation
    {
        public override void Accept(IConversionExpressionVisitor visitor) => visitor.Visit(this);
    }
}

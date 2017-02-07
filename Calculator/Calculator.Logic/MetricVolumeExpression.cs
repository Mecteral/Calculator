using System;

namespace ImperialAndMetricConverter
{
    public class MetricVolumeExpression : AnConversionExpression
    {
        public override decimal Value { get; set; }
        public override void Accept(IConversionExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"{Value}";
        public override void ReplaceChild(IConversionExpression oldChild, IConversionExpression newChild)
        {
            throw new InvalidOperationException();
        }
    }
}

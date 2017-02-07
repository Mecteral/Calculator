namespace ImperialAndMetricConverter
{
    public interface IConversionExpression
    {
        IConversionExpression Parent { get; }
        bool HasParent { get; }
        void Accept(IConversionExpressionVisitor visitor);
        void ReplaceChild(IConversionExpression oldChild, IConversionExpression newChild);
    }
}

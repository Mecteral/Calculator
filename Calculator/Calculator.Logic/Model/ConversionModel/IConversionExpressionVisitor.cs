namespace Calculator.Logic.Model.ConversionModel
{
    public interface IConversionExpressionVisitor
    {
        void Visit(ConversionAddition conversionAddition);
        void Visit(ConversionDivision conversionDivision);
        void Visit(ConversionSubtraction conversionSubtraction);
        void Visit(ConversionMultiplication conversionMultiplication);
    }
}
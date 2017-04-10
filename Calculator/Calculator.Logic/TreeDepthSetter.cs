using Calculator.Model;

namespace Calculator.Logic
{
    public class TreeDepthSetter : IExpressionVisitor, ITreeDepthSetter
    {
        public void Visit(ParenthesedExpression parenthesed)
        {
            SetDepth(parenthesed);
            parenthesed.Wrapped.Accept(this);
        }

        public void Visit(Subtraction subtraction)
        {
            SetDepth(subtraction);
            VisitOperands(subtraction);
        }

        public void Visit(Multiplication multiplication)
        {
            SetDepth(multiplication);
            VisitOperands(multiplication);
        }

        public void Visit(Addition addition)
        {
            SetDepth(addition);
            VisitOperands(addition);
        }

        public void Visit(Constant constant)
        {
            SetDepth(constant);
        }

        public void Visit(Division division)
        {
            SetDepth(division);
            VisitOperands(division);
        }

        public void Visit(Variable variable)
        {
            SetDepth(variable);
        }

        public void Visit(Cosine cosineExpression)
        {
            SetDepth(cosineExpression);
        }

        public void Visit(Tangent tangentExpression)
        {
            SetDepth(tangentExpression);
        }

        public void Visit(Sinus sinusExpression)
        {
            SetDepth(sinusExpression);
        }

        public void Visit(Power power)
        {
            SetDepth(power);
            VisitOperands(power);
        }

        public void SetTreeDepth(IExpression expression)
        {
            expression.Accept(this);
        }

        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }

        void SetDepth(IExpression expression)
        {
            if (expression.HasParent)
                expression.TreeDepth = expression.Parent.TreeDepth + 1;
            else
                expression.TreeDepth = 0;
        }
    }
}
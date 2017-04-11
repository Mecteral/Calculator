using System.Collections.Generic;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class DistributeLawHelper : IExpressionVisitor, IDistributeLawHelper
    {
        List<IExpression> mListOfMultipliableExpressions = new List<IExpression>();
        public void Visit(ParenthesedExpression parenthesed)
        {
            mListOfMultipliableExpressions.Add(parenthesed);
        }

        public void Visit(Subtraction subtraction)
        {
            VisitOperands(subtraction);
        }

        public void Visit(Multiplication multiplication)
        {
            VisitOperands(multiplication);
        }

        public void Visit(Addition addition)
        {
            VisitOperands(addition);
        }

        public void Visit(Constant constant)
        {
            mListOfMultipliableExpressions.Add(constant);
        }

        public void Visit(Division division)
        {
            VisitOperands(division);
        }

        public void Visit(Variable variable)
        {
            mListOfMultipliableExpressions.Add(variable);
        }

        public void Visit(Cosine cosineExpression)
        {
            mListOfMultipliableExpressions.Add(cosineExpression);
        }

        public void Visit(Tangent tangentExpression)
        {
            mListOfMultipliableExpressions.Add(tangentExpression);
        }

        public void Visit(Sinus sinusExpression)
        {
            mListOfMultipliableExpressions.Add(sinusExpression);
        }

        public void Visit(Power power)
        {
            VisitOperands(power);
        }

        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }

        public List<IExpression> GetAllUnderLyingMultipliableExpressions(IExpression multiplicatorRoot)
        {
            mListOfMultipliableExpressions = new List<IExpression>();
            var start = multiplicatorRoot as ParenthesedExpression;
            if (null != start)
                start.Wrapped.Accept(this);
            else
                multiplicatorRoot.Accept(this);
            return mListOfMultipliableExpressions;
        }
    }
}
using Calculator.Model;

namespace Calculator.Logic.Evaluation
{
    public abstract class AVisitingEvaluator : IEvaluator, IExpressionVisitor
    {
        int mResult;

        public int Evaluate(IExpression expression)
        {
            expression.Accept(this);
            return mResult;
        }

        public void Visit(ParenthesedExpression parenthesed)
        {
            EvaluateParenthesed(parenthesed);
            parenthesed.Wrapped.Accept(this);
        }

        public void Visit(Subtraction subtraction)
        {
            EvaluateSubtraction(subtraction);
            VisitOperands(subtraction);
        }

        public void Visit(Multiplication multiplication)
        {
            EvaluateMultiplication(multiplication);
            VisitOperands(multiplication);
        }

        public void Visit(Addition addition)
        {
            EvaluateAddition(addition);
            VisitOperands(addition);
        }

        public void Visit(Division division)
        {
            EvaluateDivision(division);
            VisitOperands(division);
        }

        public void Visit(Constant constant) => EvaluateConstant(constant);

        public void Visit(Variable variable) => EvaluateVariable(variable);

        public void Visit(Cosine cosineExpression) => EvaluateCosine(cosineExpression);

        public void Visit(Tangent tangentExpression) => EvaluateTangent(tangentExpression);

        public void Visit(Sinus sinusExpression) => EvaluateSinus(sinusExpression);

        public void Visit(Power power)
        {
            EvaluatePower(power);
            VisitOperands(power);
        }

        protected abstract void EvaluateMultiplication(Multiplication multiplication);

        protected abstract void EvaluateAddition(Addition addition);

        protected abstract void EvaluateConstant(Constant constant);

        protected abstract void EvaluateDivision(Division division);

        protected abstract void EvaluateVariable(Variable variable);

        protected abstract void EvaluateCosine(Cosine cosineExpression);

        protected abstract void EvaluateTangent(Tangent tangentExpression);

        protected abstract void EvaluateSinus(Sinus sinusExpression);

        protected abstract void EvaluatePower(Power power);

        protected abstract void EvaluateParenthesed(ParenthesedExpression parenthesed);

        protected abstract void EvaluateSubtraction(Subtraction subtraction);

        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }

        protected void IncreaseCount() => mResult++;
    }
}
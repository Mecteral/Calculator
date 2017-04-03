using System;
using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    /// <summary>
    /// This Class calculates all possible calculations without variables
    /// </summary>
    public class DirectCalculationSimplifier : IDirectCalculationSimplifier
    {
        readonly IExpressionEvaluator mEvaluator;
        IExpression mExpression;
        public DirectCalculationSimplifier(IExpressionEvaluator evaluator)
        {
            mEvaluator = evaluator;
        }
        public void Visit(Subtraction subtraction)
        {
            CalculateResultIfPossible(subtraction);
        }
        public void Visit(Multiplication multiplication)
        {
            CalculateResultIfPossible(multiplication);
        }
        public void Visit(Addition addition)
        {
            CalculateResultIfPossible(addition);
        }
        public void Visit(Division division)
        {
            CalculateResultIfPossible(division);
        }
        public void Visit(Power square)
        {
            CalculateResultIfPossible(square);
        }
        public void Visit(ParenthesedExpression parenthesed)
        {
            parenthesed.Wrapped.Accept(this);
        }
        public void Visit(Constant constant) {}
        public void Visit(Variable variable) {}
        public void Visit(Cosine cosineExpression) {}
        public void Visit(Tangent tangentExpression) {}
        public void Visit(Sinus sinusExpression) {}
        public IExpression Simplify(IExpression input)
        {
            mExpression = ExpressionCloner.Clone(input);
            mExpression.Accept(this);
            return mExpression;
        }
        static bool IsCalculateable(IArithmeticOperation operation)
            => operation.Left is Constant && operation.Right is Constant;
        void CalculateResultIfPossible(IArithmeticOperation operation)
        {
            if (IsCalculateable(operation))
            {
                if (operation is Subtraction && operation.Left is Constant && operation.Right is Constant) {
                    operation = ChangeSubtractionIfRighthandsideIsNegative(operation);
                }
                var replacement = new Constant {Value = mEvaluator.Evaluate(operation, null)};
                if (operation.HasParent) operation.Parent.ReplaceChild(operation, replacement);
                else mExpression = replacement;
            }
            else VisitOperands(operation);
        }
        IArithmeticOperation ChangeSubtractionIfRighthandsideIsNegative(IArithmeticOperation operation)
        {
            var constant = (Constant) operation.Right;
            if (constant.Value < 0)
            {
                var replaced = new Addition {Left = operation.Left, Right = operation.Right};
                if (operation.HasParent)
                {
                    operation.Parent.ReplaceChild(operation, replaced);
                    return replaced;
                }
                mExpression = replaced;
            }
            return operation;
        }
        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }
    }

    public class ExpressionsWithOnlyConstantChildrenSimplifier : AVisitingTraversingReplacer
    {
        protected override IExpression Replace(IExpression expression)
        {
            if (expression.HasOnlyConstantChildren()) return base.Replace(expression);
            return expression;
        }
        protected override IExpression ReplaceSubtraction(Subtraction subtraction)
            => new Constant {Value = subtraction.Left.GetConstantValue() - subtraction.Right.GetConstantValue()};
        protected override IExpression ReplaceMultiplication(Multiplication multiplication)
            => new Constant {Value = multiplication.Left.GetConstantValue() * multiplication.Right.GetConstantValue()};
        protected override IExpression ReplaceAddition(Addition addition)
            => new Constant { Value = addition.Left.GetConstantValue() + addition.Right.GetConstantValue() };
        protected override IExpression ReplaceDivision(Division division)
            => new Constant { Value = division.Left.GetConstantValue() / division.Right.GetConstantValue() };
        protected override IExpression ReplacePower(Power power)
            => new Constant { Value = (decimal)Math.Pow((double)power.Left.GetConstantValue(), (double)power.Right.GetConstantValue()) };
        protected override IExpression ReplaceCosine(Cosine cosine) => new Constant {Value = cosine.Value};
        protected override IExpression ReplaceTangent(Tangent tangent) => new Constant { Value = tangent.Value };
        protected override IExpression ReplaceSinus(Sinus sinus) => new Constant { Value = sinus.Value };
    }
}
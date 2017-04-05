using System;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
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
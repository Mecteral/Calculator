using System;
using System.Threading;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class PowerSimplifier : AVisitingTraversingReplacer
    {
        protected override IExpression ReplaceMultiplication(Multiplication multiplication)
        {
            var multiplicationConstantLeft = multiplication.Left as IExpressionWithValue;
            var multiplicationConstantRight = multiplication.Right as IExpressionWithValue;
            var variableLeft = multiplication.Left as Variable;
            var variableRight = multiplication.Right as Variable;


            if (null != multiplicationConstantLeft && null != multiplicationConstantRight &&
                multiplicationConstantLeft.Value == multiplicationConstantRight.Value)
                return HandleExpressionsWithSameValues(multiplicationConstantLeft, multiplicationConstantRight,
                    multiplication);

            if (null != variableLeft && null != variableRight && variableLeft.Name == variableRight.Name)
                return new Power {Left = variableLeft, Right = new Constant {Value = 2}};

            if (null != variableLeft && multiplication.Right is Power)
            {
                var powerRight = (Power) multiplication.Right;
                var powerVariable = powerRight.Left as Variable;
                var powerConstant = (Constant) powerRight.Right;
                if (null != powerVariable && powerVariable.Name == variableLeft.Name)
                    return new Power
                    {
                        Left = new Variable {Name = powerVariable.Name},
                        Right = new Constant {Value = powerConstant.Value + 1}
                    };
                return multiplication;
            }

            if (null != variableRight && multiplication.Left is Power)
            {
                var powerRight = (Power) multiplication.Left;
                var powerVariable = powerRight.Left as Variable;
                var powerConstant = (Constant) powerRight.Right;
                if (null != powerVariable && powerVariable.Name == variableRight.Name)
                    return new Power
                    {
                        Left = new Variable {Name = powerVariable.Name},
                        Right = new Constant {Value = powerConstant.Value + 1}
                    };
                return multiplication;
            }

            return multiplication;
        }

        IExpression HandleExpressionsWithSameValues(IExpression left, IExpression right, IExpression multiplication)
        {
            var result = CreatePowerFromValueExpressions<Constant>(left, right, multiplication);
            if (result != multiplication)
                return result;
            result = CreatePowerFromValueExpressions<Sinus>(left, right, multiplication);
            if (result != multiplication)
                return result;
            result = CreatePowerFromValueExpressions<Cosine>(left, right, multiplication);
            if (result != multiplication)
                return result;
            result = CreatePowerFromValueExpressions<Tangent>(left, right, multiplication);
            if (result != multiplication)
                return result;
            return result;
        }

        IExpression CreatePowerFromValueExpressions<TSelf>(IExpression left, IExpression right, IExpression multiplication) where TSelf : IExpressionWithValue
        {
            if (left is TSelf && right is TSelf)
                return new Power { Left = left, Right = new Constant() {Value = 2} };
            return multiplication;
        }
    }
}
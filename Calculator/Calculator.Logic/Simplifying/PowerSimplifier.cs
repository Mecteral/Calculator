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
                return HandleExpressionsWithSameValuesInMultiplication(multiplicationConstantLeft,
                    multiplicationConstantRight,
                    multiplication);

            if (null != variableLeft && null != variableRight && variableLeft.Name == variableRight.Name)
                return CreatePowerFromMultiplicativeExpressions<Variable>(variableLeft, variableRight, multiplication);


            if (multiplication.Right is Power)
            {
                var power = (Power) multiplication.Right;
                var multiplicator = multiplication.Left;

                var powerExponent = (Constant) power.Right;

                var powerBaseAsVariable = power.Left as Variable;
                var multiplicatorVariable = multiplication.Left as Variable;

                var powerBaseWithValue = power.Left as IExpressionWithValue;
                var multiplicatorWithValue = multiplication.Left as IExpressionWithValue;

                if (null != powerBaseAsVariable && null != multiplicatorVariable &&
                    powerBaseAsVariable.Name == multiplicatorVariable.Name)
                    return CreatePowerFromMultiplicationWithPowerAndIExpression<Variable>(powerBaseAsVariable,
                        powerExponent, multiplicator, multiplication);

                if (null != powerBaseWithValue && null != multiplicatorWithValue &&
                    powerBaseWithValue.Value == multiplicatorWithValue.Value)
                    return HandleExpressionsWithSameValuesInMultiplicationWithPower(powerBaseWithValue, powerExponent,
                        multiplicatorWithValue, multiplication);
            }
            if (multiplication.Left is Power)
            {
                var power = (Power) multiplication.Left;
                var multiplicator = multiplication.Right;

                var powerExponent = (Constant) power.Right;

                var powerBaseAsVariable = power.Left as Variable;
                var multiplicatorVariable = multiplication.Right as Variable;

                var powerBaseWithValue = power.Left as IExpressionWithValue;
                var multiplicatorWithValue = multiplication.Right as IExpressionWithValue;

                if (null != powerBaseAsVariable && null != multiplicatorVariable &&
                    powerBaseAsVariable.Name == multiplicatorVariable.Name)
                    return CreatePowerFromMultiplicationWithPowerAndIExpression<Variable>(powerBaseAsVariable,
                        powerExponent, multiplicator, multiplication);

                if (null != powerBaseWithValue && null != multiplicatorWithValue &&
                    powerBaseWithValue.Value == multiplicatorWithValue.Value)
                    return HandleExpressionsWithSameValuesInMultiplicationWithPower(powerBaseWithValue, powerExponent,
                        multiplicatorWithValue, multiplication);
            }
            return multiplication;
        }

        static IExpression HandleExpressionsWithSameValuesInMultiplication(IExpression left, IExpression right,
            IExpression multiplication)
        {
            var result = CreatePowerFromMultiplicativeExpressions<Constant>(left, right, multiplication);
            if (result != multiplication)
                return result;
            result = CreatePowerFromMultiplicativeExpressions<Sinus>(left, right, multiplication);
            if (result != multiplication)
                return result;
            result = CreatePowerFromMultiplicativeExpressions<Cosine>(left, right, multiplication);
            if (result != multiplication)
                return result;
            result = CreatePowerFromMultiplicativeExpressions<Tangent>(left, right, multiplication);
            return result;
        }

        static IExpression CreatePowerFromMultiplicativeExpressions<TSelf>(IExpression left, IExpression right,
            IExpression multiplication) where TSelf : IExpression
        {
            if (left is TSelf && right is TSelf)
                return new Power {Left = left, Right = new Constant {Value = 2}};
            return multiplication;
        }

        static IExpression HandleExpressionsWithSameValuesInMultiplicationWithPower(IExpression powerBase, IExpression exponent,
            IExpression multiplicator,
            IExpression multiplication)
        {
            var result = CreatePowerFromMultiplicationWithPowerAndIExpression<Constant>(powerBase,
                exponent, multiplicator, multiplication);
            if (result != multiplication)
                return result;
            result = CreatePowerFromMultiplicationWithPowerAndIExpression<Sinus>(powerBase,
                exponent, multiplicator, multiplication);
            if (result != multiplication)
                return result;
            result = CreatePowerFromMultiplicationWithPowerAndIExpression<Cosine>(powerBase,
                exponent, multiplicator, multiplication);
            if (result != multiplication)
                return result;
            result = CreatePowerFromMultiplicationWithPowerAndIExpression<Tangent>(powerBase,
                exponent, multiplicator, multiplication);
            return result;
        }

        static IExpression CreatePowerFromMultiplicationWithPowerAndIExpression<TSelf>(IExpression powerBase,
            IExpression exponent, IExpression multiplicator,
            IExpression multiplication) where TSelf : IExpression
        {
            if (powerBase is TSelf && multiplicator is TSelf)
                return new Power {Left = powerBase, Right = new Constant {Value = exponent.GetConstantValue() + 1}};
            return multiplication;
        }
    }
}
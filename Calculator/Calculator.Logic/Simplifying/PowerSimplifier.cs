using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class PowerSimplifier : AVisitingTraversingReplacer
    {
        protected override IExpression ReplaceMultiplication(Multiplication multiplication)
        {
            var multiplicationConstantLeft = multiplication.Left as Constant;
            var multiplicationConstantRight = multiplication.Right as Constant;
            var variableLeft = multiplication.Left as Variable;
            var variableRight = multiplication.Right as Variable;


            if (null != multiplicationConstantLeft && null != multiplicationConstantRight &&
                multiplicationConstantLeft.Value == multiplicationConstantRight.Value)
                return new Power {Left = multiplication.Left, Right = multiplication.Right};
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
    }
}
using System.Collections.Generic;
using System.Globalization;
using Calculator.Logic.Parsing;

namespace Calculator.Logic
{
    /// <summary>
    /// Takes in a List of Tokens and returns a written out Token string
    /// </summary>
    public class TokenFormatter : ITokenVisitor
    {
        string mResult = "";

        public void Visit(OperatorToken operatorToken)
        {
            switch (operatorToken.Operator)
            {
                case Operator.Add:
                    mResult += "+";
                    break;
                case Operator.Subtract:
                    mResult += "-";
                    break;
                case Operator.Multiply:
                    mResult += "*";
                    break;
                case Operator.Divide:
                    mResult += "/";
                    break;
            }
        }

        public void Visit(NumberToken numberToken)
        {
            mResult += numberToken.Value.ToString(CultureInfo.InvariantCulture);
        }

        public void Visit(ParenthesesToken parenthesesToken)
        {
            mResult += parenthesesToken.IsOpening ? "(" : ")";
        }

        public void Visit(VariableToken variableToken)
        {
            mResult += variableToken.Variable;
        }

        public string Format(IEnumerable<IToken> tokens)
        {
            foreach (var token in tokens)
                token.Accept(this);
            return mResult;
        }
    }
}
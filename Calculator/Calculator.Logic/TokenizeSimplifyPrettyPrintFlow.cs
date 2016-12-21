using Calculator.Logic.Model;
using Calculator.Logic.Parsing;

namespace Calculator.Logic
{
    public class TokenizeSimplifyPrettyPrintFlow
    {
        readonly ISimplifier mSimplifier;
        readonly ITokenizer mTokenizer;
        readonly IExpressionFormatter mFormatter;
        public string Simplify(string input)
        {
            mTokenizer.Tokenize(input);
            
            return null;
        }
    }
}
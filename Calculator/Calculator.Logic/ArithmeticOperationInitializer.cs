using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic
{
    public class ArithmeticOperationInitializer
    {
        static IEnumerable<string> _enumerable;

        public static double CalculateResult(IEnumerable<string> tokenEnumerable)
        {
            var tokenList = ArgumentParser.Tokenize(tokenEnumerable);
            foreach (var token in tokenList)
            {
                if (token == "(")
                {
                    var parantheses = new ParentheseExpression();
                    parantheses.Inner = new Addition();
                    parantheses.Inner.Evaluate();
                }
            }
            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Parsing
{
    /// <summary>
    /// Contains alphabetized <string> string </string> for Variables and double Value for calculations
    /// </summary>
    public class VariableToken : IToken
    {
        public string Variable { get; private set; }

        public VariableToken(string asText)
        {
            var variables = "";
            foreach (var c in asText)
            {
                if (char.IsLetter(c))
                    variables += c;
            }
            Variable = Alphabetize(variables);
        }

        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }

        static string Alphabetize(string input)
        {
            var cArray = input.ToCharArray();
            Array.Sort(cArray);
            return new string(cArray);
        }

    }
}

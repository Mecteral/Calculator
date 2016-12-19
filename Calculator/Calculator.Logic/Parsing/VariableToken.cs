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

        public VariableToken(char asText)
        {
            Variable += asText;
        }

        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }


    }
}

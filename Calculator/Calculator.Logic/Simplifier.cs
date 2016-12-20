using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model;

namespace Calculator.Logic
{
    public class Simplifier
    {
        readonly IExpression mExpression;
        IExpression mCurrent;
        IEnumerable<IExpression> mToEvaluate = new List<IExpression>();
        public Simplifier(IExpression expression)
        {
            mExpression = expression;
        }

        public void Simplify()
        {
            mCurrent = mExpression;
        }
        static bool IsVariable(IExpression expression) => expression is Variable;
    }
}

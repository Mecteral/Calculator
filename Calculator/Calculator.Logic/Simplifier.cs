using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model;

namespace Calculator.Logic
{
    public class Simplifier : IExpressionVisitor
    {
        IExpression mExpression;

        Simplifier(IExpression expression)
        {
            mExpression = expression;
        }
        public void Visit(ParenthesedExpression parenthesed)
        {
            throw new NotImplementedException();
        }

        public void Visit(Subtraction subtraction)
        {
            throw new NotImplementedException();
        }

        public void Visit(Multiplication multiplication)
        {
            throw new NotImplementedException();
        }

        public void Visit(Addition addition)
        {
            throw new NotImplementedException();
        }

        public void Visit(Constant constant)
        {
            throw new NotImplementedException();
        }

        public void Visit(Division division)
        {
            throw new NotImplementedException();
        }

        public void Visit(Variable variable)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Model;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Logic
{
    public class ExpressionEqualityChecker : IExpressionVisitor
    {
        IList<IExpression> mFirstExpressions = new List<IExpression>();
        IList<IExpression> mSecondExpressions = new List<IExpression>();
        public bool IsEqual(IExpression firstExpression, IExpression secondExpression)
        {
            firstExpression.Accept(this);
            FillFirstExpressions(mSecondExpressions);
            mSecondExpressions = new List<IExpression>();
            secondExpression.Accept(this);
            var result = CheckEqualityOfLists(mFirstExpressions, mSecondExpressions);
            mFirstExpressions = new List<IExpression>();
            return result;
        }
        public void Visit(ParenthesedExpression parenthesed)
        {
            parenthesed.Wrapped.Accept(this);
            mSecondExpressions.Add(parenthesed);
        }

        public void Visit(Subtraction subtraction)
        {
            VisitOperands(subtraction);
            mSecondExpressions.Add(subtraction);
        }

        public void Visit(Multiplication multiplication)
        {
            VisitOperands(multiplication);
            mSecondExpressions.Add(multiplication);
        }

        public void Visit(Addition addition)
        {
            VisitOperands(addition);
            mSecondExpressions.Add(addition);
        }

        public void Visit(Constant constant)
        {
            mSecondExpressions.Add(constant);
        }

        public void Visit(Division division)
        {
            VisitOperands(division);
            mSecondExpressions.Add(division);
        }

        public void Visit(Variable variable)
        {
            mSecondExpressions.Add(variable);
        }

        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }

        void FillFirstExpressions(IEnumerable<IExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                mFirstExpressions.Add(expression);
            }
        }

        static bool CheckEqualityOfLists(IEnumerable<IExpression> first, IEnumerable<IExpression> second)
        {
            var limit = 0;
            limit = first.Count() <= second.Count() ? first.Count() : second.Count();
            for (var i = 0; i < limit; i++)
            {
                if (first.ElementAt(i) is Division)
                    if (!(second.ElementAt(i) is Division))
                        return false;
                if (first.ElementAt(i) is Addition)
                    if (!(second.ElementAt(i) is Addition))
                        return false;
                if (first.ElementAt(i) is Multiplication)
                    if (!(second.ElementAt(i) is Multiplication))
                        return false;
                if (first.ElementAt(i) is Subtraction)
                    if (!(second.ElementAt(i) is Subtraction))
                        return false;
                if (first.ElementAt(i) is Constant)
                    if (!(second.ElementAt(i) is Constant))
                        return false;
                if (first.ElementAt(i) is Variable)
                    if (!(second.ElementAt(i) is Variable))
                        return false;
                if (first.ElementAt(i) is ParenthesedExpression)
                    if (!(second.ElementAt(i) is ParenthesedExpression))
                        return false;
            }
            return true;
        }
    }
}

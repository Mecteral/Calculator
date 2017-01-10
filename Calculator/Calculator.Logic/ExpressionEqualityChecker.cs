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
            var result = CheckEqualityOfLists();
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

        bool CheckEqualityOfLists()
        {
//            if (mFirstExpressions.Count != mSecondExpressions.Count) return false;
            var limit = 0;
            limit = mFirstExpressions.Count() <= mSecondExpressions.Count() ? mFirstExpressions.Count() : mSecondExpressions.Count();
            for (var i = 0; i < limit; i++)
            {
                var first = mFirstExpressions.ElementAt(i);
                var last = mSecondExpressions.ElementAt(i);
                if (first is Division)
                    if (!(last is Division))
                        return false;
                if (first is Addition)
                    if (!(last is Addition))
                        return false;
                if (first is Multiplication)
                    if (!(last is Multiplication))
                        return false;
                if (first is Subtraction)
                    if (!(last is Subtraction))
                        return false;
                if (first is Constant)
                    if (!(last is Constant))
                        return false;
                if (first is Variable)
                    if (!(last is Variable))
                        return false;
                if (first is ParenthesedExpression)
                    if (!(last is ParenthesedExpression))
                        return false;
            }
            return true;
        }
    }
}

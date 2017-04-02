using System.Collections.Generic;
using Calculator.Model;

namespace Calculator.Logic.Utilities
{
    public class ExpressionEqualityChecker : IExpressionEqualityChecker
    {
        IList<IExpression> mFirstExpressions = new List<IExpression>();
        IList<IExpression> mSecondExpressions = new List<IExpression>();

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

        public void Visit(Power square)
        {
            VisitOperands(square);
            mSecondExpressions.Add(square);
        }

        public void Visit(Variable variable)
        {
            mSecondExpressions.Add(variable);
        }

        public void Visit(Cosine cosineExpression)
        {
            mSecondExpressions.Add(cosineExpression);
        }

        public void Visit(Tangent tangentExpression)
        {
            mSecondExpressions.Add(tangentExpression);
        }

        public void Visit(Sinus sinusExpression)
        {
            mSecondExpressions.Add(sinusExpression);
        }

        public bool IsEqual(IExpression firstExpression, IExpression secondExpression)
        {
            mSecondExpressions.Clear();
            firstExpression.Accept(this);
            mFirstExpressions = mSecondExpressions;
            mSecondExpressions = new List<IExpression>();
            secondExpression.Accept(this);
            var result = CheckEqualityOfLists();
            return result;
        }

        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }

        bool CheckEqualityOfLists()
        {
            if (mFirstExpressions.Count != mSecondExpressions.Count) return false;

            for (var i = 0; i < mFirstExpressions.Count; i++)
            {
                var first = mFirstExpressions[i];
                var second = mSecondExpressions[i];
                if (!AreEqual(first, second)) return false;
            }
            return true;
        }

        static bool AreEqual(IExpression first, IExpression second)
        {
            if (first.GetType() != second.GetType()) return false;
            if (first is Constant)
            {
                var lhs = (Constant) first;
                var rhs = (Constant) second;
                if (lhs.Value != rhs.Value) return false;
            }
            else if (first is Variable)
            {
                var lhs = (Variable) first;
                var rhs = (Variable) second;
                if (lhs.Variables != rhs.Variables) return false;
            }
            return true;
        }
    }
}
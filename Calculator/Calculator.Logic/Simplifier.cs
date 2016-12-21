using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;

namespace Calculator.Logic
{
    public class Simplifier : IExpressionVisitor
    {
        IExpression mExpression;
        IEnumerable<IExpression> mToEvaluate = new List<IExpression>();
        public Simplifier(IExpression expression)
        {
            mExpression = new ExpressionCloner().Clone(expression);
        }

        public Simplifier(){}
        public IExpression Simplify(IExpression input)
        {
            var simplifier = new Simplifier(input);
            return simplifier.Simplify();
        }
        public IExpression Simplify()
        {
            mExpression.Accept(this);
            return mExpression;
        }

        public void Visit(Subtraction subtraction)
        {
            CalculateResultIfPossible(subtraction);
        }

        public void Visit(Multiplication multiplication)
        {
            CalculateResultIfPossible(multiplication);
        }

        public void Visit(Addition addition)
        {
            CalculateResultIfPossible(addition);
        }

        public void Visit(Division division)
        {
            CalculateResultIfPossible(division);
        }

        public void Visit(ParenthesedExpression parenthesed)
        {
            if (parenthesed.Wrapped is IArithmeticOperation)
            {
                CalculateResultIfPossible((IArithmeticOperation)parenthesed.Wrapped);
            }
        }

        public void Visit(Constant constant)
        {
        }

        public void Visit(Variable variable)
        {
        }

        static bool IsCalculateable(IArithmeticOperation operation) => (operation.Left is Constant && operation.Right is Constant);

        void CalculateResultIfPossible(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
            if (IsCalculateable(operation))
            {
                var constant = new Constant {Value = EvaluatingExpressionVisitor.Evaluate(operation)};
                if (operation.Parent != null)
                {
                    if (operation.Parent is IArithmeticOperation)
                    {
                        var parent = (IArithmeticOperation)operation.Parent;
                        if (parent.Left == operation)
                            parent.Left = constant;
                        else
                            parent.Right = constant;
                    }
                    else
                    {
                        var parent = (ParenthesedExpression) operation.Parent;
                        parent.Wrapped = constant;
                    }
                }
                else
                {
                    mExpression = constant;
                }
            }
            if ((operation.Left is Addition || operation.Left is Subtraction) && operation.Right is Constant)
            {
                var operationLeft = (IArithmeticOperation) operation.Left;
                if (operationLeft.Right is Constant)
                {
                    if (operation.Parent != null)
                    {
                        var parent = (IArithmeticOperation) operation.Parent;
                        parent.Left = operationLeft;
                    }
                    operation.Left.Parent = operation.Parent;
                    operation.Left = operationLeft.Right;
                    var constant = new Constant { Value = EvaluatingExpressionVisitor.Evaluate(operation) };
                    operationLeft.Right = constant;
                    operationLeft.Right.Parent = operationLeft;
                    if (operationLeft.Parent == null)
                    {
                        mExpression = operationLeft;
                    }
                }
            }
        }
    }
}

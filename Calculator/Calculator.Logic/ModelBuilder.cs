using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;

namespace Calculator.Logic
{
    /// <summary>
    /// Takes in a List of tokens to make an IExpression for Algorithmic Operations
    /// </summary>
    public class ModelBuilder : ITokenVisitor, IModelBuilder
    {
        readonly IList<IExpression> mExpressions = new List<IExpression>();
        readonly IList<ParenthesesNode> mRootNodes = new List<ParenthesesNode>();
        IArithmeticOperation mCurrentOperation;
        ParenthesesNode mNode;
        bool IsWrapped => null != mNode;
        public IExpression BuildFrom(IEnumerable<IToken> tokens)
        {
            foreach (var token in tokens) { token.Accept(this); }
            HandleParenthesesExpressions();
            PointBeforeAdditionOrSubtraction(mExpressions);
            return mExpressions[0];
        }
        public void Visit(OperatorToken operatorToken) => Add(CreateOperatorExpression(operatorToken));
        public void Visit(NumberToken numberToken) => Add(new Constant {Value = numberToken.Value});
        public void Visit(ParenthesesToken parenthesisToken)
        {
            if (parenthesisToken.IsOpening && mNode == null) {
                HandleTopLevelOpeningParenthesis();
            }
            else if (parenthesisToken.IsOpening) {
                HandleChildOpeningParenthesis();
            }
            else
            {
                HandleClosingParenthesis();
            }
        }
        public void Visit(VariableToken variableToken) => Add(new Variable {Variables = variableToken.Variable});
        void IterateChildren(ParenthesesNode root)
        {
            var index = 0;
            foreach (var child in root.Children)
            {
                index++;
                if (child.HasChild) { IterateChildren(child); }
                child.ParenthesedExpression.Wrapped = PointBeforeAdditionOrSubtraction(child.Expressions);
                ChangeParenthesesExpressionOfParent(index, child);
            }
        }
        static void ChangeParenthesesExpressionOfParent(int index, ParenthesesNode child)
        {
            child.Parent.Expressions[FindExpressionIndexForParenthesesIndex(index, child)] = child.ParenthesedExpression;
        }
        static int FindExpressionIndexForParenthesesIndex(int parenthesisIndex, ParenthesesNode child)
        {
            var parenthesisCount = 0;
            //Cant use foreach as expressionIndex is returned
            for (var expressionIndex = 0; expressionIndex < child.Parent.Expressions.Count; expressionIndex++)
            {
                if (child.Parent.Expressions[expressionIndex] is ParenthesedExpression) parenthesisCount++;
                if (parenthesisCount == parenthesisIndex) return expressionIndex;
            }
            throw new InvalidOperationException();
        }
        IExpression PointBeforeAdditionOrSubtraction(IList<IExpression> expressions)
        {
            HandleOperations<Multiplication, Division>(expressions);
            HandleOperations<Addition, Subtraction>(expressions);
            return expressions.First();
        }
        void HandleParenthesesExpressions()
        {
            //Cant use foreach because changing mExpressions
            for (var i = 0; i < mExpressions.Count; i++)
            {
                var expression = mExpressions[i];
                if (expression is ParenthesedExpression)
                {
                    mNode = mRootNodes[0];
                    if (mNode.HasChild) { IterateChildren(mNode); }
                    mNode.ParenthesedExpression.Wrapped = PointBeforeAdditionOrSubtraction(mNode.Expressions);
                    mNode.ParenthesedExpression.Wrapped.Parent = mNode.ParenthesedExpression;
                    mRootNodes.RemoveAt(0);
                    mExpressions[i] = mNode.ParenthesedExpression;
                }
            }
        }
        void HandleOperations<U, V>(IList<IExpression> expressions) where U : IArithmeticOperation
            where V : IArithmeticOperation
        {
            var i = 0;
            while (i < expressions.Count)
            {
                if (expressions[i] is U) HandleOperation<U>(expressions, i);
                else if (expressions[i] is V) HandleOperation<V>(expressions, i);
                else ++i;
            }
        }
        void HandleOperation<T>(IList<IExpression> expressions, int index) where T : IArithmeticOperation
        {
            mCurrentOperation = (T) expressions[index];
            FillOperators(index, expressions);
        }
        void FillOperators(int i, IList<IExpression> expressions)
        {
            mCurrentOperation.Left = expressions[i - 1];
            mCurrentOperation.Right = expressions[i + 1];
            mCurrentOperation.Left.Parent = mCurrentOperation;
            mCurrentOperation.Right.Parent = mCurrentOperation;
            expressions.RemoveAt(i + 1);
            expressions.RemoveAt(i - 1);
        }
        static IArithmeticOperation CreateOperatorExpression(OperatorToken operatorToken)
        {
            switch (operatorToken.Operator)
            {
                case Operator.Add:
                    return new Addition();
                case Operator.Subtract:
                    return new Subtraction();
                case Operator.Multiply:
                    return new Multiplication();
                case Operator.Divide:
                    return new Division();
            }
            throw new ArgumentOutOfRangeException();
        }
        void Add(IExpression expression)
        {
            if (IsWrapped) mNode.Expressions.Add(expression);
            else mExpressions.Add(expression);
        }
        void HandleClosingParenthesis()
        {
            mNode = mNode.Parent;
        }
        void HandleChildOpeningParenthesis()
        {
            mNode.Expressions.Add(new ParenthesedExpression());
            var childNode = new ParenthesesNode();
            mNode.AddChild(childNode);
            mNode = childNode;
        }
        void HandleTopLevelOpeningParenthesis()
        {
            mNode = new ParenthesesNode();
            mExpressions.Add(new ParenthesedExpression());
            mRootNodes.Add(mNode);
        }
    }
}
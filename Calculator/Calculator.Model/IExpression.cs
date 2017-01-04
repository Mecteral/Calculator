﻿namespace Calculator.Model
{
    public interface IExpression
    {
        IExpression Parent { get; }
        bool HasParent { get; }
        void Accept(IExpressionVisitor visitor);
    }
}
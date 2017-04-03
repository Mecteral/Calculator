﻿using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class ParenthesisAroundConstantsRemover : AVisitingTraversingReplacer
    {
        protected override IExpression ReplaceParenthesed(ParenthesedExpression parenthesed)
            => parenthesed.Wrapped is Constant ? parenthesed.Wrapped : parenthesed;
    }
}
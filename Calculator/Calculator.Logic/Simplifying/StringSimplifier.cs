﻿using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Simplifying
{
    public class StringSimplifier
    {
        readonly IExpressionFormatter mFormatter;
        readonly IModelBuilder mModelBuilder;
        readonly ISimplifier mSimplifier;
        readonly ITokenizer mTokenizer;
        public StringSimplifier(
            ISimplifier simplifier,
            ITokenizer tokenizer,
            IExpressionFormatter formatter,
            IModelBuilder modelBuilder)
        {
            mSimplifier = simplifier;
            mTokenizer = tokenizer;
            mFormatter = formatter;
            mModelBuilder = modelBuilder;
        }
        public string Simplify(string input)
        {
            mTokenizer.Tokenize(input, null);
            var expressionTree = mModelBuilder.BuildFrom(mTokenizer.Tokens);
            var simplified = mSimplifier.Simplify(expressionTree);
            return mFormatter.Format(simplified);
        }
    }
}
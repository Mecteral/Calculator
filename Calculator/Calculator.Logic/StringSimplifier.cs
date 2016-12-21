using Calculator.Logic.Model;
using Calculator.Logic.Parsing;

namespace Calculator.Logic
{
    public class StringSimplifier
    {
        readonly ISimplifier mSimplifier;
        readonly ITokenizer mTokenizer;
        readonly IExpressionFormatter mFormatter;
        readonly IModelBuilder mModelBuilder;
        public StringSimplifier(ISimplifier simplifier, ITokenizer tokenizer, IExpressionFormatter formatter, IModelBuilder modelBuilder)
        {
            mSimplifier = simplifier;
            mTokenizer = tokenizer;
            mFormatter = formatter;
            mModelBuilder = modelBuilder;
        }
        public string Simplify(string input)
        {
            mTokenizer.Tokenize(input);
            var expressionTree = mModelBuilder.BuildFrom(mTokenizer.Tokens);
            var simplified = mSimplifier.Simplify(expressionTree);
            return mFormatter.Format(simplified);
        }
    }
}
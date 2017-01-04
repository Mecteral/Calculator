﻿using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class SimplifierTests
    {
        static void Check(string input, string expected)
        {
            var tokens = Tokenize(input);
            var inputTree = CreateInMemoryModel(tokens);
            var simplified = new Simplifier().Simplify(inputTree);
            var asString = new FormattingExpressionVisitor().Format(simplified);
            asString.Should().Be(expected);
        }
        static IEnumerable<IToken> Tokenize(string input)
        {
            var tokenizer = new Tokenizer();
            tokenizer.Tokenize(input);
            var tokens = tokenizer.Tokens;
            return tokens;
        }
        static IExpression CreateInMemoryModel(IEnumerable<IToken> tokens) => new ModelBuilder().BuildFrom(tokens);

        [Test]
        public void LoopRemovesParenthesesAndCalculatesPossibleCalculationAnew()
        {
            Check("(1+2)*3a", "9*a");
        }

        [Test]
        public void LoopRearrangesConstantsAndAddsThem()
        {
            Check("(1+2)*3a+4-2a+3", "9*a - 2*a + 7");
        }
    }
}
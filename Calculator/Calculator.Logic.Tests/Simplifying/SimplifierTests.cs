﻿using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplification
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
            tokenizer.Tokenize(input, null);
            var tokens = tokenizer.Tokens;
            return tokens;
        }
        static IExpression CreateInMemoryModel(IEnumerable<IToken> tokens) => new ModelBuilder().BuildFrom(tokens);
        [Test]
        public void ComplexCaseWithFullSimplification()
        {
            Check("(1/1)+1a-2*3+2a-4a", "-5 - 1*a");
        }
        [Test]
        public void FullSimplification()
        {
            Check("(1a+2a)*3+4-2a+5-6a", "(3*a)*3 + 9 - 8*a");
        }
        [Test]
        public void FullSimplificationWithVariableAtTheBeginning()
        {
            Check("a+1+2+3", "1*a + 6");
        }
        [Test]
        public void LoopRearrangesConstantsAndAddsThem()
        {
            Check("(1+2)*3a+4-2a+3", "7*a + 7");
        }
        [Test]
        public void LoopRemovesParenthesesAndCalculatesPossibleCalculationAnew()
        {
            Check("(1+2)*3a", "9*a");
        }
        [Test]
        public void MoverWithFullSimplificationSubtraction()
        {
            Check("-1+2a-3+4a-4+5a", "-8 + 11*a");
        }
        [Test]
        public void Regression_01()
        {
            Check("-1 + 2a -  3 - 4 + 9a", "-8 + 11*a");
        }
        [Test]
        public void SimplificationProcessHandlesDifferentVariablesCorrectly()
        {
            Check("a+2b+3a+4b", "4*a + 6*b");
        }

        [Test]
        public void SimplificationWithCosine()
        {
            Check("cos(60deg)+1a+23a+23-13", "0.5 + 24*a + 10");
        }
        [Test]
        public void SimplificationWithTangent()
        {
            Check("tan(45deg)+1a+23a+23-13", "1.0 + 24*a + 10");
        }
        [Test]
        public void SimplificationWithSinus()
        {
            Check("sin(30deg)+1a+23a+23-13", "0.5 + 24*a + 10");
        }
    }
}
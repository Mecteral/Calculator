﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    class VariableCalculatorTests
    {
        static void Check(string input, string expected)
        {
            var tokens = Tokenize(input);
            var inputTree = CreateInMemoryModel(tokens);
            var calculator = new VariableCalculator();
            var underTest = calculator.Calculate(inputTree);
            var asString = new FormattingExpressionVisitor().Format(underTest);
            asString.Should().Be(expected);
        }

        static void CheckWithFullSimplification(string input, string expected)
        {
            var tokens = Tokenize(input);
            var inputTree = CreateInMemoryModel(tokens);
            var simplifier = new Simplifier();
            var underTest = simplifier.Simplify(inputTree);
            var asString = new FormattingExpressionVisitor().Format(underTest);
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
        public void AdditionOfVariables()
        {
            Check("1a+2a", "3*a");
        }

        [Test]
        public void SubtractionOfVariables()
        {
            Check("1a-1a", "0*a");
        }

        [Test]
        public void CalculatorDoesntCalculateDifferingVariables()
        {
            Check("1a+2b", "1*a + 2*b");
        }

        [Test]
        public void CalculatorFindsVariablesDownTheChain()
        {
            Check("1a+2+3a", "2 + 4*a");
        }

        [Test]
        public void CalculationInParentheses()
        {
            Check("(1a+2a)", "(3*a)");
        }

        [Test]
        public void CalculatorFindsVariablesInComplexForm()
        {
            Check("1+2a+3+4a", "1 + 3 + 6*a");
        }

        [Test]
        public void SimpleSubtractionOfVariables()
        {
            Check("3a-2-1a", "0 - 2 + 2*a");
        }

        [Test]
        public void ComplexSubtractionOfVariables()
        {
            Check("1-4a-3-1a", "1 - 0 - 3 - 5*a");
        }

        [Test]
        public void SimpleAdditionToSubtraction()
        {
            Check("3a+2-1a", "2 - -2*a");
        }

        [Test]
        public void ComplexAdditionToSubtraction()
        {
            Check("1+3a+2-1a", "1 + 2 - -2*a");
        }

        [Test]
        public void SimpleSubtractionToAddition()
        {
            Check("1a-2+2a", "0 - 2 + 3*a");
        }

        [Test]
        public void ComplexSubtractionToAddition()
        {
            Check("1-1a-2+2a", "1 - 0 - 2 + 1*a");
        }

        [Test]
        public void ComplexCaseWithFullSimplification()
        {
            CheckWithFullSimplification("(1/1)+1a-2*3+2a-4a", "-5 - 1*a");
        }

        [Test]
        public void FullSimplification()
        {
            CheckWithFullSimplification("(1a+2a)*3+4-2a+5-6a", "(3*a)*3 + 9 - 8*a");
        }
    }
}
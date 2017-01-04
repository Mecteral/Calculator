﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Parsing;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class ExpressionEqualityCheckerTests
    {
        static void Check(string first, string second, bool expected)
        {
            var tokensFirst = Tokenize(first);
            var inputTreeFirst = CreateInMemoryModel(tokensFirst);
            var tokensSecond = Tokenize(second);
            var inputTreeSecond = CreateInMemoryModel(tokensSecond);
            var result = new ExpressionEqualityChecker();
            result.IsEqual(inputTreeFirst, inputTreeSecond).Should().Be(expected);
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
        public void UnequalExpressionsReturnFalse()
        {
            Check("(1+2)", "1-2*3a", false);
        }

        [Test]
        public void EqualExpressionsReturnTrue()
        {
            Check("1/2", "1/2", true);
        }

        [Test]
        public void SubtractionCaseChecker()
        {
            Check("1-2", "1-2", true);
        }
        [Test]
        public void MultiplicationCaseChecker()
        {
            Check("1*2", "1*2", true);
        }
        [Test]
        public void VariableCaseChecker()
        {
            Check("1-2a", "1-2a", true);
        }
        [Test]
        public void ParenthesesCaseChecker()
        {
            Check("(1-2)", "(1-2)", true);
        }
    }
}
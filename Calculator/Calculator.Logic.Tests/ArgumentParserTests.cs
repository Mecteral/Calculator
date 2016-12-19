using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class ArgumentParserTests
    {
        static void TestWithSingleCharInput(string input, string expected)
        {
            var inputList = input.Select(c => c.ToString()).ToList();
            var expectedList = expected.Select(c => c.ToString()).ToList();
            var result = ArgumentParser.Tokenize(inputList);
            result.Should().Equal(expectedList);
        }

        static void Test(string input, string expected)
        {
            var result = ArgumentParser.Tokenize(input.Split(','));
            result.Should().Equal(expected.Split(','));
        }

        [Test]
        public void ParseToMemoryModel_Works_With_Chained_Terms()
        {
            TestWithSingleCharInput("1+2*3-4/5", "(1+(2*3)-(4/5))");
        }

        [Test]
        public void ParseToMemoryModel_Works_With_Parenthesed_Terms()
        {
            Test("(,1,+,2,),*,(,2,+,3,)", "(,(,1,+,2,),*,(,2,+,3,),)");
        }

        [Test]
        public void Tokenize_Adds_Brackets_Around_A_Multiplication_With_Following_Bracket()
        {
            Test("1,/,2,*,(,3,/,4,)", "(,(,1,/,2,),*,(,3,/,4,),)");
        }

        [Test]
        public void Tokenize_Adds_Brackets_Around_Every_Multiplication_And_Division()
        {
            Test("1,*,2,*,(,3,*,4,)", "(,(,1,*,2,),*,(,3,*,4,),)");
        }

        [Test]
        public void Tokenize_Adds_Brackets_Around_Multiplication_Correctly()
        {
            Test("2,*,3", "(,2,*,3,)");
        }

        [Test]
        public void Tokenize_Adds_Brackets_Around_The_Whole_Input()
        {
            Test("1,*,2,*,(,3,+,4,)", "(,(,1,*,2,),*,(,3,+,4,),)");
        }

        [Test]
        public void Tokenize_Deals_Correctly_With_Negative_Numbers()
        {
            TestWithSingleCharInput("-1+(-2)-(-3)", "(-1+(-2)-(-3))");
        }

        [Test]
        public void Tokenizer_Removes_Whitespace_Correctly()
        {
            Test("-2, ,+,34", "(,-2,+,34,)");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Parsing.CalculationTokenizer;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Parsing.CalculationTokenizer
{
    [TestFixture]
    public class InputStringValidatorTests
    {
        InputStringValidator mValidator;

        [SetUp]
        public void SetUp()
        {
            mValidator = new InputStringValidator();
        }

        [Test]
        public void Double_Variable_Throws_Exception()
        {
            Action a = () => mValidator.Validate("2aa");
            a.ShouldThrow<CalculationException>();
        }

        [Test]
        public void Validator_Accepts_Rad()
        {
            mValidator.Validate("sin(90rad)");

        }

        [Test]
        public void Validator_Accepts_Deg()
        {
            mValidator.Validate("cos(90deg)");
        }

        [Test]
        public void Validator_Accepts_tan()
        {
            mValidator.Validate("tan(90deg)");
        }

        [Test]
        public void Validator_Throws_Exception_If_Number_Is_Defined_After_Deg_Or_Rad_In_Trigonometric_Function()
        {
            Action a = () => mValidator.Validate("tan(deg90)");
            a.ShouldThrow<CalculationException>();
        }

        [Test]
        public void Validator_Throws_Exception_If_String_Holds_Unknown_Character()
        {
            Action a = () => mValidator.Validate("#+tan(90)");
            a.ShouldThrow<CalculationException>();
        }
        [Test]
        public void Validator_Throws_Exception_If_String_Holds_Uneven_Amount_Of_Parantheses()
        {
            Action a = () => mValidator.Validate("(2+3))");
            a.ShouldThrow<CalculationException>();
        }
        [Test]
        public void Validator_Throws_Exception_If_Function_Holds_No_Information()
        {
            Action a = () => mValidator.Validate("sqrt()");
            a.ShouldThrow<CalculationException>();
        }
        [Test]
        public void Validator_Throws_Exception_If_Function_Is_Not_Closed()
        {
            Action a = () => mValidator.Validate("sqrt(");
            a.ShouldThrow<CalculationException>();
        }
        [Test]
        public void Validator_Throws_Exception_If_Function_With_Value_Holds_Variables()
        {
            Action a = () => mValidator.Validate("sqrt(25a)");
            a.ShouldThrow<CalculationException>();
        }

        [Test]
        public void Validator_Accepts_Squared_With_Addition()
        {
            mValidator.Validate("sqrt(9)+34");
        }
    }
}

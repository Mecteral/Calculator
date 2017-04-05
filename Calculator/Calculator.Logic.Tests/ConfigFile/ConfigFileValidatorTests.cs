using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.ConfigFile;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.ConfigFile
{
    [TestFixture]
    public class ConfigFileValidatorTests
    {
        [SetUp]
        public void SetUp()
        {
            mValidator = new ConfigFileValidator();
        }

        ConfigFileValidator mValidator;

        [Test]
        public void ConfigFileValidator_Adds_Error_For_Wrong_Unit()
        {
            var input = new string[] {"-u=gh"};
            mValidator.Validate(input, "");
            mValidator.Errors.Count.Should().Be(1);
            mValidator.Errors.Should().Contain("Wrong abbreviation in config file");
        }

        [Test]
        public void ConfigFileValidator_Adds_Error_For_Different_Unit_Entries()
        {
            var input = new string[] { "-u=m", "--unit==km"};
            mValidator.Validate(input, "");
            mValidator.Errors.Count.Should().Be(1);
            mValidator.Errors.Should().Contain("Conflicting entries with unit");
        }
        [Test]
        public void ConfigFileValidator_Adds_Error_For_Different_Degree_Entries()
        {
            var input = new string[] { "-d=true", "--degree==false" };
            mValidator.Validate(input, "");
            mValidator.Errors.Count.Should().Be(1);
            mValidator.Errors.Should().Contain("Conflicting entries with degree");
        }
        [Test]
        public void ConfigFileValidator_Adds_Error_For_Different_Step_Entries()
        {
            var input = new string[] { "-s=true", "--steps==false" };
            mValidator.Validate(input, "");
            mValidator.Errors.Count.Should().Be(1);
            mValidator.Errors.Should().Contain("Conflicting entries with steps");
        }
        [Test]
        public void ConfigFileValidator_Adds_Error_For_Different_Writer_Entries()
        {
            var input = new string[] { "-w=true", "--writer==false" };
            mValidator.Validate(input, "");
            mValidator.Errors.Count.Should().Be(1);
            mValidator.Errors.Should().Contain("Conflicting entries with writer");
        }
        [Test]
        public void ConfigFileValidator_Adds_Error_For_Different_Revert_Entries()
        {
            var input = new string[] { "-r=true", "--revert==false" };
            mValidator.Validate(input, "");
            mValidator.Errors.Count.Should().Be(1);
            mValidator.Errors.Should().Contain("Conflicting entries with revert");
        }
        [Test]
        public void ConfigFileValidator_Adds_Error_For_Different_Import_Entries()
        {
            var input = new string[] { "-i=true", "--import==false" };
            mValidator.Validate(input, "");
            mValidator.Errors.Count.Should().Be(1);
            mValidator.Errors.Should().Contain("Conflicting entries with import");
        }
        [Test]
        public void ConfigFileValidator_Adds_Error_For_Different_Custom_Entries()
        {
            var input = new string[] { "-c=true", "--custom==false" };
            mValidator.Validate(input, "");
            mValidator.Errors.Count.Should().Be(1);
            mValidator.Errors.Should().Contain("Conflicting entries with custom");
        }
        [Test]
        public void ConfigFileValidator_Adds_Error_For_Different_SaveAll_Entries()
        {
            var input = new string[] { "-a=true", "--saveAll==false" };
            mValidator.Validate(input, "");
            mValidator.Errors.Count.Should().Be(1);
            mValidator.Errors.Should().Contain("Conflicting entries with saveAll");
        }
        [Test]
        public void ConfigFileValidator_Adds_Error_For_Wrong_Argument()
        {
            var input = new string[] { "-x=true", "--test==false" };
            mValidator.Validate(input, "");
            mValidator.Errors.Count.Should().Be(1);
            mValidator.Errors.Should().Contain("There is an Argument defined that doesnt exist.");
        }
    }
}

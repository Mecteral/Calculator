using System.Collections.Generic;
using System.Linq;
using Autofac;
using Calculator.Logic;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Pipelines;
using Calculator.Logic.Utilities;
using FluentAssertions;
using Mecteral.Calculator.IntegrationTests.Properties;
using NSubstitute;
using NUnit.Framework;

namespace Mecteral.Calculator.IntegrationTests
{
    [TestFixture]
    // ReSharper disable once TestFileNameWarning
    public class IntegrationTests
    {
        static IEnumerable<string[]> TestCases
        {
            get
            {
                return
                    Resources.TestCases.Split('\r', '\n')
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Select(l => l.Split('='));
            }
        }
        //[TestCase("1+1", "2")]
        [TestCaseSource(nameof(TestCases))]
        public void Run_Integration_Case(string input, string expected)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(LogicModule).Assembly);
            var container = builder.Build();
            var simplificationPipeline = container.Resolve<ISimplificationPipeline>();
            var output = simplificationPipeline.UseSimplificationPipeline(input.WithoutAnyWhitespace(), Substitute.For<IApplicationArguments>());
            output.WithoutAnyWhitespace()
                .Should()
                .Be(expected.WithoutAnyWhitespace());
        }
        
    }
}
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.ConfigFile;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.Logic.Tests.ConfigFile
{
    [TestFixture]
    public class SwitchesToConfigFileWriterTests
    {
        [SetUp]
        public void SetUp()
        {
            mWriter = Substitute.For<IConfigFileWriter>();
            mUnderTest = new SwitchesToConfigFileWriter(mWriter);
        }

        SwitchesToConfigFileWriter mUnderTest;
        IConfigFileWriter mWriter;

        [Test]
        public void Long_Differing_Switch_To_Config_Is_Detected()
        {
            var switches = new[] {"--unit = m", "-s=false"};
            var config = new[] {"--unit = km", "-s=true"};
            var args = new ApplicationArguments();
            var path = "";

            mUnderTest.WriteToConfigFile(switches, config, args, path);
        }

        [Test]
        public void Short_Differing_Switch_To_Config_Is_Detected()
        {
            var switches = new[] {"-u = m", "-s=false"};
            var config = new[] {"-u = km", "-s=true"};
            var args = new ApplicationArguments();
            var path = "";

            mUnderTest.WriteToConfigFile(switches, config, args, path);
        }

        [Test]
        public void SwitchesToConfigFileWriter_Writes_With_Proper_Argument()
        {
            var switches = new[] {"--unit = m", "-s=false"};
            var config = new[] {"--unit = km", "-s=true"};
            var args = new ApplicationArguments {SaveAllOrIgnoreAllDifferingSwitches = true};
            var path = "";

            mUnderTest.WriteToConfigFile(switches, config, args, path);
        }
    }
}
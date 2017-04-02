using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Pipelines
{
    public class ConsoleToMetricDecider : IConsoleToMetricDecider
    {
        readonly IApplicationArguments mArguments;

        public ConsoleToMetricDecider(IApplicationArguments arguments)
        {
            mArguments = arguments;
        }
        public void Decide()
        {
            Console.WriteLine("Do you want to convert to the metric system? \n y, or yes for yes.");
            var metric = Console.ReadLine();
            if (metric == "y" || metric == "yes")
            {
                mArguments.ToMetric = true;
            }
        }
    }
}

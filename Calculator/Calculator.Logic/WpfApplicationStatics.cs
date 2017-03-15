using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Calculator.Logic
{        
    //All saved options for startup
    [DataContract]
    public class WpfApplicationStatics
    {
        [DataMember]
        public static bool StepExpander { get; set; }
        [DataMember]
        public static bool UnitExpander { get; set; }
        [DataMember]
        public static bool UseMetric { get; set; }
        [DataMember]
        public static bool IsConversionActive { get; set; }
        [DataMember]
        public static int ShellWindowHeight { get; set; }
        [DataMember]
        public static int ShellWindowWidth { get; set; }
        [DataMember]
        public static int ShellWindowPositionX { get; set; }
        [DataMember]
        public static int ShellWindowPositionY { get; set; }
        [DataMember]
        public static string LastPickedUnit { get; set; }
    }
}

namespace Examples.Ci.Core.Utilities
{
    using Infrastructures;

    public class BootstrapSettings
    {
        public static ISettings Settings { get; set; }

        static BootstrapSettings() { }
    }
}

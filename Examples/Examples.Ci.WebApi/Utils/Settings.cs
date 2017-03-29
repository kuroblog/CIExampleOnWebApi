
namespace Examples.Ci.WebApi.Utils
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    public interface ISettings
    {
        string DefaultConnectionString { get; }
    }

    [ExcludeFromCodeCoverage]
    public class Settings : ISettings
    {
        private const string defaultConnectionStringKey = nameof(DefaultConnectionString);

        public string DefaultConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[defaultConnectionStringKey].ConnectionString; }
        }
    }
}
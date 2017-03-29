
using System;
using System.Configuration;

namespace Examples.Ci.Core.Infrastructures
{
    public interface ISettings
    {
        string ConnectionString { get; }
    }

    public class BasicSettings : ISettings
    {
        private const string connectionString = nameof(connectionString);

        public virtual string ConnectionString
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}

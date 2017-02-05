
namespace Examples.Ci.Ef
{
    using Entities;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class CiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

#if DEBUG
        private const string connectionStringKey = "CiContextDevKey";
#else
        private const string connectionStringKey = "CiContextProdKey";
#endif

        public CiContext() : base($"name={connectionStringKey}") { }

        public DbSet<UserEntity> Users { get; set; }
    }
}
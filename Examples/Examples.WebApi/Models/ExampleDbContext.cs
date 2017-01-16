
namespace Examples.WebApi.Models
{
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ExampleDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

#if DEBUG
        private const string connectionStringKey = "ExampleDbContext";
#else
        private const string connectionStringKey = "CiOnWebApiContext";
#endif

        public ExampleDbContext() : base($"name={connectionStringKey}") { }

        public DbSet<UserEntity> Users { get; set; }
    }
}

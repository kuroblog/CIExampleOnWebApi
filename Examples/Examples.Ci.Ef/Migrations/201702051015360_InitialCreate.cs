
namespace Examples.Ci.Ef.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.t_users",
                c => new
                    {
                        id = c.Guid(nullable: false, identity: true),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        user_no = c.String(nullable: false, maxLength: 30),
                        user_name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.user_no, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.t_users", new[] { "user_no" });
            DropTable("dbo.t_users");
        }
    }
}

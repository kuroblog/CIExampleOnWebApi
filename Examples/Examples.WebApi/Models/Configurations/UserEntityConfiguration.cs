
namespace Examples.WebApi.Models
{
    using System.Data.Entity.ModelConfiguration;

    public class UserEntityConfiguration : EntityTypeConfiguration<UserEntity>
    {
        public UserEntityConfiguration()
        {
            //Property(p=>p.CreatedAt).
        }
    }
}
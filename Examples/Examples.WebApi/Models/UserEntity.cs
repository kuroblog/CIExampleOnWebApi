
namespace Examples.WebApi.Models
{
    using Infrastructures;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_users")]
    public class UserEntity : IBasicEntity
    {
        #region IBasicEntity Implements
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("created_at"), Required]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at"), Required]
        public DateTime UpdatedAt { get; set; }
        #endregion

        [Column("user_no"), Required, Index(IsUnique = true), StringLength(30, MinimumLength = 3)]
        public string UserNo { get; set; }

        [Column("user_name"), Required, StringLength(50, MinimumLength = 5)]
        public string UserName { get; set; }

        public UserEntity()
        {
            Id = Guid.NewGuid();
            UpdatedAt = DateTime.Now;
        }
    }
}
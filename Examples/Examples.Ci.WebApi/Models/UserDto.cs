
namespace Examples.Ci.WebApi.Models
{
    using Ef.Entities;
    using System;
    using System.Linq.Expressions;

    public class UserDto
    {
        public string UserNo { get; set; }

        public string UserName { get; set; }

        public static readonly Expression<Func<UserEntity, UserDto>> UserEntityParse = p => new UserDto
        {
            UserNo = p.UserNo,
            UserName = p.UserName
        };
    }
}
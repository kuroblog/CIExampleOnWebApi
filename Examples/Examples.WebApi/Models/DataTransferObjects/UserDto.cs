
namespace Examples.WebApi.Models
{
    using System;
    using System.Linq.Expressions;

    public class UserDto
    {
        public string UserNo { get; set; }

        public string UserName { get; set; }

        public static readonly Expression<Func<UserEntity, UserDto>> UserEntityParse = x => new UserDto
        {
            UserNo = x.UserNo,
            UserName = x.UserName
        };
    }
}
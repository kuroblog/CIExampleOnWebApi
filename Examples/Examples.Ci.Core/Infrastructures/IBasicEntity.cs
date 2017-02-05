
namespace Examples.Ci.Core.Infrastructures
{
    using System;

    public interface IBasicEntity
    {
        Guid Id { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}
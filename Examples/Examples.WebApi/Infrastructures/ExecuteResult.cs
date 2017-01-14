
namespace Examples.WebApi.Infrastructures
{
    using System;

    public class ExecuteResult<T>
    {
        public Guid Id { get; }

        public Exception Error { get; set; }

        public bool HasError
        {
            get { return Error != null; }
        }

        public T Content { get; set; }

        public ExecuteResult(Exception error = null)
        {
            Id = Guid.NewGuid();
            Error = error;
        }
    }
}
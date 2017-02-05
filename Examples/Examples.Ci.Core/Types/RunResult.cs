
namespace Examples.Ci.Core.Types
{
    using System;

    public class RunResult<TObject>
    {
        public Guid Id { get; }

        public Exception Error { get; set; }

        public bool HasError
        {
            get { return Error != null; }
        }

        public TObject Content { get; set; }

        public RunResult(Exception error = null)
        {
            Id = Guid.NewGuid();
            Error = error;
        }
    }
}
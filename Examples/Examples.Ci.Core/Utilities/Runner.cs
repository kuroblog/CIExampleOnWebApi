
namespace Examples.Ci.Core.Utilities
{
    using System;
    using System.Threading.Tasks;
    using Types;

    public static class Runner
    {
        public static async Task<RunResult<TObject>> Execute<TObject>(Func<TObject> func)
        {
            var result = new RunResult<TObject>();

            try
            {
                result.Content = func.Invoke();
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }

            return await Task.FromResult(result);
        }
    }
}
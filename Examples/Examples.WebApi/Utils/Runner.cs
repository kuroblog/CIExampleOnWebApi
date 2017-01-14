
namespace Examples.WebApi
{
    using Infrastructures;
    using System;
    using System.Threading.Tasks;

    public static class Runner
    {
        public static async Task<ExecuteResult<T>> Execute<T>(Func<T> func)
        {
            var result = new ExecuteResult<T>();

            try
            {
                var invokeResult = func.Invoke();
                result.Content = invokeResult;
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }

            return await Task.FromResult(result);
        }
    }
}
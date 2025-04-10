using BuildCell.LogMe.Constants;
using BuildCell.LogMe.Services;

namespace ExampleDotnetFramework
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var projectToken = "";
            var authToken = "";

            var logMe = new LogMeService(authToken, projectToken, LogEnvironment.Development);

            var createLogResult = logMe.InfoAsync("test", "text description").Result;
            var createErrorLogResult = logMe.ErrorAsync("test", "text description").Result;

            var resultInfo = logMe.GetLogsAsync(projectToken).Result;
        }
    }
}
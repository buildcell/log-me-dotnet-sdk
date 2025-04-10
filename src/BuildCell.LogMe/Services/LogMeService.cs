using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BuildCell.LogMe.Constants;
using BuildCell.LogMe.Contracts;
using BuildCell.LogMe.Dtos;

namespace BuildCell.LogMe.Services
{
    public class LogMeService : ILogMeService
    {
        private string _authToken;
        private string _projectToken;
        private readonly LogEnvironment _environment;

        private HttpClient _httpClient;

        public LogMeService(string authToken, string projectToken, LogEnvironment environment = LogEnvironment.Production, string baseUrl = LogMeConstant.BaseUrl)
        {
            _authToken = authToken ?? throw new NullReferenceException("Token cannot be null");
            _projectToken = projectToken ?? throw new NullReferenceException("Project Id cannot be null");
            _environment = environment;

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl),
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
            _httpClient.DefaultRequestHeaders.Add("token", _projectToken);
        }

        /// <summary>
        /// Sends an info log asynchronously to the logger API.
        /// </summary>
        /// <param name="name">The name of the info or the source where it occurred.</param>
        /// <param name="description">Detailed info description for diagnosis.</param>
        /// <returns>
        /// A result object containing either the logging outcome.
        /// </returns>
        public async Task<IResult<object>> InfoAsync(string name, string description, string trace = "")
        {
            try
            {
                var createRequest = new CreateLog()
                {
                    Name = name,
                    Environment = GetEnvironment(_environment),
                    Level = GetLogLevel(LogLevel.Info),
                    Trace = trace,
                    Description = description,
                };

                var response = await _httpClient.PostAsJsonAsync("api/Logger", createRequest);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Result<object>>();
                }

                return Result<object>.Error($"Failed to send info log. StatusCode: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return Result<object>.Error($"Exception while logging info: {ex.Message}");
            }
        }

        /// <summary>
        /// Sends an error log asynchronously to the logger API.
        /// </summary>
        /// <param name="name">The name of the error or the source where it occurred.</param>
        /// <param name="description">Detailed error description for diagnosis.</param>
        /// <param name="trace">Optional stack trace or additional trace information.</param>
        /// <returns>
        /// A result object containing either the logging outcome or error information in case of failure.
        /// </returns>
        public async Task<IResult<object>> ErrorAsync(string name, string description, string trace = "")
        {
            try
            {
                var createRequest = new CreateLog()
                {
                    Name = name,
                    Environment = GetEnvironment(_environment),
                    Level = GetLogLevel(LogLevel.Error),
                    Trace = trace,
                    Description = description,
                };

                var response = await _httpClient.PostAsJsonAsync("api/Logger", createRequest);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Result<object>>();
                }

                return Result<object>.Error($"Failed to send error log. StatusCode: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return Result<object>.Error($"Exception while logging error: {ex.Message}");
            }
        }

        public async Task<IResult<string>> GetLogsAsync(string projectToken)
        {
            _httpClient.DefaultRequestHeaders.Add("token", projectToken);
            var result = await _httpClient.GetFromJsonAsync<Result<string>>("api/Logger");

            return result;
        }

        private string GetLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Warn: return LogLevelConstant.Warn;
                case LogLevel.Error: return LogLevelConstant.Error;
                case LogLevel.Trace: return LogLevelConstant.Trace;
                default: return LogLevelConstant.Info;
            }
        }

        private string GetEnvironment(LogEnvironment environment)
        {
            switch (environment)
            {
                case LogEnvironment.Development: return EnvironmentConstant.Development;
                case LogEnvironment.Uat: return EnvironmentConstant.Uat;
                default:
                    return EnvironmentConstant.Production;
            }
        }

        private class CreateLog
        {
            public string Name { get; set; }
            public string Environment { get; set; }
            public string Level { get; set; }
            public string Trace { get; set; } = "";
            public string Description { get; set; } = "";
        }
    }
}
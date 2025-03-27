namespace BuildCell.LogMe.Constants
{
    public static class LogLevelConstant
    {
        public const string Info = "Info";
        public const string Warn = "Warn";
        public const string Error = "Error";
        public const string Trace = "Trace";
    }

    public enum LogLevel
    {
        Info,
        Warn,
        Error,
        Trace
    }

    public enum LogEnvironment
    {
        Production,
        Development,
        Uat,
    }

    public static class EnvironmentConstant
    {
        public const string Production = "Production";
        public const string Development = "Development";
        public const string Uat = "UAT";
    }

    public static class LogMeConstant
    {
        public const string BaseUrl = "https://bug-me.t3vada.xyz";
    }
}
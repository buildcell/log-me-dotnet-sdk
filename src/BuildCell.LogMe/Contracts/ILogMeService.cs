using System.Threading.Tasks;

namespace BuildCell.LogMe.Contracts
{
    public interface ILogMeService
    {
        Task<IResult<object>> InfoAsync(string name, string description, string trace = "");
        Task<IResult<object>> ErrorAsync(string name, string description, string trace = "");
        Task<IResult<string>> GetLogsAsync(string projectToken);
    }
}
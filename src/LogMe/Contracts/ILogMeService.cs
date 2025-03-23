using System.Threading.Tasks;
using LogMe.Constants;
using LogMe.Dtos;

namespace LogMe.Contracts
{
    public interface ILogMeService
    {
        Task<IResult<object>> InfoAsync(string name, string description, string trace = "");
        Task<IResult<string>> GetLogsAsync(string projectToken);
    }
}
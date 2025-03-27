using System.Threading.Tasks;
using BuildCell.LogMe.Constants;
using BuildCell.LogMe.Dtos;

namespace BuildCell.LogMe.Contracts
{
    public interface ILogMeService
    {
        Task<IResult<object>> InfoAsync(string name, string description, string trace = "");
        Task<IResult<string>> GetLogsAsync(string projectToken);
    }
}
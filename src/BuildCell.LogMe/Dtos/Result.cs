using BuildCell.LogMe.Contracts;

namespace BuildCell.LogMe.Dtos
{
    public class Result<T> : IResult<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = "";
        public int StatusCode { get; set; }
    }
}
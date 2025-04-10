using BuildCell.LogMe.Contracts;

namespace BuildCell.LogMe.Dtos
{
    public class Result<T> : IResult<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = "";
        public int StatusCode { get; set; }

        public static Result<T> Success(T data, string message = "", int statusCode = 200)
        {
            return new Result<T>()
            {
                Data = data,
                Message = message,
                StatusCode = statusCode,
            };
        }

        public static Result<T> Error(string message = "", int statusCode = 500)
        {
            return new Result<T>()
            {
                Message = message,
                StatusCode = statusCode,
            };
        }
    }
}
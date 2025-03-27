namespace BuildCell.LogMe.Contracts
{
    public interface IResult<T>
    {
        T Data { get; set; }
        string Message { get; set; }
        int StatusCode { get; set; }
    }
}
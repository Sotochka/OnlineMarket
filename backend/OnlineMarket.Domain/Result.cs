namespace OnlineMarket.Domain;

public class Result
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }

    public static Result Success() => new() { IsSuccess = true };
    public static Result Failure(string errorMessage) => new() { IsSuccess = false, ErrorMessage = errorMessage };
}

public class Result<T> : Result
{
    public T Value { get; set; }
    public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };
    public new static Result<T> Failure(string errorMessage) => new() { IsSuccess = false, ErrorMessage = errorMessage };
}
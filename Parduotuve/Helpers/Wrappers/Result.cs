using System.Data;

namespace Parduotuve.Helpers.Wrappers;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
    private readonly T? _value = default(T);
    public T Value
    {
        get { return (T)_value ?? throw new InvalidOperationException(); }
    }
    private Result(T? value, bool isSuccess, string message)
    {
        _value = value;
        IsSuccess = isSuccess;
        Message = message;
    }
    public static Result<T> Ok(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }
    public static Result<T> Err(string message)
    {
        return new Result<T>(default, false, message);
    }
    public static implicit operator bool(Result<T> result) => result.IsSuccess;
}
public class Result
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
    private Result( bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }
    public static Result Err(string message)
    {
        return new Result(false, message);
    }
    public static implicit operator bool(Result result) => result.IsSuccess;
}
namespace Parduotuve.Helpers.Wrappers;

public class Result<T>
{
    private readonly T? _value;

    private Result(T? value, bool isSuccess, string message)
    {
        _value = value;
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }
    public string Message { get; private set; }
    public T Value => (T)_value ?? throw new InvalidOperationException();

    public static Result<T> Ok(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }

    public static Result<T> Err(string message)
    {
        return new Result<T>(default, false, message);
    }

    public static implicit operator bool(Result<T> result)
    {
        return result.IsSuccess;
    }
}

public class Result
{
    private Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }
    public string Message { get; private set; }

    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }

    public static Result Err(string message)
    {
        return new Result(false, message);
    }

    public static implicit operator bool(Result result)
    {
        return result.IsSuccess;
    }
}
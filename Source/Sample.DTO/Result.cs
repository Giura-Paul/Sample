using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DTO;

public class Result
{
    public bool IsSuccess { get; set; }
    required public string Message { get; set; }

    public static Result Success(string message) => new Result { IsSuccess = true, Message = message };
    public static Result Error(string message) => new Result { IsSuccess = false, Message = message };
}

public class Result<T> where T : class
{
    private bool _isSuccess;
    private T? _data;
    private string? _errorMessage;

    public bool IsSuccess => _isSuccess;

    public T Data => _data ?? throw new InvalidOperationException("Data not available if an error occurred.");

    public string ErrorMessage => _errorMessage ?? throw new InvalidOperationException("Error message not available if operation was successful.");

    public static Result<T> Success(T data) => new Result<T> { _isSuccess = true, _data = data };

    public static Result<T> Error(string errorMessage) => new Result<T> { _isSuccess = false, _errorMessage = errorMessage };
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Application.Common.Results
{
    public class OperationResult
    {
        public string? Message { get; set; }

        public bool Success { get; set; }

        public object? Data { get; set; }

        public static OperationResult Ok(string message)
        {
            return new OperationResult
            {
                Message = message,
                Success = true,
                Data = null
            };
        }

        public static OperationResult SuccessOBJ(string message, object? data)
        {
            return new OperationResult
            {
                Message = message,
                Data = data,
                Success = true
            };
        }

        public static OperationResult Fail(string message)
        {
            return new OperationResult
            {
                Message = message,
                Success = false,
                Data = null
            };
        }
    }
    public class OperationResult<T> : OperationResult
    {
        public T? Data { get; set; }

        public static OperationResult<T> Ok(T data, string? message = null) =>
            new OperationResult<T> { Success = true, Data = data, Message = message };

        public new static OperationResult<T> Fail(string message) =>
            new OperationResult<T> { Success = false, Message = message };
    }

}

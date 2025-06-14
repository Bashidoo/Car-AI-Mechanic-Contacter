using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Common.Results
{
    public class OperationResult
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public object? Data { get; set; }

        public static OperationResult Success(string message)
        {
            return new OperationResult
            {
                Message = message,
                IsSuccess = true,
                Data = null
            };
        }

        public static OperationResult SuccessOBJ(string message, object? data)
        {
            return new OperationResult
            {
                Message = message,
                Data = data,
                IsSuccess = true
            };
        }

        public static OperationResult Failure(string message)
        {
            return new OperationResult
            {
                Message = message,
                IsSuccess = false,
                Data = null
            };
        }
    }
}

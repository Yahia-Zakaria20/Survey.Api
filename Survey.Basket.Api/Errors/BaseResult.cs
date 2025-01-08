using Survey.Basket.Api.Error;
using System.Data;

namespace Survey.Basket.Api.Errors
{
    public class BaseResult
    {
        public bool IsSuccess { get; set; }

        public ApiResponse Error { get; set; } = default!;

        public BaseResult(bool issucces, ApiResponse error)
        {
         if((issucces && error != ApiResponse.None) || (!issucces && error == ApiResponse.None)) 
                throw new InvalidOperationException();

            IsSuccess = issucces;
            Error = error;
        }
    }
}


namespace Survey.Basket.Api.Error
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public static readonly  ApiResponse None = new ApiResponse(0,null);

        public ApiResponse(int code , string? massage = null )
        {
            StatusCode = code ;
            Message = massage ?? GetDefultMassageForStatusCode(code) ;
        }

        private string? GetDefultMassageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, You Have Made",
                401 => "Authorized, you are not",
                404 => "Resource Was Not Found",
                500 => "Internal Server Error",
                409 => "Duplicated Resource",
                _ => null
            };
        }
    }
}
